using System.Drawing;
using TriangleNet.Meshing;
using System.Collections.Generic;
using OpenTK;
using System;

namespace AutoTrace
{
	public class Tracing
	{
		private Bitmap image;
		private List<IMesh> meshes;
		private PointF localTextureOffset = Point.Empty;
		private double solidification = 1;
		private bool invertedSolid = false;
		private double xAdjustment = 0;
		private double yAdjustment = 0;
		private double zAdjustment = 0;
		private bool fillEdges = true;

		public Bitmap Image
		{
			get { return image; }
			set { image = value; }
		}

		public int ImageHeight
		{
			get { return (Image != null) ? Image.Height : 0; }
		}

		public List<IMesh> Meshes
		{
			get { return meshes; }
			set { meshes = value; }
		}

		public PointF LocalTextureOffset
		{
			get { return localTextureOffset; }
			set { localTextureOffset = value; }
		}

		public double Solidification
		{
			get { return solidification; }
			set { solidification = value; }
		}

		public bool InvertedSolid
		{
			get { return invertedSolid; }
			set { invertedSolid = value; }
		}

		public double XAdjustment
		{
			get { return xAdjustment; }
			set { xAdjustment = value; }
		}

		public double YAdjustment
		{
			get { return yAdjustment; }
			set { yAdjustment = value; }
		}

		public double ZAdjustment
		{
			get { return zAdjustment; }
			set { zAdjustment = value; }
		}

		public bool FillEdges
		{
			get { return fillEdges; }
			set { fillEdges = value; }
		}

		// Metrics needed for conversion.
		public double HalfSolidification
		{
			get { return Solidification / 2.0; }
		}

		// Derived globals.
		private Vector3d cachedDimensions = Vector3d.Zero;
		private Vector3d cachedMinimum = Vector3d.Zero;
		private Vector3d cachedMaximum = Vector3d.Zero;
		FontTool.Trace.Outbound.TrianglesCollection cachedGeometry = new FontTool.Trace.Outbound.TrianglesCollection();
		private double halfWidth;
		private double halfHeight;

		public Vector3d CachedDimensions
		{
			get { return cachedDimensions; }
			set { cachedDimensions = value; }
		}

		public Vector3d CachedMinimum
		{
			get { return cachedMinimum; }
			set { cachedMinimum = value; }
		}

		public Vector3d CachedMaximum
		{
			get { return cachedMaximum; }
			set { cachedMaximum = value; }
		}

		public FontTool.Trace.Outbound.TrianglesCollection CachedGeometry
		{
			get { return cachedGeometry; }
			set { cachedGeometry = value; }
		}

		public double HalfWidth
		{
			get { return halfWidth; }
			set { halfWidth = value; }
		}

		public double HalfHeight
		{
			get { return halfHeight; }
			set { halfHeight = value; }
		}

		// Set globals.
		private Vector3d cachedGlobalTextureOffset = Vector3d.Zero;

		public Vector3d CachedGlobalTextureOffset
		{
			get { return cachedGlobalTextureOffset; }
			set { cachedGlobalTextureOffset = value; }
		}

		public Tracing(Bitmap image) : this(image, new List<IMesh>(), Point.Empty, 0, false, 0, 0, 0, false) { }

		public Tracing(Bitmap image, IMesh mesh) : this(image, new List<IMesh> { mesh }, Point.Empty, 0, false, 0, 0, 0, false) { }

		public Tracing(Bitmap image, List<IMesh> meshes) : this(image, meshes, Point.Empty, 0, false, 0, 0, 0, false) { }

		public Tracing(Bitmap image, List<IMesh> meshes, PointF textureOffset, double solidification, bool invertedSolid, double xAdjustment, double yAdjustment, double zAdjustment, bool fillEdges)
		{
			this.image = image;
			this.meshes = meshes;
			this.localTextureOffset = textureOffset;
			this.solidification = solidification;
			this.invertedSolid = invertedSolid;
			this.xAdjustment = xAdjustment;
			this.yAdjustment = yAdjustment;
			this.zAdjustment = zAdjustment;
			this.fillEdges = fillEdges;
			//
			cacheDimensions();
			cacheGeometry();
		}

