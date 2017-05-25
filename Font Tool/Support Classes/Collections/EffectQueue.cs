using System.Collections.Generic;

namespace FontTool
{
	public class EffectQueue : Queue<BitmapEffect>
	{
		// Provide a means by which to track how many of the effects come from the variant (and not the color).
		private int countFromVariant = 0;
		private bool shouldNotDiscardDelta = false;
		private bool shouldNotDiscardWidth = true;
		public int CountFromVariant
		{
			get { return countFromVariant; }
			set { countFromVariant = value; }
		}

		public bool ShouldNotDiscardDelta
		{
			get { return shouldNotDiscardDelta; }
			set { shouldNotDiscardDelta = value; }
		}

		public bool ShouldNotDiscardWidth
		{
			get { return shouldNotDiscardWidth; }
			set { shouldNotDiscardWidth = value; }
		}
	}
}
