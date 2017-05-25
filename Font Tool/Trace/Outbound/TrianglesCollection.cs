using System.Collections.Generic;
using OpenTK;
using System.Drawing;

namespace FontTool.Trace.Outbound
{
	public class TrianglesCollection
	{
		List<Vertex> pool = new List<Vertex>();
		List<Triangle> triangles = new List<Triangle>();

		public List<Vertex> Vertices
		{
			get { return pool; }
			set { pool = value; }
		}

		public List<Triangle> Triangles
		{
			get { return triangles; }
			set { triangles = value; }
		}

		public List<Vertex> TrianglesAsVerticesWithFaceNormals
		{
			get
			{
				List<Vertex> vertices = new List<Vertex>();
				foreach (Triangle triangle in Triangles)
				{
					// Rewrite the vertices with the face normal.
					Vertex A = new Vertex(pool[triangle.P0 - 1], triangle.Normal.Value);
					Vertex B = new Vertex(pool[triangle.P1 - 1], triangle.Normal.Value);
					Vertex C = new Vertex(pool[triangle.P2 - 1], triangle.Normal.Value);
					// Add them to the list.
					vertices.Add(A);
					vertices.Add(B);
					vertices.Add(C);
				}
				return vertices;
			}
		}

		public TrianglesCollection()
		{
		}

		// NOTE: Counts from 1 (not zero).
		public void addTriangle(Vertex A, Vertex B, Vertex C, Vector3d? normal = null)
		{
			int indexA = pool.IndexOf(A), indexB = pool.IndexOf(B), indexC = pool.IndexOf(C);
			// Add vertices to pool if necessary.
			if (indexA < 0)
			{
				pool.Add(A);
				indexA = pool.Count - 1;
			}
			if (indexB < 0)
			{
				pool.Add(B);
				indexB = pool.Count - 1;
			}
			if (indexC < 0)
			{
				pool.Add(C);
				indexC = pool.Count - 1;
			}
			// Add triangle definition.
			triangles.Add(new Triangle(indexA + 1, indexB + 1, indexC + 1, normal: normal));
		}

		public string getPolygonEntries(string vertexPoolName, string textureName, int indentation = 0)
		{
			// Realize indentation.
			string indentationString = "";
			for (int i = 0; i < indentation; i++)
				indentationString += "\t";
			string textureEntry = (textureName != null) ? indentationString + "	<TRef> { " + textureName + " }" : null;
			// Make a place to store the individual parts of the vertex pool entry.
			List<string> entries = new List<string>();
			foreach (Triangle triangle in triangles)
			{
				entries.Add(indentationString + "<Polygon> {");
				if (textureEntry != null)
					entries.Add(textureEntry);
				if (triangle.Normal != null)
					entries.Add(indentationString + "	<Normal> { " + triangle.Normal.Value.X + " " + triangle.Normal.Value.Y + " " + triangle.Normal.Value.Z + " }");
				entries.Add(indentationString + "	<VertexRef> { " + triangle.P0 + " " + triangle.P1 + " " + triangle.P2 + " <Ref> { " + vertexPoolName + " } }");
				entries.Add(indentationString + "}");
			}
			//
			return string.Join("\n", entries.ToArray());
		}

		public string getEGGVertexPool(string name, int indentation = 0, PointF? globalTextureOffset = null, Vector3d? screenScale = null)
		{
			// Realize indentation.
			string indentationString = "";
			for (int i = 0; i < indentation; i++)
				indentationString += "\t";
			// Realize global texture offset, if applicable.
			if (globalTextureOffset == null)
				globalTextureOffset = PointF.Empty;
			// Realize scale, if applicable.
			if (screenScale == null)
				screenScale = Vector3d.One;
			// Make a place to store the individual parts of the vertex pool entry.
			List<string> entries = new List<string>();
			entries.Add(indentationString + "<VertexPool> " + name +" {");
			// Vertex entries.
			for (int i = 0; i < pool.Count; i++)
			{
				Vertex thisVertex = pool[i];
				// Make a place to store the individual parts of the vertex entry.
				List<string> entryParts = new List<string>();
				entryParts.Add(indentationString + "	<Vertex> " + (i + 1) + " {");
				// TODO: Multiply position by screen scales.
				if (thisVertex.Position != null)
				{
					double x = thisVertex.Position.Value.X * screenScale.Value.X;
					double y = thisVertex.Position.Value.Y * screenScale.Value.Y;
					double z = thisVertex.Position.Value.Z * screenScale.Value.Z;
					entryParts.Add(x + " " + y + " " + z);
				}
				if (thisVertex.Normal != null)
				{
					entryParts.Add("<Normal> {");
					entryParts.Add(thisVertex.Normal.Value.X + " " + thisVertex.Normal.Value.Y + " " + thisVertex.Normal.Value.Z);
					entryParts.Add("}");
				}
				if (thisVertex.UVW != null)
				{
					double u = globalTextureOffset.Value.X + thisVertex.UVW.Value.X;
					double v = globalTextureOffset.Value.Y + thisVertex.UVW.Value.Y;
					double w = thisVertex.UVW.Value.Z;
					//
					entryParts.Add("<UV> {");
					entryParts.Add((w != 0) ? u + " " + v + " " + w : u + " " + v);
					entryParts.Add("}");
				}
				// Add all auxiliary data (like horizontal percent).
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
			entries.Add(indentationString + "}");
			// Send the <VertexPool> entry back.
			return string.Join("\n", entries.ToArray());
		}
	}
}
