using System.Collections.Generic;
using OpenTK;
using System.Drawing;

namespace FontTool
{
	public class DisplayList
	{
		protected ulong id;
		protected DerivedCharacter character;
		protected List<ImageDescription> descriptions;
		protected List<IVertexBuffer> vertexBuffers;
		protected Vector3d dimensions;
		protected Vector3d offset;
		//
		protected Vector3d? minimum = Vector3d.Zero, maximum = Vector3d.Zero;
		// Cached data.
		protected Bitmap cachedPreviewImage = null;

		public ulong Id
		{
			get { return id; }
			set { id = value; }
		}

		public DerivedCharacter Character
		{
			get { return character; }
			set { character = value; }
		}

		public List<ImageDescription> ImageDescriptions
		{
			get { return descriptions; }
			set { descriptions = value; }
		}

		public List<IVertexBuffer> VertexBuffers
		{
			get { return vertexBuffers; }
			set { vertexBuffers = value; }
		}

		public Vector3d Dimensions
		{
			get { return dimensions; }
			set { dimensions = value; }
		}

		public Vector3d Offset
		{
			get { return offset; }
			set { offset = value; }
		}

		public Bitmap CachedPreviewImage
		{
			get { return cachedPreviewImage; }
			set { cachedPreviewImage = value; }
		}

		public Vector3d? Minimum
		{
			get { return minimum; }
			set { minimum = value; }
		}

		public Vector3d? Maximum
		{
			get { return maximum; }
			set { maximum = value; }
		}

		public DisplayList(ulong id, Vector3d dimensions, Bitmap previewImage = null)
		{
			this.id = id;
			this.descriptions = new List<ImageDescription>();
			this.vertexBuffers = new List<IVertexBuffer>();
			this.dimensions = dimensions;
			this.cachedPreviewImage = previewImage;
		}

