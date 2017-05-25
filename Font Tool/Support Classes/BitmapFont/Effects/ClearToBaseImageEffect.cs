using System;
using System.Drawing;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	[XmlType(TypeName = "ClearToBaseImageEffect", Namespace = "http://darkabstraction.com/schemas/font.xsd")]
	public class ClearToBaseImageEffect : BitmapEffect
	{
		public ClearToBaseImageEffect()
			: base("ClearToBaseImage")
		{
			this.StartsNewLayer = true;
			this.ExpectsToStartFromBaseImage = true;
		}

		public ClearToBaseImageEffect(List<EffectParameter> parameters)
			: base("ClearToBaseImage", parameters)
		{
			this.StartsNewLayer = true;
			this.ExpectsToStartFromBaseImage = true;
		}

		public ClearToBaseImageEffect(List<EffectParameter> parameters, List<ulong> characters)
			: base("ClearToBaseImage", parameters, characters)
		{
			this.StartsNewLayer = true;
			this.ExpectsToStartFromBaseImage = true;
		}

		public override Bitmap Apply(Bitmap bitmap, OffsetableUnicodeCharacter character = null, FontInSize size = null)
		{
			return null;
		}

		public override OffsetableUnicodeCharacter Delta(OffsetableUnicodeCharacter character)
		{
			return null;
		}

		// No parameters.
		public new static Dictionary<string, ParameterDescription> ParameterDescriptions = new Dictionary<string, ParameterDescription>
		{
		};

		public override Dictionary<string, ParameterDescription> getDescriptions()
		{
			return ClearToBaseImageEffect.ParameterDescriptions;
		}

		public override string getDescription(string parameterName)
		{
			return GetDescriptionFromParameters(parameterName, ParameterDescriptions);
		}

		public override Control getValidatingControl(string parameterName, object value)
		{
			return GetValidatingControlFromParameters(parameterName, value, descriptions: getDescriptions());
		}
	}
}
