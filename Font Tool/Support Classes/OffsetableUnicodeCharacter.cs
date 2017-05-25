using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.ComponentModel;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public class OffsetableUnicodeCharacter : UnicodeCharacter
	{
		private float offsetWidth = 0;
		private float offsetX = 0;
		private float offsetY = 0;

		[XmlAttribute(AttributeName = "code")]
		public ulong Code
		{
			get { return base.Id; }
			set { base.Id = value; }
		}

		public string Name
		{
			get
			{
				string s = base.PrimaryName;
				string unformattedName = s;
				if (unformattedName != null)
				{
					string formattedName = Regex.Replace(unformattedName.ToLower(), @"(^|\s)([^\s])", m => m.Value.ToUpper());
					formattedName = Regex.Replace(formattedName, @"\s(of|in|by|and)\s", m => m.Value.ToLower(), RegexOptions.IgnoreCase);
					return formattedName;
				}
				else
					return s;
			}
		}

		[DefaultValue(0), XmlAttribute(AttributeName = "offset-width")]
		public virtual float OffsetWidth
		{
			get { return offsetWidth; }
			set { offsetWidth = value; }
		}

		[DefaultValue(0), XmlAttribute(AttributeName = "offset-x")]
		public virtual float OffsetX
		{
			get { return offsetX; }
			set { offsetX = value; }
		}

		[DefaultValue(0), XmlAttribute(AttributeName = "offset-y")]
		public virtual float OffsetY
		{
			get { return offsetY; }
			set { offsetY = value; }
		}

		private float scaleX = 1f;

		[DefaultValue(1), XmlAttribute(AttributeName = "scale-x")]
		public virtual float ScaleX
		{
			get { return scaleX; }
			set { scaleX = value; }
		}

		private float scaleY = 1f;

		[DefaultValue(1), XmlAttribute(AttributeName = "scale-y")]
		public virtual float ScaleY
		{
			get { return scaleY; }
			set { scaleY = value; }
		}

		public OffsetableUnicodeCharacter()
		{ }

		public OffsetableUnicodeCharacter(UnicodeCharacter character)
			: this(character, 0, 0, 0, 1, 1)
		{ }

		public OffsetableUnicodeCharacter(ulong id)
			: this(UnicodeBlocks.GetCharacterById(id), 0, 0, 0, 1, 1)
		{ }

		public OffsetableUnicodeCharacter(UnicodeCharacter character, float offsetWidth, float offsetX, float offsetY, float scaleX, float scaleY)
			: base(character.Id, character.PrimaryName, character.SecondaryName, character.Block)
		{
			this.offsetWidth = offsetWidth;
			this.offsetX = offsetX;
			this.offsetY = offsetY;
			this.scaleX = scaleX;
			this.scaleY = scaleY;
		}

		public override string ToString()
		{
			return char.ConvertFromUtf32((int)Code);
		}
	}
}