		public DisplayList(TracedCharacter tracedCharacter, bool pushToGraphicsContext = false, bool calculateSimplifications = false, Bitmap previewImage = null, OpenGLConfiguration oglConfiguration = null)
			: this(tracedCharacter.Id, tracedCharacter.GetDimensions(), previewImage: previewImage)
		{
			if (pushToGraphicsContext && oglConfiguration == null)
				oglConfiguration = new OpenGLConfiguration();
			// Get minimum and maximum points. Necessary for creation of rectangles for virtual keyboard.
			tracedCharacter.GetBoundaries(ref minimum, ref maximum);
			//
			this.character = tracedCharacter.DerivedUnicodeCharacter;
			// Direct the origin point to be the lower-left of the drawing (its final offset).
			this.offset = new Vector3d(
				tracedCharacter.DerivedUnicodeCharacter.FinalOffset.OffsetX,
				tracedCharacter.DerivedUnicodeCharacter.FinalOffset.OffsetY,
				0
			);
			// Create texture and geometry for each trace.
			foreach (AutoTrace.Tracing trace in tracedCharacter.Traces)
			{
				int thisIndex = tracedCharacter.Traces.IndexOf(trace);
				string nameOfTrace = string.Format(
					"{0}{1}",
					new object[] {
						tracedCharacter.DerivedUnicodeCharacter.Name,
						(thisIndex > 0) ? string.Format(" {0}", thisIndex) : ""
					}
				);
				// Create texture.
				ImageDescription description = new ImageDescription(id, bitmap: trace.Image, pushToContext: pushToGraphicsContext);
				// Create name in format: Primary Unicode Name (or) Primary Unicode Name n
				description.Name = nameOfTrace;
				descriptions.Add(description);
				// Create geometry.
				if (pushToGraphicsContext)
				{
					//
					Vector3d textureScale = new Vector3d(
						description.TextureScale.X * 1 / description.SignificantWidth,
						description.TextureScale.Y * 1 / description.SignificantHeight, 1
					);
					Vector3d textureOffset = new Vector3d(
						description.TextureScale.X * trace.CachedGlobalTextureOffset.X,
						description.TextureScale.Y * trace.CachedGlobalTextureOffset.Y,
						1 * trace.CachedGlobalTextureOffset.Z
					);
					//
					List<BufferedVertex> vertices = new List<BufferedVertex>();
					// Create out-bound vertices.
					foreach (Trace.Outbound.Vertex vertex in trace.CachedGeometry.TrianglesAsVerticesWithFaceNormals)
					{
						BufferedVertex thisVertex = new BufferedVertex();
						thisVertex.TexCoord = (Vector2)new Vector2d(vertex.UVW.Value.X, vertex.UVW.Value.Y);
						thisVertex.Normal = (Vector3)vertex.Normal.Value;
						thisVertex.Position = (Vector3)vertex.Position.Value;
						// Store the vertex.
						vertices.Add(thisVertex);
					}
					// Create the vertex buffer (on GPU only).
					vertexBuffers.Add(new VertexBufferOnGPU(nameOfTrace, vertices.ToArray(), Vector3d.Zero, oglConfiguration, textureScale: textureScale, textureOffset: textureOffset));
				}
				else
				{
					//
					Vector3d textureScale = new Vector3d(1 / description.SignificantWidth, 1 / description.SignificantHeight, 1);
					Vector3d textureOffset = trace.CachedGlobalTextureOffset;
					//
					List<BufferedVertexWithAuxiliaryData> vertices = new List<BufferedVertexWithAuxiliaryData>();
					// Create centered, out-bound vertices.
					foreach (Trace.Outbound.Vertex vertex in trace.CachedGeometry.TrianglesAsVerticesWithFaceNormals)
					{
						BufferedVertexWithAuxiliaryData thisVertex = new BufferedVertexWithAuxiliaryData();
						thisVertex.TexCoord = (Vector2)new Vector2d(vertex.UVW.Value.X, vertex.UVW.Value.Y);
						thisVertex.Normal = (Vector3)vertex.Normal.Value;
						thisVertex.Position = (Vector3)vertex.Position.Value;
						// Add in data for animations.
						thisVertex.Auxiliary = vertex.Auxiliary;
						// Store the vertex.
						vertices.Add(thisVertex);
					}
					// Create the vertex buffer (in system memory only).
					vertexBuffers.Add(new VertexBuffer(nameOfTrace, vertices, Vector3d.Zero, textureScale: textureScale, textureOffset: textureOffset, calculateSimplifications: calculateSimplifications));
				}
			}
		}

		// Get texture entries.
		public List<string> getTextureEntries(Plugins.Configuration configuration, string relativeBasePath = null, bool flipped = false)
		{
			List<string> list = new List<string>();
			foreach (ImageDescription description in descriptions)
				list.Add(description.getTextureEntry(configuration, relativeBasePath: relativeBasePath, flipped: flipped));
			//
			return list;
		}

		public void exportTextures(string relativeBasePath = null)
		{
			// Export each texture.
			foreach (ImageDescription description in descriptions)
			{
				string filename = string.Format("{0}.png", description.Name);
				string path = (relativeBasePath != null) ? System.IO.Path.Combine(relativeBasePath, filename) : filename;
				description.Save(path);
			}
		}

		public ImageDescription exportTexturesAsSingleTexture(string name, List<Vector3d> textureOffsets, string relativeBasePath = null)
		{
			// Create a single texture to contain the other textures.
			int width = 0;
			int maxHeight = 0;
			foreach (ImageDescription description in descriptions)
			{
				width += (int)System.Math.Ceiling(description.SignificantWidth);
				maxHeight = (int)System.Math.Ceiling(System.Math.Max(maxHeight, description.SignificantHeight));
			}
			// Composite the image.
			if (width != 0 && maxHeight != 0)
			{
				Bitmap bitmap = new Bitmap(width, maxHeight);
				int runningX = 0;
				// Draw each texture.
				foreach (ImageDescription description in descriptions)
				{
					textureOffsets.Add(
						new Vector3d(runningX, 0, 0)
					);
					using (Graphics g = Graphics.FromImage(bitmap))
					{
						g.DrawImage(description.SignificantBitmap, new Point(runningX, 0));
					}
					runningX += (int)System.Math.Ceiling(description.SignificantWidth);
				}
				// Save image.
				ImageDescription compositeDescription = new ImageDescription(0, bitmap: bitmap, pushToContext: false);
				compositeDescription.Name = name;
				string filename = string.Format("{0}.png", compositeDescription.Name);
				string path = (relativeBasePath != null) ? System.IO.Path.Combine(relativeBasePath, filename) : filename;
				compositeDescription.Save(path);
				return compositeDescription;
			}
			else
				return null;
		}

