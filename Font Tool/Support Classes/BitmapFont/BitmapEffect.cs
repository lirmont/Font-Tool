using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.ComponentModel;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]
	[XmlType(TypeName = "BitmapEffect", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class BitmapEffect : IGeometryProvider
	{
		public enum ScopeType { Generic = 0, Specific = 1 };

		//
		private string name = null;
		private ParameterCollection parameters = null;
		private UlongCollection applyToCharacters = null;
		private bool requestsRedrawOnCharacterOffsetChanged = false;
		private Tracer tracer = null;
		private bool startsNewLayer = false;
		private bool expectsToStartFromBaseImage = false;

		[XmlIgnore]
		public bool RedrawOnCharacterOffsetChanged
		{
			get { return requestsRedrawOnCharacterOffsetChanged; }
			set { requestsRedrawOnCharacterOffsetChanged = value; }
		}

		[XmlIgnore]
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		[XmlArray(ElementName = "parameters")]
		[XmlArrayItem(ElementName = "parameter")]
		public ParameterCollection Parameters
		{
			get { return parameters; }
			set { parameters = value; }
		}

		[XmlArray(ElementName = "apply-to-characters")]
		[XmlArrayItem(ElementName = "character-code")]
		public UlongCollection ApplyToCharacters
		{
			get { return applyToCharacters; }
			set { applyToCharacters = value; }
		}

		[XmlElement(ElementName = "tracer")]
		public Tracer Tracer
		{
			get { return tracer; }
			set { tracer = value; }
		}

		[DefaultValue(false), XmlAttribute(AttributeName = "starts-new-layer")]
		public bool StartsNewLayer
		{
			get { return startsNewLayer; }
			set { startsNewLayer = value; }
		}

		[DefaultValue(false), XmlAttribute(AttributeName = "expects-base-image")]
		public bool ExpectsToStartFromBaseImage
		{
			get { return expectsToStartFromBaseImage; }
			set { expectsToStartFromBaseImage = value; }
		}

		[XmlIgnore]
		public ScopeType Scope
		{
			get {
				return (ApplyToCharacters != null && ApplyToCharacters.Count > 0) ? ScopeType.Specific : ScopeType.Generic;
			}
		}

		[XmlIgnore]
		public IGeometryProvider GeometryProvider
		{
			get
			{
				return this as IGeometryProvider;
			}
		}

		[XmlIgnore]
		public ITracer TraceProvider
		{
			get
			{
				return tracer as ITracer;
			}
		}

		public BitmapEffect()
		{
			this.parameters = new ParameterCollection();
			this.applyToCharacters = new UlongCollection();
		}

		public BitmapEffect(string name)
			: this()
		{
			this.name = name;
		}

		public BitmapEffect(string name, List<EffectParameter> parameters) : this(name)
		{
			if (parameters != null)
				foreach (EffectParameter parameter in parameters)
					this.parameters.Add(parameter);
		}

		public BitmapEffect(string name, List<EffectParameter> parameters, List<ulong> characterCodes)
			: this(name, parameters)
		{
			if (characterCodes != null)
				foreach (ulong code in characterCodes)
					this.applyToCharacters.Add(code);
		}

		public virtual Bitmap Apply(Bitmap bitmap, OffsetableUnicodeCharacter character = null, FontInSize size = null)
		{
			Bitmap outputBitmap = (Bitmap)bitmap.Clone();
			bitmap.Dispose();
			return outputBitmap;
		}

		public virtual OffsetableUnicodeCharacter Delta(OffsetableUnicodeCharacter character)
		{
			OffsetableUnicodeCharacter derivedCharacter = character;
			return derivedCharacter;
		}

		//
		public static Dictionary<string, ParameterDescription> ParameterDescriptions = new Dictionary<string, ParameterDescription> { };

		public virtual Dictionary<string, ParameterDescription> getDescriptions()
		{
			return BitmapEffect.ParameterDescriptions;
		}

		public virtual string getDescription(string parameterName)
		{
			return GetDescriptionFromParameters(parameterName);
		}

		public virtual Control getValidatingControl(string parameterName, object value)
		{
			return GetValidatingControlFromParameters(parameterName, value);
		}

		protected string GetDescriptionFromParameters(string parameterName, Dictionary<string, ParameterDescription> descriptions = null)
		{
			if (descriptions == null)
				descriptions = ParameterDescriptions;
			//
			if (descriptions != null && descriptions.ContainsKey(parameterName))
				return descriptions[parameterName].Description;
			return string.Format("Value of \"{0}\".", SupportFunctions.TitleCaseString(parameterName));
		}

		protected Control GetValidatingControlFromParameters(string parameterName, object value, Dictionary<string, ParameterDescription> descriptions = null)
		{
			if (descriptions == null)
				descriptions = ParameterDescriptions;
			//
			if (descriptions != null && descriptions.ContainsKey(parameterName))
			{
				Control control = descriptions[parameterName].GetNewValidatingControl(value);
				control.Tag = parameterName;
				return control;
			}
			return new Panel();
		}

		public bool shouldApplyEffectTo(ulong characterCode)
		{
			return (Scope == ScopeType.Specific) ? ApplyToCharacters.Contains(characterCode) : true;
		}

		public bool shouldApplyEffectTo(UnicodeCharacter character)
		{
			return shouldApplyEffectTo(character.Id);
		}

		AutoTrace.Tracing IGeometryProvider.GetGeometry(Bitmap image)
		{
			// First, try to use the specified version. On failure, provide an empty tracing.
			try {
				return TraceProvider.TraceImage(image);
			} catch {
				return Tracer.TraceImage(image);
			}
		}
	}
}
