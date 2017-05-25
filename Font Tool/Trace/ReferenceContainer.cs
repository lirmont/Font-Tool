using System;
using System.Drawing;

namespace AutoTrace
{
	public class ReferenceContainer<T>
	{
		public T Value { get; set; }
		public ReferenceContainer(T item)
		{
			Value = item;
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		public static implicit operator T(ReferenceContainer<T> item)
		{
			return item.Value;
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public bool Equal(PointF point, float? thisTolerance = null)
		{
			if (this.Value is PointF)
			{
				PointF thisObject = (PointF)(object)this.Value;
				bool exactlyEqual = (thisObject.X == point.X && thisObject.Y == point.Y);
				if (exactlyEqual)
					return true;
				else if (thisTolerance != null)
				{
					bool xKindOfEqual = (Math.Abs(thisObject.X - point.X) < thisTolerance.Value);
					bool yKindOfEqual = (Math.Abs(thisObject.Y - point.Y) < thisTolerance.Value);
					bool kindOfEqual = (xKindOfEqual && yKindOfEqual);
					if (kindOfEqual)
						return true;
				}
			}
			return false;
		}
	}
}
