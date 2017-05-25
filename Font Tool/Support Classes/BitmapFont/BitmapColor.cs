using System.Collections.Generic;
using System.Xml.Serialization;
using System.Drawing;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlType(TypeName = "BitmapColor", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class BitmapColor
	{
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		private List<BitmapEffect> effects = new List<BitmapEffect>();

		[XmlArray(ElementName = "effects")]
		[XmlArrayItem(ElementName = "effect", Type = typeof(BitmapEffect))]
		public List<BitmapEffect> Effects
		{
			get
			{
				return effects;
			}
			set
			{
				effects = value;
			}
		}

		public BitmapColor() { }

		public BitmapColor(string name, List<BitmapEffect> effects)
		{
			this.Name = name;
			this.Effects = effects;
		}

		public BitmapColor(BitmapColor color)
			: this(color.Name, color.Effects)
		{ }

		public BitmapColor(string name)
			: this(name, new List<BitmapEffect>())
		{ }

		public List<Color> GetUsedColors()
		{
			List<Color> list = new List<Color>();
			if (Effects != null)
			{
				foreach (BitmapEffect effect in Effects)
				{
					foreach (EffectParameter parameter in effect.Parameters)
					{
						if (parameter.Value != null && parameter.Value is XmlColor)
						{
							Color color = (System.Drawing.Color)((XmlColor)parameter.Value);
							if (!list.Contains(color))
								list.Add(color);
						}
					}
				}
			}
			return list;
		}
	}
}
