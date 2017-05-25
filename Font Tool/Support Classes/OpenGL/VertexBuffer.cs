using System;
using OpenTK;
using System.Runtime.InteropServices;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace FontTool
{
	[StructLayout(LayoutKind.Sequential)]
	public struct BufferedVertex
	{
		public Vector2 TexCoord;
		public Vector3 Normal;
		public Vector3 Position;
		public static readonly int Stride = Marshal.SizeOf(default(BufferedVertex));
	}

	public struct BufferedVertexWithAuxiliaryData
	{
		public Vector2 TexCoord;
		public Vector3 Normal;
		public Vector3 Position;
		public Dictionary<string, Vector4d> Auxiliary;

		public static bool Equivalent(BufferedVertexWithAuxiliaryData a, BufferedVertexWithAuxiliaryData b, bool ignoreAuxiliary = true)
		{
			bool texCoordsAreEqual = (a.TexCoord == b.TexCoord);
			bool normalsAreEqual = (a.Normal == b.Normal);
			bool positionsAreEqual = (a.Position == b.Position);
			bool vertexIsEqual = (texCoordsAreEqual && normalsAreEqual && positionsAreEqual);
			if (ignoreAuxiliary || !vertexIsEqual)
				return vertexIsEqual;
			else {
				bool keysAreSameLength = (a.Auxiliary.Keys.Count == b.Auxiliary.Keys.Count);
				//
				if (keysAreSameLength)
				{
					List<string> keys = new List<string>(a.Auxiliary.Keys);
					List<string> otherKeys = new List<string>(b.Auxiliary.Keys);
					bool keysAreSame = (keys.TrueForAll(item => otherKeys.Contains(item)));
					if (keysAreSame)
					{
						bool valuesAreSame = (keys.TrueForAll(item => a.Auxiliary[item] == b.Auxiliary[item]));
						return (keysAreSameLength && keysAreSame && valuesAreSame);
					}
					else
						return false;
				}
				else
					return false;
				
			}
		}
	}

	public interface IVertexBuffer
	{
		string Name
		{
			get;
			set;
		}

		Vector3d Offset
		{
			get;
			set;
		}

		Vector3d TextureScale
		{
			get;
			set;
		}

		Vector3d TextureOffset
		{
			get;
			set;
		}

		void Render();
	}
	
	public class VertexBuffer : IVertexBuffer
	{
		BufferedVertexWithAuxiliaryData[] data = new BufferedVertexWithAuxiliaryData[0];
		protected string name = "";
		protected Vector3d offset = Vector3d.Zero;
		protected Vector3d textureScale = Vector3d.One;
		protected Vector3d textureOffset = Vector3d.Zero;
		//
		protected Dictionary<int, int> simplifications = new Dictionary<int, int>();

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public Vector3d Offset
		{
			get { return offset; }
			set { offset = value; }
		}

		public Vector3d TextureScale
		{
			get { return textureScale; }
			set { textureScale = value; }
		}

		public Vector3d TextureOffset
		{
			get { return textureOffset; }
			set { textureOffset = value; }
		}

		public BufferedVertexWithAuxiliaryData[] Data
		{
			get { return data; }
			set { data = value; }
		}

		public int Length
		{
			get { return (data != null) ? data.Length : 0; }
		}

		public Dictionary<int, int> Simplifications
		{
			get { return simplifications; }
			set { simplifications = value; }
		}

		public VertexBuffer(string name, Vector3d? offset, Vector3d? textureScale = null, Vector3d? textureOffset = null)
		{
			this.name = name;
			// Store location translation (INTENT: store local object transforms from center point).
			this.offset = (offset != null) ? offset.Value : this.offset;
			// Store texture scale (INTENT: store texture coordinates in pixels).
			this.textureScale = (textureScale != null) ? textureScale.Value : this.textureScale;
			// Store texture offset (INTENT: move texture coordinates within a larger, combined image).
			this.textureOffset = (textureOffset != null) ? textureOffset.Value : this.textureOffset;
		}

		public VertexBuffer(string name, List<BufferedVertexWithAuxiliaryData> data, Vector3d offset, Vector3d? textureScale = null, Vector3d? textureOffset = null, bool calculateSimplifications = false)
			: this(name, offset, textureScale: textureScale, textureOffset: textureOffset)
		{
			this.data = data.ToArray();
			// Calculate simplifications of vertices.
			if (calculateSimplifications)
			{
				for (int i = 0; i < data.Count; i++)
				{
					BufferedVertexWithAuxiliaryData vertex = data[i];
					int equivalentVertex = data.FindIndex(item => BufferedVertexWithAuxiliaryData.Equivalent(vertex, item, ignoreAuxiliary: true));
					simplifications.Add(i, equivalentVertex);
				}
			}
		}

		public void Render()
		{ }
	}

	public class VertexBufferOnGPU : VertexBuffer
	{
		int id, length;
		OpenGLConfiguration oglConfiguration;

		int Id
		{
			get
			{
				// Create an id on first use.
				if (id == 0)
				{
					GraphicsContext.Assert();
					GL.GenBuffers(1, out id);
					// If still zero, an error has happened.
					if (id == 0)
						throw new Exception("Could not create VBO.");
				}
				return id;
			}
		}

		new public int Length
		{
			get { return length; }
		}

		public VertexBufferOnGPU(string name, BufferedVertex[] data, Vector3d offset, OpenGLConfiguration oglConfiguration, Vector3d? textureScale = null, Vector3d? textureOffset = null)
			: base(name, offset, textureScale: textureScale, textureOffset: textureOffset)
		{
			this.oglConfiguration = oglConfiguration;
			SetData(data);
		}

		public new BufferedVertex[] Data = null;

		public void SetData(BufferedVertex[] data)
		{
			// Sanitize.
			if (data == null)
				data = new BufferedVertex[0];
			// Cache length.
			this.length = data.Length;
			// Send the data to the GPU.
			if (oglConfiguration.BufferedDataIsSupported)
			{
				GL.BindBuffer(BufferTarget.ArrayBuffer, Id);
				GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Length * BufferedVertex.Stride), data, BufferUsageHint.StaticDraw);
			}
			else
				Data = data;
		}

		public void Render(BeginMode mode = BeginMode.Triangles, int? start = null, int? countPerStep = null, int? limit = null)
		{
			RenderFloatingPoints(mode: mode, start: start, countPerStep: countPerStep, limit: limit);
		}

		public void RenderFloatingPoints(BeginMode mode = BeginMode.Triangles, int? start = null, int? countPerStep = null, int? limit = null)
		{
			bool noSettings = (start == null && countPerStep == null && limit == null) ? true : false;
			start = start ?? 0;
			countPerStep = countPerStep ?? length;
			if (oglConfiguration.BufferedDataIsSupported)
			{
				GL.BindBuffer(BufferTarget.ArrayBuffer, Id);
				// Enable and set look-up types.
				GL.InterleavedArrays(InterleavedArrayFormat.T2fN3fV3f, 0, (IntPtr)null);
			}
			else {
				GL.InterleavedArrays<BufferedVertex>(InterleavedArrayFormat.T2fN3fV3f, BufferedVertex.Stride, Data);
			}
			// Draw all elements at once (design: triangles).
			if (noSettings)
				GL.DrawArrays(mode, start.Value, countPerStep.Value);
			else {
				// Draw elements, breaking as requested.
				if (limit == null)
					for (int i = start.Value; i < length; i += countPerStep.Value)
						GL.DrawArrays(mode, i, countPerStep.Value);
				else {
					for (int i = start.Value; i < limit.Value; i += countPerStep.Value)
					{
						int countInThisStep = countPerStep.Value;
						if (!(i + countPerStep.Value <= limit.Value))
							countInThisStep = i + countPerStep.Value - limit.Value;
						if (countInThisStep > 0)
							GL.DrawArrays(mode, i, countInThisStep);
					}
				}
			}
			// Unbind.
			if (oglConfiguration.BufferedDataIsSupported)
				GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		}
	}
}
