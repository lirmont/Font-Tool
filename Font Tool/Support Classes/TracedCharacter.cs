using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using System.Collections.Specialized;
using System.Collections;

namespace FontTool
{
	public class TracedCharacter
	{
		DerivedCharacter offsetableUnicodeCharacter;
		List<AutoTrace.Tracing> traces = new List<AutoTrace.Tracing>();
		// Image that may represent the letter (not necessarily true if tracing is involved).
		Bitmap previewImage;

		public ulong Id
		{
			get {
				return offsetableUnicodeCharacter.Id;
			}
		}

		public DerivedCharacter DerivedUnicodeCharacter
		{
			get { return offsetableUnicodeCharacter; }
			set { offsetableUnicodeCharacter = value; }
		}
		
		public List<AutoTrace.Tracing> Traces
		{
			get { return traces; }
			set { traces = value; }
		}

		public Bitmap PreviewImage
		{
			get {
				return previewImage;
			}
			set { previewImage = value; }
		}

		// A statistic used to help determine the size for combined textures (which happen to be in power of 2 dimensions) during certain exports.
		public int EstimatedPixelRequirement {
			get {
				int pixels = 0;
				foreach (AutoTrace.Tracing trace in traces)
					if (trace.Image != null)
						pixels += (int)Math.Pow(Math.Max(trace.Image.Width, trace.Image.Height) + 6, 2);
				// Send the pixels back.
				return pixels;
			}
		}

		public string SafeName
		{
			get {
				if (DerivedUnicodeCharacter != null)
				{
					string thisName = SupportFunctions.TitleCaseString(DerivedUnicodeCharacter.PrimaryName);
					string safeName = thisName.Replace("#", "").Replace("-", "").Replace(" ", "");
					return safeName;
				}
				else
					return "NoName";
			}
		}

		public TracedCharacter(DerivedCharacter offsetableUnicodeCharacter, List<AutoTrace.Tracing> traces, Bitmap previewImage)
		{
			this.offsetableUnicodeCharacter = offsetableUnicodeCharacter;
			this.traces = traces;
			this.previewImage = previewImage;
			// Calculate the percentage on each trace's vertices against the overall dimensions (rather than the local dimensions of each trace).
			updateVertexAnimationPercentages();
		}

		private void updateVertexAnimationPercentages()
		{
			if (this.traces.Count > 0)
			{
				Vector3d? min = null, max = null;
				GetBoundaries(ref min, ref max);
				if (min != null && max != null)
				{
					Vector3d dimensions = max.Value - min.Value;
					foreach (AutoTrace.Tracing trace in this.traces)
					{
						for (int i = 0; i < trace.CachedGeometry.Vertices.Count; i++)
						{
							Trace.Outbound.Vertex vertex = trace.CachedGeometry.Vertices[i];
							Vector4d vector = vertex.Auxiliary["percent"];
							double horizontalPercent = (vertex.Position.Value.X - min.Value.X) / dimensions.X;
							double verticalPercent = (vertex.Position.Value.Y - min.Value.Y) / dimensions.Y;
							double depthPercent = (dimensions.Z != 0) ? (vertex.Position.Value.Z - min.Value.Z) / dimensions.Z : 1;
							vector.X = Math.Min(1, Math.Max(0, horizontalPercent));
							vector.Y = Math.Min(1, Math.Max(0, verticalPercent));
							vector.Z = Math.Min(1, Math.Max(0, depthPercent));
							vertex.Auxiliary["percent"] = vector;
							trace.CachedGeometry.Vertices[i] = vertex;
						}
					}
				}
			}
		}

		~TracedCharacter()
		{
			foreach (AutoTrace.Tracing trace in traces)
				trace.Image.Dispose();
			if (previewImage != null)
				previewImage.Dispose();
		}

		public void GetBoundaries(ref Vector3d? minimum, ref Vector3d? maximum)
		{
			Vector3d? compositeMinimum = null;
			Vector3d? compositeMaximum = null;
			foreach (AutoTrace.Tracing trace in Traces)
			{
				Vector3d thisMinimum = trace.CachedMinimum;
				thisMinimum.X += trace.XAdjustment;
				thisMinimum.Y += trace.YAdjustment;
				thisMinimum.Z += trace.ZAdjustment;
				Vector3d thisMaximum = trace.CachedMaximum;
				thisMaximum.X += trace.XAdjustment;
				thisMaximum.Y += trace.YAdjustment;
				thisMaximum.Z += trace.ZAdjustment;
				//
				compositeMinimum = (compositeMinimum == null) ? new Vector3d(thisMinimum) : Vector3d.ComponentMin(thisMinimum, compositeMinimum.Value);
				compositeMaximum = (compositeMaximum == null) ? new Vector3d(thisMaximum) : Vector3d.ComponentMax(thisMaximum, compositeMaximum.Value);
			}
			minimum = compositeMinimum;
			maximum = compositeMaximum;
		}