		public List<string> getTextureNames()
		{
			List<string> list = new List<string>();
			foreach (ImageDescription description in descriptions)
			{
				list.Add(description.SafeName);
			}
			//
			return list;
		}

		// Get group with embedded vertex pool.
		public string getPolygonEntries(List<string> vertexPoolNames, List<string> textureNames, Vector3d screenScale, int indentation = 0, bool useIndividualGroups = false, List<int> vertexOffsets = null, bool treatAsSingleVertexPool = false)
		{
			// Realize indentation.
			string indentationString = "";
			for (int i = 0; i < indentation; i++)
				indentationString += "\t";
			string polygonIndentationString = (useIndividualGroups) ? indentationString + "\t" : indentationString;
			// Make a list to redirect vertex numbers.
			if (vertexOffsets == null || vertexOffsets.Count == 0)
				vertexOffsets = new List<int> { 0 };
			if (textureNames == null || textureNames.Count == 0)
				textureNames = new List<string> { null };
			// If there are actually vertex buffers and those vertex buffers are not GPU-only, describe them.
			if (vertexBuffers.Count > 0 && vertexBuffers.TrueForAll(item => !(item is VertexBufferOnGPU)))
			{
				// Make a place to store the individual parts of the vertex pool entry.
				List<string> entries = new List<string>();
				//
				foreach (VertexBuffer vertexBuffer in vertexBuffers)
				{
					//
					int thisIndex = vertexBuffers.IndexOf(vertexBuffer);
					string textureName = textureNames[thisIndex % textureNames.Count];
					string textureReferenceEntry = (textureName != null) ? polygonIndentationString + "	<TRef> { " + textureName + " }" : null;
					string vertexPoolName = (treatAsSingleVertexPool) ? vertexPoolNames[0] : vertexPoolNames[thisIndex % vertexPoolNames.Count];
					int vertexOffset = vertexOffsets[thisIndex % vertexOffsets.Count];
					// Group the triangles.
					if (useIndividualGroups)
						entries.Add(
							string.Format(
								"{0}<Group> Trace{1} {{",
								new object[] {
									indentationString, thisIndex + 1
								}
							)
						);
					// Iterate triangles.
					for (int i = 0; i < vertexBuffer.Data.Length; i = i + 3)
					{
						// Generate 1 Polygon per triangle (NOTE: this is especially verbose).
						entries.Add(polygonIndentationString + "<Polygon> {");
						if (textureReferenceEntry != null)
							entries.Add(textureReferenceEntry);
						// Ignore face normals here. Realized on vertices already.
						int v1 = i, v2 = i + 1, v3 = i + 2;
						// Replace the vertex indices with a more appropriate version.
						if (vertexBuffer.Simplifications.ContainsKey(v1))
							v1 = vertexBuffer.Simplifications[v1];
						if (vertexBuffer.Simplifications.ContainsKey(v2))
							v2 = vertexBuffer.Simplifications[v2];
						if (vertexBuffer.Simplifications.ContainsKey(v3))
							v3 = vertexBuffer.Simplifications[v3];
						//
						v1 += 1 + vertexOffset;
						v2 += 1 + vertexOffset;
						v3 += 1 + vertexOffset;
						//
						entries.Add(
							string.Format(
								// Example: <VertexRef> { 1 2 3 <Ref> { vertexPoolName } }
								"{0}	<VertexRef> {{ {1} {2} {3} <Ref> {{ {4} }} }}",
								new object[] {
									polygonIndentationString, v1, v2, v3, vertexPoolName
								}
							)
						);
						entries.Add(polygonIndentationString + "}");
					}
					// End the group.
					if (useIndividualGroups)
						entries.Add(indentationString + "}");
				}
				//
				return string.Join("\n", entries.ToArray());
			}
			else
				return "";
		}