		public void cacheDimensions()
		{
			// Track the components for use with percentages.
			double? minX = null, minY = null, maxX = null, maxY = null;
			List<TriangleNet.Geometry.Vertex> localVertices = new List<TriangleNet.Geometry.Vertex>();
			foreach (var mesh in Meshes)
			{
				foreach (var vertex in mesh.Vertices)
				{
					// Add to tracking list for triangle pass.
					if (minX == null || minX > vertex.x)
						minX = vertex.x;
					if (minY == null || minY > vertex.y)
						minY = vertex.y;
					if (maxX == null || maxX < vertex.x)
						maxX = vertex.x;
					if (maxY == null || maxY < vertex.y)
						maxY = vertex.y;
				}
			}
			// Set the dimensions.
			this.cachedDimensions = new Vector3d(
				(minX != null && maxX != null) ? maxX.Value - minX.Value : 0,
				(minY != null && maxY != null) ? maxY.Value - minY.Value : 0,
				Solidification
			);
			this.cachedMinimum = new Vector3d(
				(minX != null) ? minX.Value : 0,
				(minY != null) ? minY.Value : 0,
				-Solidification / 2.0
			);
			this.cachedMaximum = new Vector3d(
				(maxX != null) ? maxX.Value : 0,
				(maxY != null) ? maxY.Value : 0,
				Solidification / 2.0
			);
			// For convenience later, pre-calculate half width and height of the dimensions (used outside this class for centering).
			this.halfWidth = this.cachedDimensions.X / 2.0;
			this.halfHeight = this.cachedDimensions.Y / 2.0;
		}

		public void cacheGeometry()
		{
			// Draw geometry to the upper-right of (0, 0, 0).
			List<FontTool.Trace.Outbound.Vertex> outboundVertices = new List<FontTool.Trace.Outbound.Vertex>();
			FontTool.Trace.Outbound.TrianglesCollection triangles = new FontTool.Trace.Outbound.TrianglesCollection();
			// Read in all the vertices generated by the tracing.
			List<TriangleNet.Geometry.Vertex> localVertices = new List<TriangleNet.Geometry.Vertex>();
			foreach (var mesh in Meshes)
			{
				foreach (var vertex in mesh.Vertices)
				{
					// Add to tracking list for triangle pass.
					localVertices.Add(vertex);
				}
			}
			// Count the total vertices for this trace.
			int localVertexCount = localVertices.Count;
			//
			int passes = (Solidification != 0) ? 2 : 1;
			bool edgesShouldBeFilled = (passes > 1 && FillEdges) ? true : false;
			// Add vertices such that local vertices are continuous. If soldified, append mirrored copies after the local vertices.
			for (int pass = 0; pass < passes; pass++)
			{
				int verticalModifier = (pass % 2 == 0) ? 1 : -1;
				foreach (var vertex in localVertices)
				{
					FontTool.Trace.Outbound.Vertex thisVertex = getDerivedVertex(verticalModifier, vertex);
					// Add vertex representing local geometry only.
					outboundVertices.Add(thisVertex);
				}
			}
			// Push the vertex references into polygons.
			foreach (var mesh in Meshes)
			{
				foreach (var triangle in mesh.Triangles)
				{
					// Track directional incidence of neighboring triangles (i.e. +x, +y, -x, -y; z is ignored).
					Vector3d n0Incidence = Vector3d.Zero, n1Incidence = Vector3d.Zero, n2Incidence = Vector3d.Zero;
					if (edgesShouldBeFilled)
						CalculateDirectionalVectors(triangle, ref n0Incidence, ref n1Incidence, ref n2Incidence);
					// Write the faces out in passes: (1) top-facing and (2) bottom-facing (optional).
					for (int r = 0; r < passes; r++)
					{
						List<int> verticesInFace = new List<int>();
						// Pick between original face and solidified face.
						int vertexOffset = r * localVertexCount + 1;
						// Add vertex indices representing the face.
						for (int i = 0; i < triangle.vertices.Length; i++)
						{
							int vertexId = vertexOffset + localVertices.IndexOf(triangle.vertices[i]);
							verticesInFace.Add(vertexId);
						}
						// Create the data entry.
						bool reversed = (r > 0) ? !InvertedSolid : InvertedSolid;
						Vector3d normal = (reversed) ? -Vector3d.UnitZ : Vector3d.UnitZ;
						// Add the face (reversed if necessary).
						addTriangleToTriangleCollection(verticesInFace, normal, reversed, outboundVertices, triangles);
					}
					// Optionally, fill the space created between the top-facing and bottom-facing sub-meshes.
					if (edgesShouldBeFilled)
					{
						// If certain edges lack neighbors, use that information to fill the edge.
						if (triangle.N0 == -1)
						{
							Vector3d normal = (!InvertedSolid) ? n0Incidence : -1 * n0Incidence;
							addQuadrilateralGivenThreePoints(1, 2, -1, triangle, normal, localVertices, outboundVertices, triangles);
						}
						if (triangle.N1 == -1)
						{
							Vector3d normal = (!InvertedSolid) ? n1Incidence : -1 * n1Incidence;
							addQuadrilateralGivenThreePoints(2, 0.01, -2, triangle, normal, localVertices, outboundVertices, triangles);
						}
						if (triangle.N2 == -1)
						{
							Vector3d normal = (!InvertedSolid) ? n2Incidence : -1 * n2Incidence;
							addQuadrilateralGivenThreePoints(0.01, 1, -0.01, triangle, normal, localVertices, outboundVertices, triangles);
						}
					}
				}
			}
			// Cache the triangle collection (includes references to derived vertices).
			CachedGeometry = triangles;
		}