		public Vector3d GetDimensions()
		{
			Vector3d? minimum = null, maximum = null;
			GetBoundaries(ref minimum, ref maximum);
			if (minimum != null && maximum != null)
				return maximum.Value - minimum.Value;
			else
				return Vector3d.Zero;
		}

		public static TracedCharacter Factory(ulong id, BitmapVariant variant, BitmapColor color, bool useEffects = true, bool skipDetailedTracing = false)
		{
			Character character = variant.Characters[id];
			return TracedCharacter.Factory(character, variant, color, useEffects: useEffects, skipDetailedTracing: skipDetailedTracing);
		}

		public static OrderedDictionary GroupEffectsIntoTraceableQueues(Character character, BitmapVariant variant, BitmapColor bitmapColor, bool skipDetailedTracing = false)
		{
			Queue<BitmapEffect> effectQueue = new Queue<BitmapEffect>();
			// Queue the effects from the variant.
			if (variant != null)
				foreach (BitmapEffect effect in variant.Effects)
					effectQueue.Enqueue(effect);
			// List the effects from the color.
			List<BitmapEffect> coloredEffects = (bitmapColor != null) ? bitmapColor.Effects : null;
			// Obtain the dictionary.
			return GroupEffectsIntoTraceableQueues(character, effectQueue, coloredEffects, skipDetailedTracing: skipDetailedTracing);
		}

		public static OrderedDictionary GroupEffectsIntoTraceableQueues(Character character, Queue<BitmapEffect> queue, List<BitmapEffect> coloredEffects, bool skipDetailedTracing = false)
		{
			OrderedDictionary dictionary = new OrderedDictionary();
			// Track applicable color points.
			Tracer currentTracer = null;
			// Track items in current pass.
			EffectQueue currentQueue = new EffectQueue();
			Tracer previousKey = null;
			// Iterate the stack.
			if (queue.Count > 0)
			{
				while (queue.Count > 0)
				{
					BitmapEffect item = queue.Dequeue();
					if (item.shouldApplyEffectTo(character))
					{
						// Add effect to queue.
						currentQueue.Enqueue(item);
						// Do backwards lookup to specify that this stack needs to inherit the previous stack's delta.
						if (currentQueue.Count == 1 && previousKey != null && !item.StartsNewLayer)
						{
							EffectQueue effects = dictionary[previousKey] as EffectQueue;
							effects.ShouldNotDiscardDelta = true;
							effects.ShouldNotDiscardWidth = true;
						}
						// Determine ability to be traced. If it can be traced and skipping detailed tracing is requested, defer to a simple boundary tracing.
						if (item.Tracer != null)
							currentTracer = (!skipDetailedTracing) ? item.Tracer : new BitmapBoundaryTracer();
					}
					// Fall back to the boundary tracer if there are no more effects and this was all there was to do.
					if (queue.Count == 0 && currentQueue.Count > 0 && currentTracer == null && dictionary.Count == 0)
						currentTracer = new BitmapBoundaryTracer();
					// If this effect should be traced, consider it the point to add color to.
					if (currentTracer != null)
					{
						// Get count of effects from the variant.
						currentQueue.CountFromVariant = currentQueue.Count;
						// Add colors if available.
						if (coloredEffects != null)
							foreach (BitmapEffect coloredEffect in coloredEffects)
								if (coloredEffect.shouldApplyEffectTo(character))
									currentQueue.Enqueue(coloredEffect);
						// Add the effects as a pass.
						dictionary.Add(currentTracer, currentQueue);
						// Reset the tracking variables.
						currentQueue = new EffectQueue();
						previousKey = currentTracer;
						currentTracer = null;
					}
				}
				//
				if (dictionary.Count == 0 && currentQueue.Count == 0) {
					// There were no effects, so fall back to the base image.
					currentQueue.Enqueue(null);
					// Add colors if available.
					if (coloredEffects != null)
						foreach (BitmapEffect coloredEffect in coloredEffects)
							if (coloredEffect.shouldApplyEffectTo(character))
								currentQueue.Enqueue(coloredEffect);
					//
					dictionary.Add(new BitmapBoundaryTracer(), currentQueue);
				}
			}
			else {
				// There were no effects, so fall back to the base image.
				currentQueue.Enqueue(null);
				// Add colors if available.
				if (coloredEffects != null)
					foreach (BitmapEffect coloredEffect in coloredEffects)
						if (coloredEffect.shouldApplyEffectTo(character))
							currentQueue.Enqueue(coloredEffect);
				//
				dictionary.Add(new BitmapBoundaryTracer(), currentQueue);
			}
			return dictionary;
		}

