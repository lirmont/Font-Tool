using System;
using System.Xml.Serialization;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlInclude(typeof(XmlColor))]
	public class EffectParameter
	{
		private string name = null;
		private object value = null;
		private Type type = null;

		[XmlAttribute(AttributeName = "name")]
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		[XmlElement(ElementName = "value")]
		public object Value
		{
			get { return this.value; }
			set { this.value = value; }
		}
		
		public EffectParameter()
		{
		}

		public EffectParameter(string name, object value) : this(value.GetType())
		{
			this.name = name;
			this.value = value;
		}

		public EffectParameter(Type type)
			: this()
		{
			this.type = type;
		}
	}
}