		private static void CalculateDirectionalVectors(TriangleNet.Data.Triangle triangle, ref Vector3d n0Incidence, ref Vector3d n1Incidence, ref Vector3d n2Incidence, bool forceCalculation = false)
		{
			TriangleNet.Geometry.Vertex v0 = triangle.vertices[0];
			TriangleNet.Geometry.Vertex v1 = triangle.vertices[1];
			TriangleNet.Geometry.Vertex v2 = triangle.vertices[2];
			// Given a triangle called (P0, P1, P2) and the lack of presence of a specific neighbor, pick 2 points to find a vector, v, that is perpendicular to that line, moving counter-clockwise.
			if (forceCalculation || triangle.N0 == -1)
			{
				// Find vector perpendicular to line created by p1 to p2.
				n0Incidence = new Vector3d(v2.Y - v1.Y, -(v2.X - v1.X), 0);
				n0Incidence.Normalize();
			}
			if (forceCalculation || triangle.N1 == -1)
			{
				// Find vector perpendicular to line created by p2 to p0.
				n1Incidence = new Vector3d(v0.Y - v2.Y, -(v0.X - v2.X), 0);
				n1Incidence.Normalize();
			}
			if (forceCalculation || triangle.N2 == -1)
			{
				// Find vector perpendicular to line created by p0 to p1.
				n2Incidence = new Vector3d(v1.Y - v0.Y, -(v1.X - v0.X), 0);
				n2Incidence.Normalize();
			}
		}

		private void addQuadrilateralGivenThreePoints(double p0, double p1, double p2, TriangleNet.Data.Triangle triangle, Vector3d normal, List<TriangleNet.Geometry.Vertex> localVertices, List<FontTool.Trace.Outbound.Vertex> outboundVertices, FontTool.Trace.Outbound.TrianglesCollection triangles)
		{
			List<List<int>> extraFaces = getFacesForRelativePointIndices(p0, p1, p2, triangle, localVertices);
			// Add polygon entries to list (i.e. list of triangles).
			foreach (List<int> verticesInFace in extraFaces)
				addTriangleToTriangleCollection(verticesInFace, normal, !InvertedSolid, outboundVertices, triangles);
		}

		private static List<List<int>> getFacesForRelativePointIndices(double p0, double p1, double p2, TriangleNet.Data.Triangle triangle, List<TriangleNet.Geometry.Vertex> localVertices)
		{
			List<List<int>> extraFaces = new List<List<int>>();
			// Edge: P1 P2 P1' (or) P2 P0 P2' (or) P0 P1 P0'
			addFacePointsGivenTriangleAndRelativePointIndices(p0, p1, p2, localVertices, triangle, extraFaces);
			// Edge: P2 P2' P1' (or) P0 P0' P2' (or) P1 P1' P0'
			addFacePointsGivenTriangleAndRelativePointIndices(p1, -1 * p1, p2, localVertices, triangle, extraFaces);
			return extraFaces;
		}

