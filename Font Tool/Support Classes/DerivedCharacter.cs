using System.Diagnostics;
namespace FontTool
{
	[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
	public class DerivedCharacter : OffsetableUnicodeCharacter
	{
		private Character baseCharacter = null;
		private OffsetableUnicodeCharacter delta = null;

		public Character BaseCharacter
		{
			get { return baseCharacter; }
			set { baseCharacter = value; }
		}

		public OffsetableUnicodeCharacter Delta
		{
			get { return delta; }
			set { delta = value; }
		}

		public DerivedCharacter()
		{ }

		public DerivedCharacter(ulong key)
			: this(new Character(UnicodeBlocks.GetCharacterById(key), 0, 0, 0, 1, 1), new OffsetableUnicodeCharacter(UnicodeBlocks.GetCharacterById(key), 0, 0, 0, 1, 1))
		{ }

		public DerivedCharacter(Character baseCharacter)
			: this(baseCharacter, new OffsetableUnicodeCharacter(baseCharacter, 0, 0, 0, 1, 1))
		{ }

		public DerivedCharacter(Character baseCharacter, OffsetableUnicodeCharacter delta)
		{
			this.Id = baseCharacter.Id;
			this.baseCharacter = baseCharacter;
			this.delta = delta;
			base.PrimaryName = baseCharacter.PrimaryName;
		}

		public DerivedCharacter(OffsetableUnicodeCharacter baseCharacter)
		{
			this.Id = baseCharacter.Id;
			this.baseCharacter = new Character(
				baseCharacter,
				//
				baseCharacter.OffsetWidth,
				baseCharacter.OffsetX,
				baseCharacter.OffsetY,
				baseCharacter.ScaleX,
				baseCharacter.ScaleY
			);
			this.delta = new OffsetableUnicodeCharacter(baseCharacter, 0, 0, 0, 1, 1);
			base.PrimaryName = baseCharacter.PrimaryName;
		}

		public override float OffsetWidth
		{
			get { return delta.OffsetWidth; }
			set { delta.OffsetWidth = value; }
		}

		public override float OffsetX
		{
			get { return delta.OffsetX; }
			set { delta.OffsetX = value; }
		}

		public override float OffsetY
		{
			get { return delta.OffsetY; }
			set { delta.OffsetY = value; }
		}

		public override float ScaleX
		{
			get { return delta.ScaleX; }
			set { delta.ScaleX = value; }
		}

		public override float ScaleY
		{
			get { return delta.ScaleY; }
			set { delta.ScaleY = value; }
		}

		public OffsetableUnicodeCharacter FinalOffset
		{
			get
			{
				return new OffsetableUnicodeCharacter(baseCharacter, baseCharacter.OffsetWidth + delta.OffsetWidth, baseCharacter.OffsetX + delta.OffsetX, baseCharacter.OffsetY + delta.OffsetY, baseCharacter.ScaleX * delta.ScaleX, baseCharacter.ScaleY * delta.ScaleY);
			}
		}

		public static implicit operator Character(DerivedCharacter derived)
		{
			return derived.baseCharacter;
		}
	}
}