		public string getEGGVertexPools(string baseName, int indentation = 0, Vector3d? screenScale = null)
		{
			// Realize indentation.
			string indentationString = "";
			for (int i = 0; i < indentation; i++)
				indentationString += "\t";
			// Realize scale, if applicable.
			if (screenScale == null)
				screenScale = Vector3d.One;
			// Make a place to store the individual parts of the vertex pool entry.
			List<string> entries = new List<string>();
			List<string> vertexPoolNames = SupportFunctions.GetRangeOfNames(baseName, vertexBuffers.Count);
			//
			foreach (VertexBuffer vertexBuffer in vertexBuffers)
			{
				bool noSimplifications = (vertexBuffer.Simplifications.Count == 0);
				//
				int thisIndex = vertexBuffers.IndexOf(vertexBuffer);
				string thisVertexPoolName = vertexPoolNames[thisIndex];
				// Realize global texture offset, if applicable.
				Vector3d thisGlobalTextureOffset = vertexBuffer.TextureOffset;
				// Begin the entry.
				entries.Add(indentationString + "<VertexPool> " + thisVertexPoolName + " {");
				// Vertex entries.
				for (int i = 0; i < vertexBuffer.Length; i++)
				{
					// Only add the vertex if it cannot be simplified.
					if (noSimplifications || vertexBuffer.Simplifications[i] == i)
					{
						//
						BufferedVertexWithAuxiliaryData thisVertex = vertexBuffer.Data[i];
						// Make a place to store the individual parts of the vertex entry.
						List<string> entryParts = new List<string>();
						// Position attribute. Position: <Vertex> n { X Y Z ... }
						entryParts.Add(indentationString + "	<Vertex> " + (i + 1) + " {");
						// EGG vertices are stored in global coordinates. NOTE: it's a waste of time to try to alter that.
						double x = (Offset.X + vertexBuffer.Offset.X + thisVertex.Position.X) * screenScale.Value.X;
						double y = (Offset.Y + vertexBuffer.Offset.Y + thisVertex.Position.Y) * screenScale.Value.Y;
						double z = (Offset.Z + vertexBuffer.Offset.Z + thisVertex.Position.Z) * screenScale.Value.Z;
						entryParts.Add(x + " " + y + " " + z);
						// Normal attribute. Normal: <Normal> { X Y Z }
						entryParts.Add("<Normal> {");
						entryParts.Add(thisVertex.Normal.X + " " + thisVertex.Normal.Y + " " + thisVertex.Normal.Z);
						entryParts.Add("}");
						// UV attribute. UV (in pixels): <UV> { U V }
						double u = thisGlobalTextureOffset.X + thisVertex.TexCoord.X;
						double v = thisGlobalTextureOffset.Y + thisVertex.TexCoord.Y;
						double w = 0;
						entryParts.Add("<UV> {");
						entryParts.Add((w != 0) ? u + " " + v + " " + w : u + " " + v);
						entryParts.Add("}");
						// AUX attribute. Add all auxiliary data (like horizontal percent).
						foreach (string auxiliaryKey in thisVertex.Auxiliary.Keys)
						{
							Vector4d data = thisVertex.Auxiliary[auxiliaryKey];
							entryParts.Add("<AUX> " + auxiliaryKey + " {");
							entryParts.Add(data.X + " " + data.Y + " " + data.Z + " " + data.W);
							entryParts.Add("}");
						}
						entryParts.Add("}");
						// Add the entry: <Vertex> number { x [y [z [w]]] [attributes] }
						entries.Add(string.Join(" ", entryParts.ToArray()));
					}
				}
				entries.Add(indentationString + "}");
			}
			// Send the <VertexPool> entry back.
			return string.Join("\n", entries.ToArray());
		}