		public static TracedCharacter Factory(Character character, BitmapVariant variant, BitmapColor color, bool useEffects = true, bool skipDetailedTracing = false)
		{
			DerivedCharacter offsetableUnicodeCharacter = new DerivedCharacter(character);
			// Prepare a place to store image-and-geometry traces.
			List<AutoTrace.Tracing> traces = new List<AutoTrace.Tracing>();
			// Prepare a place to store an image that may (or may not) represent the character (may be different because of 3d transformations).
			Bitmap previewImage = null;
			// Prepare place to store applicable stack.
			OrderedDictionary passes = new OrderedDictionary();
			if (variant != null)
			{
				// Get base image.
				ImageDescription baseImageDescription = variant.BaseImageProvider.GetImage(character, false);
				// Make applicable stack.
				if (useEffects)
					passes = GroupEffectsIntoTraceableQueues(character, variant, color, skipDetailedTracing: skipDetailedTracing);
				else
					passes.Add(new BitmapBoundaryTracer(), null);
				// If there's a base image, apply the effects to it.
				if (baseImageDescription != null)
				{
					Bitmap baseBitmap = baseImageDescription.SignificantBitmap;
					// Set the current image to be the base image.
					Bitmap currentImage = baseBitmap;
					// Track the running delta.
					OffsetableUnicodeCharacter delta = new OffsetableUnicodeCharacter(character, 0, 0, 0, 1f, 1f);
					// Track traces.
					Queue<TraceRequest> layersToComposite = new Queue<TraceRequest>();
					//
					float totalWidthDelta = 0;
					foreach (DictionaryEntry entry in passes)
					{
						Tracer tracer = entry.Key as Tracer;
						EffectQueue effects = entry.Value as EffectQueue;
						// Track how wide this layer thinks it is.
						float runningWidthDelta = 0;
						if (effects != null)
						{
							// Start looping through the effects in the pass.
							while (effects.Count > 0)
							{
								BitmapEffect effect = effects.Dequeue();
								if (effect != null)
								{
									// If this doesn't start a new layer, proceed normally. Otherwise, reset images as necessary.
									if (effect.StartsNewLayer)
									{
										// Reset the image.
										currentImage = (effect.ExpectsToStartFromBaseImage) ? (Bitmap)baseImageDescription.SignificantBitmap.Clone() : new Bitmap(1, 1);
										// Reset the change in location.
										delta = new OffsetableUnicodeCharacter(character, 0, 0, 0, 1f, 1f);
									}
									else
									{
										// Get image after effect has been processed, or start a new layer.
										currentImage = effect.Apply(currentImage, character: character, size: variant.Dependency);
										// Do offset pass (for use determining advance later; refered to as "width marker").
										delta = effect.Delta(delta);
									}
								}
							}
						}
						// Make a request to trace the image, push a copy of the current image and delta to the stack.
						layersToComposite = addImageAndDeltaAsLayer(currentImage, delta, tracer, layersToComposite);
						// Here, the delta is reset, due to the fact that the delta from earlier contributes to the final delta (in the form of the zero point).
						if (effects != null)
						{
							// Branch the delta.
							if (effects.ShouldNotDiscardDelta)
								delta = new OffsetableUnicodeCharacter(character, delta.OffsetWidth, delta.OffsetX, delta.OffsetY, delta.ScaleX, delta.ScaleY);
							else
							{
								if (effects.ShouldNotDiscardWidth)
									runningWidthDelta = delta.OffsetWidth;
								//
								delta = new OffsetableUnicodeCharacter(character, 0, 0, 0, 1f, 1f);
							}
						} else
							delta = new OffsetableUnicodeCharacter(character, delta.OffsetWidth, delta.OffsetX, delta.OffsetY, 1f, 1f);
						//
						totalWidthDelta = Math.Max(runningWidthDelta, totalWidthDelta);
					}
					// Trace everything out.
					traces = traceAllTraceRequests(new Queue<TraceRequest>(layersToComposite));
					// Flatten the queue into a single image/layer.
					previewImage = flattenQueueIntoSingleImage(new Queue<TraceRequest>(layersToComposite));
					// Get the zero point of the image, the point that represents: (0, 0).
					PointF zeroPoint = (previewImage != null) ? (PointF)previewImage.Tag : PointF.Empty;
					// Add the delta to the offsetable character.
					offsetableUnicodeCharacter = new DerivedCharacter(
						new Character(
							character,
							// Set at time of creation. Updated after by request. NOTE: This is a reporting feature and not a generation feature.
							character.OffsetWidth,
							character.OffsetX,
							character.OffsetY,
							character.ScaleX,
							character.ScaleY
						),
						new OffsetableUnicodeCharacter(
							character,
							// Width.
							delta.OffsetWidth + totalWidthDelta,
							// dx, dy
							(float)(-zeroPoint.X + delta.OffsetX + totalWidthDelta),
							(float)(-zeroPoint.Y + delta.OffsetY),
							// mdSx, mdSy
							delta.ScaleX,
							delta.ScaleY
						)
					);
					// Finish using the base image.
					if (currentImage != baseBitmap)
						baseImageDescription.Dispose();
				}
			}
			TracedCharacter tracedCharacter = new TracedCharacter(offsetableUnicodeCharacter, traces, previewImage);
			// Send the traces back.
			return tracedCharacter;
		}