		private static void addTriangleToTriangleCollection(List<int> verticesInFace, Vector3d normal, bool reversed, List<FontTool.Trace.Outbound.Vertex> outboundVertices, FontTool.Trace.Outbound.TrianglesCollection triangles)
		{
			// Indices to points A, B, C, representing a triangle, ABC, defined in a counter-clockwise manner.
			int a, b, c;
			// If reversed, store the vertex indices in reverse-order. Otherwise, store them as-is.
			if (reversed)
			{
				a = verticesInFace[2];
				b = verticesInFace[1];
				c = verticesInFace[0];
			}
			else
			{
				a = verticesInFace[0];
				b = verticesInFace[1];
				c = verticesInFace[2];
			}
			triangles.addTriangle(outboundVertices[a - 1], outboundVertices[b - 1], outboundVertices[c - 1], normal: normal);
		}

		public const double SafeTexturePadding = 0.01;
		private FontTool.Trace.Outbound.Vertex getDerivedVertex(int verticalModifier, TriangleNet.Geometry.Vertex vertex)
		{
			// The derived component.
			double z = verticalModifier * HalfSolidification;
			// Derived data.
			double horizontalPercent = (CachedDimensions.X != 0) ? (vertex.x - cachedMinimum.X) / CachedDimensions.X : 1;
			double verticalPercent = (CachedDimensions.Y != 0) ? (vertex.y - cachedMinimum.Y) / CachedDimensions.Y : 1;
			double depthPercent = (CachedDimensions.Z != 0) ? (z - cachedMinimum.Z) / CachedDimensions.Z : 1;
			// Texture coordinates (after appropriate offsets).
			double safeU = LocalTextureOffset.X + vertex.x;
			double safeV = LocalTextureOffset.Y + vertex.y;
			// Attempt to clamp horizontally (to avoid always needing dilation).
			if (horizontalPercent >= 1.0)
				safeU -= SafeTexturePadding;
			else if (horizontalPercent <= 0)
				safeU += SafeTexturePadding;
			// Attempt to clamp vertically (to avoid always needing dilation).
			if (verticalPercent >= 1.0)
				safeV -= SafeTexturePadding;
			else if (verticalPercent <= 0)
				safeV += SafeTexturePadding;
			// In case something went wrong, clamp the percentages to: 0..1
			horizontalPercent = Math.Min(1, Math.Max(0, horizontalPercent));
			verticalPercent = Math.Min(1, Math.Max(0, verticalPercent));
			depthPercent = Math.Min(1, Math.Max(0, depthPercent));
			// Create the display vertex.
			FontTool.Trace.Outbound.Vertex thisVertex = new FontTool.Trace.Outbound.Vertex(XAdjustment + vertex.x, YAdjustment + vertex.y, ZAdjustment + z);
			thisVertex.Normal = new Vector3d(0, 0, verticalModifier);
			thisVertex.UVW = new Vector3d(safeU, safeV, 0);
			// NOTE: These values may later be overwritten (in the context of a TracedCharacter, since other traces may change the dynamic).
			thisVertex.addAuxiliaryEntry("percent", horizontalPercent, verticalPercent, depthPercent);
			return thisVertex;
		}

		private static void addFacePointsGivenTriangleAndRelativePointIndices(double v0IndexD, double v1IndexD, double v2IndexD, List<TriangleNet.Geometry.Vertex> localVertices, TriangleNet.Data.Triangle triangle, List<List<int>> extraFaces)
		{
			int localVertexCount = localVertices.Count;
			// Positive: regular face. Negative: other face.
			int v0Offset = (v0IndexD >= 0) ? 1 : localVertexCount + 1;
			int v1Offset = (v1IndexD >= 0) ? 1 : localVertexCount + 1;
			int v2Offset = (v2IndexD >= 0) ? 1 : localVertexCount + 1;
			// Get the actual vertex index.
			int p0Index = (int)Math.Floor(Math.Abs(v0IndexD));
			int p1Index = (int)Math.Floor(Math.Abs(v1IndexD));
			int p2Index = (int)Math.Floor(Math.Abs(v2IndexD));
			// Get the target vertex indices. May be an existing vertex (starting from 1) or point to a reflected vertex (starting after all the real vertices plus 1).
			int v0 = v0Offset + localVertices.IndexOf(triangle.vertices[p0Index]),
				v1 = v1Offset + localVertices.IndexOf(triangle.vertices[p1Index]),
				v2 = v2Offset + localVertices.IndexOf(triangle.vertices[p2Index]);
			extraFaces.Add(new List<int> { v0, v1, v2 });
		}
	}
}