		public string getSingleEGGVertexPool(string baseName, List<int> outboundVertexOffsets, List<Vector3d> textureOffsets = null, int indentation = 0, Vector3d? screenScale = null)
		{
			// Realize indentation.
			string indentationString = "";
			for (int i = 0; i < indentation; i++)
				indentationString += "\t";
			// Realize scale, if applicable.
			if (screenScale == null)
				screenScale = Vector3d.One;
			// Make a place to store the individual parts of the vertex pool entry.
			List<string> entries = new List<string>();
			List<string> vertexPoolNames = SupportFunctions.GetRangeOfNames(baseName, vertexBuffers.Count);
			if (textureOffsets == null || textureOffsets.Count == 0)
				textureOffsets = new List<Vector3d> { Vector3d.Zero };
			//
			int vertexOffset = 0;
			outboundVertexOffsets.Add(0);
			string thisVertexPoolName = baseName;
			// Begin the entry.
			entries.Add(indentationString + "<VertexPool> " + thisVertexPoolName + " {");
			foreach (VertexBuffer vertexBuffer in vertexBuffers)
			{
				bool noSimplifications = (vertexBuffer.Simplifications.Count == 0);
				//
				int thisIndex = vertexBuffers.IndexOf(vertexBuffer);
				// Get offset in container image (if there is one).
				Vector3d thisTextureOffset = textureOffsets[thisIndex % textureOffsets.Count];
				// Realize global texture offset, if applicable.
				Vector3d thisGlobalTextureOffset = thisTextureOffset + vertexBuffer.TextureOffset;
				// Vertex entries.
				for (int i = 0; i < vertexBuffer.Length; i++)
				{
					// Only add the vertex if it cannot be simplified.
					if (noSimplifications || vertexBuffer.Simplifications[i] == i)
					{
						//
						BufferedVertexWithAuxiliaryData thisVertex = vertexBuffer.Data[i];
						// Make a place to store the individual parts of the vertex entry.
						List<string> entryParts = new List<string>();
						// Position attribute. Position: <Vertex> n { X Y Z ... }
						entryParts.Add(indentationString + "	<Vertex> " + (vertexOffset + i + 1) + " {");
						// EGG vertices are stored in global coordinates. NOTE: it's a waste of time to try to alter that.
						double x = (Offset.X + vertexBuffer.Offset.X + thisVertex.Position.X) * screenScale.Value.X;
						double y = (Offset.Y + vertexBuffer.Offset.Y + thisVertex.Position.Y) * screenScale.Value.Y;
						double z = (Offset.Z + vertexBuffer.Offset.Z + thisVertex.Position.Z) * screenScale.Value.Z;
						entryParts.Add(x + " " + y + " " + z);
						// Normal attribute. Normal: <Normal> { X Y Z }
						entryParts.Add("<Normal> {");
						entryParts.Add(thisVertex.Normal.X + " " + thisVertex.Normal.Y + " " + thisVertex.Normal.Z);
						entryParts.Add("}");
						// UV attribute. UV (in pixels): <UV> { U V }
						double u = thisGlobalTextureOffset.X + thisVertex.TexCoord.X;
						double v = thisGlobalTextureOffset.Y + thisVertex.TexCoord.Y;
						double w = 0;
						entryParts.Add("<UV> {");
						entryParts.Add((w != 0) ? u + " " + v + " " + w : u + " " + v);
						entryParts.Add("}");
						// AUX attribute. Add all auxiliary data (like horizontal percent).
						foreach (string auxiliaryKey in thisVertex.Auxiliary.Keys)
						{
							Vector4d data = thisVertex.Auxiliary[auxiliaryKey];
							entryParts.Add("<AUX> " + auxiliaryKey + " {");
							entryParts.Add(data.X + " " + data.Y + " " + data.Z + " " + data.W);
							entryParts.Add("}");
						}
						entryParts.Add("}");
						// Add the entry: <Vertex> number { x [y [z [w]]] [attributes] }
						entries.Add(string.Join(" ", entryParts.ToArray()));
					}
				}
				vertexOffset += vertexBuffer.Length;
				outboundVertexOffsets.Add(vertexOffset);
			}
			entries.Add(indentationString + "}");
			// Send the <VertexPool> entry back.
			return string.Join("\n", entries.ToArray());
		}
	}
}