		private static List<AutoTrace.Tracing> traceAllTraceRequests(Queue<TraceRequest> layersToComposite)
		{
			List<AutoTrace.Tracing> traces = new List<AutoTrace.Tracing>();
			// Trace each request.
			while (layersToComposite.Count > 0)
			{
				TraceRequest request = layersToComposite.Dequeue();
				traces.Add(
					request.GeometryProvider.GetGeometry(request.Image)
				);
			}
			return traces;
		}

		private static Bitmap flattenQueueIntoSingleImage(Queue<TraceRequest> layersToComposite)
		{
			// Compress the layers into a single image.
			Bitmap image = null;
			while (layersToComposite.Count > 0)
			{
				TraceRequest request = layersToComposite.Dequeue();
				Bitmap layer = request.Image;
				if (image == null)
					image = layer;
				else
				{
					using (Graphics g = Graphics.FromImage(image))
						g.DrawImage(layer, Point.Empty);
					image.Tag = layer.Tag;
				}
			}
			return image;
		}

		private static Queue<TraceRequest> addImageAndDeltaAsLayer(Bitmap currentImage, OffsetableUnicodeCharacter delta, ITracer tracer, Queue<TraceRequest> layersToComposite)
		{
			Bitmap thisImage = (currentImage.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined) ? (Bitmap)currentImage.Clone() : null;
			// If there's nothing else in the queue, put it into the queue. It becomes the form factor.
			if (layersToComposite.Count == 0)
			{
				// Determine changes to canvas size (re: against the original image itself).
				CanvasChangeAfterIncomingImageMetrics canvasMetrics = getChangesToCanvasGivenIncomingImage(PointF.Empty, thisImage.Size, thisImage.Size, delta);
				// Resize the image if necessary. Just set the zero-point if it's not necessary.
				if (canvasMetrics.SuggestsCanvasExpansion)
					thisImage = SupportFunctions.ResizeImageAndDiscardOldImage(canvasMetrics.CanvasSize, canvasMetrics.ZeroPoint, canvasMetrics.ImageLocation, thisImage);
				// Document the starting point on the outbound image.
				thisImage.Tag = canvasMetrics.ZeroPoint;
				// Add the image and trace to the queue.
				layersToComposite.Enqueue(
					new TraceRequest(thisImage, tracer)
				);
			}
			// However, if there are things already there, it's necessary to inspect them.
			else
			{
				TraceRequest exampleRequest = layersToComposite.Peek();
				Bitmap example = exampleRequest.Image;
				// Get the current canvas.
				Size currentCanvasSize = example.Size;
				PointF currentCanvasZeroPoint = (PointF)example.Tag;
				// Determine changes to canvas size.
				CanvasChangeAfterIncomingImageMetrics canvasMetrics = getChangesToCanvasGivenIncomingImage(currentCanvasZeroPoint, currentCanvasSize, thisImage.Size, delta);
				// Get an indexed copy of the layers.
				TraceRequest[] layerTraceRequests = layersToComposite.ToArray();
				// Canvas needs space if any of these is true.
				if (canvasMetrics.SuggestsCanvasExpansion)
				{
					// Redraw all the existing layers.
					for (int i = 0; i < layerTraceRequests.Length; i++)
					{
						layerTraceRequests[i].Image = SupportFunctions.ResizeImageAndDiscardOldImage(canvasMetrics.CanvasSize, PointF.Empty, canvasMetrics.ExistingImagesLocation, layerTraceRequests[i].Image);
						layerTraceRequests[i].Image.Tag = canvasMetrics.ZeroPoint;
					}
				}
				// Recreate the layers to composite using updated images if applicable.
				layersToComposite = new Queue<TraceRequest>(layerTraceRequests);
				// Resize the incoming image. This step is different from existing layers because it is drawn at a different offset.
				thisImage = SupportFunctions.ResizeImageAndDiscardOldImage(canvasMetrics.CanvasSize, canvasMetrics.ZeroPoint, canvasMetrics.ImageLocation, thisImage);
				// Document the starting point on the outbound image.
				thisImage.Tag = canvasMetrics.ZeroPoint;
				// Add value to layers.
				layersToComposite.Enqueue(
					new TraceRequest(thisImage, tracer)
				);
			}
			return layersToComposite;
		}

