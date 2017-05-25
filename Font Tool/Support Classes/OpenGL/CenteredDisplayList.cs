using System.Collections.Generic;
using OpenTK;

namespace FontTool
{
	public class CenteredDisplayList : DisplayList
	{
		public CenteredDisplayList(TracedCharacter tracedCharacter, bool pushToGraphicsContext = false, bool calculateSimplifications = false, System.Drawing.Bitmap previewImage = null, OpenGLConfiguration oglConfiguration = null)
			: base(tracedCharacter.Id, tracedCharacter.GetDimensions(), previewImage: previewImage)
		{
			if (pushToGraphicsContext && oglConfiguration == null)
				oglConfiguration = new OpenGLConfiguration();
			//
			this.Character = tracedCharacter.DerivedUnicodeCharacter;
			//
			Vector3d? minimum = null, maximum = null;
			tracedCharacter.GetBoundaries(ref minimum, ref maximum);
			if (this.dimensions.Length != 0)
			{
				// Get center of object (i.e. ignore the final offset).
				Vector3d centerOfWholeObjectInGlobalCoordinates = new Vector3d(
					minimum.Value.X + dimensions.X / 2.0,
					minimum.Value.Y + dimensions.Y / 2.0,
					0
				);
				// Get center of letter (i.e. apply the final offset). This is the center of the geometry in the letter.
				Vector3d centerOfWholeLetterInGlobalCoordinates = new Vector3d(
					tracedCharacter.DerivedUnicodeCharacter.FinalOffset.OffsetX + centerOfWholeObjectInGlobalCoordinates.X,
					tracedCharacter.DerivedUnicodeCharacter.FinalOffset.OffsetY + centerOfWholeObjectInGlobalCoordinates.Y,
					0
				);
				this.offset = centerOfWholeLetterInGlobalCoordinates;
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
					// Now, calculate the center of this trace.
					Vector3d centerOfLocalObjectInGlobalCoordinates = new Vector3d(
						trace.CachedMinimum.X + trace.HalfWidth,
						trace.CachedMinimum.Y + trace.HalfHeight,
						0
					);
					// Then, calculate the difference between the center of this trace and the center of the whole object (not the letter). This represents the amount of distance to move so that the local object is locally centered.
					Vector3d localDelta = centerOfLocalObjectInGlobalCoordinates - centerOfWholeObjectInGlobalCoordinates;
					// Create texture.
					ImageDescription description = new ImageDescription(tracedCharacter.Id, bitmap: trace.Image, pushToContext: pushToGraphicsContext);
					description.Name = nameOfTrace;
					descriptions.Add(description);
					// Create geometry.
					if (pushToGraphicsContext)
					{
						//
						Vector3d textureScale = new Vector3d(
							description.TextureScale.X * 1 / description.Width,
							description.TextureScale.Y * 1 / description.Height, 1
						);
						Vector3d textureOffset = new Vector3d(
							description.TextureScale.X * trace.CachedGlobalTextureOffset.X,
							description.TextureScale.Y * trace.CachedGlobalTextureOffset.Y,
							1 * trace.CachedGlobalTextureOffset.Z
						);
						//
						List<BufferedVertex> vertices = new List<BufferedVertex>();
						// Create centered, out-bound vertices.
						foreach (Trace.Outbound.Vertex vertex in trace.CachedGeometry.TrianglesAsVerticesWithFaceNormals)
						{
							BufferedVertex thisVertex = new BufferedVertex();
							thisVertex.TexCoord = (Vector2)new Vector2d(vertex.UVW.Value.X, vertex.UVW.Value.Y);
							thisVertex.Normal = (Vector3)vertex.Normal.Value;
							// Move local coordinates so that they center around their own (0, 0, 0). NOTE: Z is done by default.
							thisVertex.Position = (Vector3)(vertex.Position.Value - centerOfLocalObjectInGlobalCoordinates);
							// Store the vertex.
							vertices.Add(thisVertex);
						}
						// Create the vertex buffer (on GPU only).
						vertexBuffers.Add(new VertexBufferOnGPU(nameOfTrace, vertices.ToArray(), localDelta, oglConfiguration, textureScale: textureScale, textureOffset: textureOffset));
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
							// Move local coordinates so that they center around their own (0, 0, 0). NOTE: Z is done by default.
							thisVertex.Position = (Vector3)(vertex.Position.Value - centerOfWholeObjectInGlobalCoordinates);
							// Add in data for animations.
							thisVertex.Auxiliary = vertex.Auxiliary;
							// Store the vertex.
							vertices.Add(thisVertex);
						}
						// Create the vertex buffer (in system memory only).
						vertexBuffers.Add(new VertexBuffer(nameOfTrace, vertices, localDelta, textureScale: textureScale, textureOffset: textureOffset, calculateSimplifications: calculateSimplifications));
					}
				}
			}
		}
	}
}
