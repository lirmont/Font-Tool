using System;

namespace ColorControl
{
	/// <summary>
	/// Structure to define HSB.
	/// </summary>
	public struct HSV
	{
		/// <summary>
		/// Gets an empty HSB structure;
		/// </summary>
		public static readonly HSV Empty = new HSV();

		private double hue;
		private double saturation;
		private double value;

		public static bool operator ==(HSV item1, HSV item2)
		{
			return (item1.Hue == item2.Hue && item1.Saturation == item2.Saturation && item1.Value == item2.Value);
		}

		public static bool operator !=(HSV item1, HSV item2)
		{
			return (item1.Hue != item2.Hue || item1.Saturation != item2.Saturation || item1.Value != item2.Value);
		}

		/// <summary>
		/// Gets or sets the hue component.
		/// </summary>
		public double Hue
		{
			get
			{
				return Math.Round(hue, MidpointRounding.AwayFromZero);
			}
			set
			{
				hue = (value > 360) ? 360 : ((value < 0) ? 0 : value);
			}
		}

		/// <summary>
		/// Gets or sets saturation component.
		/// </summary>
		public double Saturation
		{
			get
			{
				return Math.Round(saturation * 100, MidpointRounding.AwayFromZero) / 100.0;
			}
			set
			{
				saturation = (value > 1) ? 1 : ((value < 0) ? 0 : Math.Round(value * 100, MidpointRounding.AwayFromZero) / 100.0);
			}
		}

		/// <summary>
		/// Gets or sets the brightness component.
		/// </summary>
		public double Value
		{
			get
			{
				return Math.Round(value * 100, MidpointRounding.AwayFromZero) / 100.0;
			}
			set
			{
				this.value = (value > 1) ? 1 : ((value < 0) ? 0 : Math.Round(value * 100, MidpointRounding.AwayFromZero) / 100.0);
			}
		}

		/// <summary>
		/// Creates an instance of a HSB structure.
		/// </summary>
		/// <param name="h">Hue value.</param>
		/// <param name="s">Saturation value.</param>
		/// <param name="v">Brightness value.</param>
		public HSV(double h, double s, double v)
		{
			hue = (h > 360) ? 360 : ((h < 0) ? 0 : Math.Round(h, MidpointRounding.AwayFromZero));
			saturation = (s > 1) ? 1 : ((s < 0) ? 0 : Math.Round(s * 100, MidpointRounding.AwayFromZero) / 100.0);
			value = (v > 1) ? 1 : ((v < 0) ? 0 : Math.Round(v * 100, MidpointRounding.AwayFromZero) / 100.0);
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType()) return false;

			return (this == (HSV)obj);
		}

		public override int GetHashCode()
		{
			return Hue.GetHashCode() ^ Saturation.GetHashCode() ^ Value.GetHashCode();
		}

		public static implicit operator RGB(HSV hsv)
		{
			return HSVtoRGB(hsv);
		}

		public static implicit operator CIEXYZ(HSV hsv)
		{
			return RGB.RGBtoXYZ(HSVtoRGB(hsv));
		}

		public static implicit operator CIELab(HSV hsv)
		{
			return RGB.RGBtoLab(HSVtoRGB(hsv));
		}

		/// <summary>
		/// Converts HSB to RGB.
		/// </summary>
		public static RGB HSVtoRGB(double h, double s, double v)
		{
			RGB rgbNot = new RGB(0, 0, 0);
			//
			h = Math.Round(h, MidpointRounding.AwayFromZero);
			double C = v * s;
			double hNot = h / 60.0;
			double X = C * (1 - Math.Abs(hNot % 2 - 1));
			if (hNot < 1 && 0 <= hNot)
				rgbNot = new RGB((int)Math.Round(C * 255, MidpointRounding.AwayFromZero), (int)Math.Round(X * 255, MidpointRounding.AwayFromZero), 0);
			else if (hNot < 2 && 1 <= hNot)
				rgbNot = new RGB((int)Math.Round(X * 255, MidpointRounding.AwayFromZero), (int)Math.Round(C * 255, MidpointRounding.AwayFromZero), 0);
			else if (hNot < 3 && 2 <= hNot)
				rgbNot = new RGB(0, (int)Math.Round(C * 255, MidpointRounding.AwayFromZero), (int)Math.Round(X * 255, MidpointRounding.AwayFromZero));
			else if (hNot < 4 && 3 <= hNot)
				rgbNot = new RGB(0, (int)Math.Round(X * 255, MidpointRounding.AwayFromZero), (int)Math.Round(C * 255, MidpointRounding.AwayFromZero));
			else if (hNot < 5 && 4 <= hNot)
				rgbNot = new RGB((int)Math.Round(X * 255, MidpointRounding.AwayFromZero), 0, (int)Math.Round(C * 255, MidpointRounding.AwayFromZero));
			else if (hNot < 6 && 5 <= hNot)
				rgbNot = new RGB((int)Math.Round(C * 255, MidpointRounding.AwayFromZero), 0, (int)Math.Round(X * 255, MidpointRounding.AwayFromZero));
			int m = (int)Math.Round((v - C) * 255, MidpointRounding.AwayFromZero);
			//
			return new RGB(Math.Min(rgbNot.Red + m, 255), Math.Min(rgbNot.Green + m, 255), Math.Min(rgbNot.Blue + m, 255));
		}

		public static RGB HSVtoRGB(HSV hsv)
		{
			return HSVtoRGB(hsv.Hue, hsv.Saturation, hsv.Value);
		}
	}
}