using System;
using System.Drawing;
using System.Xml.Serialization;
using System.Globalization;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlType(TypeName = "XmlColor", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class XmlColor
	{
		private Color color_ = System.Drawing.Color.Black;

		public XmlColor() { }
		public XmlColor(Color c) { color_ = c; }
		public XmlColor(string s) { Color = s; }

		public Color ToColor()
		{
			return color_;
		}

		public void FromColor(Color c)
		{
			color_ = c;
		}

		public static implicit operator Color(XmlColor x)
		{
			return x.ToColor();
		}

		public static implicit operator XmlColor(Color c)
		{
			return new XmlColor(c);
		}

		public static implicit operator string(XmlColor x)
		{
			return x.ToString();
		}

		public static implicit operator XmlColor(string s)
		{
			return new XmlColor(s);
		}

		[XmlAttribute(AttributeName = "value")]
		public string Color
		{
			get { return ToColorCode(color_.ToArgb()); }
			set
			{
				try
				{
					color_ = FromColorCode(value);
				}
				catch (Exception)
				{
					color_ = System.Drawing.Color.Black;
				}
			}
		}

		private Color FromColorCode(string s)
		{
			int argb = Int32.Parse(s.Replace("#", ""), NumberStyles.HexNumber);
			Color color = System.Drawing.Color.FromArgb(argb);
			return color;
		}

		private string ToColorCode(int argb)
		{
			return argb.ToString("X");
		}

		public override string ToString()
		{
			return Color;
		}
	}
}
