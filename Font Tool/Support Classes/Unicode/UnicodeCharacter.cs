using System.Xml.Serialization;

namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public class UnicodeCharacter
	{
		ulong id = 0U;
		bool isControlCharacter = false;
		string primaryName = null;
		string secondaryName = null;
		UnicodeBlock block = null;
		
		[XmlIgnore]
		public ulong Id
		{
			get { return id; }
			set { id = value; }
		}

		public bool IsControlCharacter
		{
			get { return isControlCharacter; }
		}

		public bool IsSurrogate
		{
			get { return (id >= 0x00d800 && id <= 0x00dfff); }
		}

		[XmlAttribute(AttributeName = "comment")]
		public string PrimaryName
		{
			get { return primaryName; }
			set { primaryName = value; }
		}

		public string SecondaryName
		{
			get { return secondaryName; }
		}

		public UnicodeBlock Block
		{
			get { return block; }
		}

		public UnicodeCharacter() {
			
		}

		public UnicodeCharacter(ulong id) {
			this.id = id;
		}

		public UnicodeCharacter(ulong id, string primaryName, string secondaryName) : this(id)
		{
			if (primaryName == "<control>")
			{
				this.isControlCharacter = true;
				this.primaryName = secondaryName;
			}
			else
			{
				this.primaryName = primaryName;
				this.secondaryName = secondaryName;
			}
		}

		public UnicodeCharacter(ulong id, string primaryName, string secondaryName, UnicodeBlock block)
			: this(id, primaryName, secondaryName)
		{
			this.block = block;
		}

		public UnicodeCharacter(char c) : this((ulong)c) {
		}

		public override string ToString()
		{
			return string.Format("{0}", (char)Id);
		}
	}
}