		private static CanvasChangeAfterIncomingImageMetrics getChangesToCanvasGivenIncomingImage(PointF originalCanvasZeroPoint, Size originalCanvasSize, Size incomingImageSize, OffsetableUnicodeCharacter incomingImageOffset)
		{
			// Calculate available space.
			double canvasMaxX = originalCanvasSize.Width - originalCanvasZeroPoint.X, canvasMinX = -(originalCanvasSize.Width - canvasMaxX);
			double canvasMaxY = originalCanvasSize.Height - originalCanvasZeroPoint.Y, canvasMinY = -(originalCanvasSize.Height - canvasMaxY);
			// Determine the starting point of the incoming drawing (determined from lower-left with +Y moving upwards).
			double hw = incomingImageOffset.OffsetWidth / 2.0;
			double x = Math.Floor(incomingImageOffset.OffsetX - hw);
			double y = incomingImageOffset.OffsetY;
			PointF drawIncomingImageAt = new PointF((float)x, (float)y);
			// Calculate space requirements.
			double minX = x, maxX = minX + incomingImageSize.Width;
			double minY = y, maxY = minY + incomingImageSize.Height;
			// Calculate native-language directives.
			bool leftNeedsSpace = (minX < canvasMinX);
			bool rightNeedsSpace = (maxX > canvasMaxX);
			bool upNeedsSpace = (maxY > canvasMaxY);
			bool downNeedsSpace = (minY < canvasMinY);
			bool suggestsCanvasExpansion = (leftNeedsSpace || rightNeedsSpace || upNeedsSpace || downNeedsSpace);
			// Prepare to track the new zero-point (though it may stay the same).
			PointF newZeroPoint = originalCanvasZeroPoint;
			Size newCanvasSize = originalCanvasSize;
			PointF deltaToNewZeroPoint = PointF.Empty;
			// Canvas needs space if any of these is true.
			if (suggestsCanvasExpansion)
			{
				// Determine what the net changes need to be.
				float addLeft = (leftNeedsSpace) ? (float)(canvasMinX - minX) : 0;
				float addRight = (rightNeedsSpace) ? (float)(maxX - canvasMaxX) : 0;
				float addUp = (upNeedsSpace) ? (float)(maxY - canvasMaxY) : 0;
				float addDown = (downNeedsSpace) ? (float)(canvasMinY - minY) : 0;
				// Determine the new zero point following such changes.
				newZeroPoint = new PointF(
					// If (and only if) the canvas needs to increase leftwards, increase the zero-point's horizontal location.
					originalCanvasZeroPoint.X + addLeft,
					// If (and only if) the canvas needs to increase downwards, increase the zero-point's vertical location.
					originalCanvasZeroPoint.Y + addDown
				);
				// Calculate the distance away from the new zero point to draw existing layers.
				deltaToNewZeroPoint = new PointF(
					newZeroPoint.X - originalCanvasZeroPoint.X,
					newZeroPoint.Y - originalCanvasZeroPoint.Y
				);
				// Calculate the new canvas size, which the new zero-point relies on.
				newCanvasSize = new Size(
					(int)(originalCanvasSize.Width + addLeft + addRight),
					(int)(originalCanvasSize.Height + addUp + addDown)
				);
			}
			return new CanvasChangeAfterIncomingImageMetrics(newZeroPoint, newCanvasSize, drawIncomingImageAt, deltaToNewZeroPoint, suggestsCanvasExpansion);
		}
	}
}
