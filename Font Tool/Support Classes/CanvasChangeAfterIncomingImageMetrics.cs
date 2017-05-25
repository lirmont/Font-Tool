using System.Drawing;

namespace FontTool
{
	public class CanvasChangeAfterIncomingImageMetrics
	{
		private PointF zeroPoint = PointF.Empty;
		private Size canvasSize = Size.Empty;
		private PointF imageLocation = PointF.Empty;
		private PointF existingImagesLocation = PointF.Empty;
		private bool suggestsCanvasExpansion = false;

		public PointF ZeroPoint
		{
			get { return zeroPoint; }
			set { zeroPoint = value; }
		}

		public Size CanvasSize
		{
			get { return canvasSize; }
			set { canvasSize = value; }
		}

		public PointF ImageLocation
		{
			get { return imageLocation; }
			set { imageLocation = value; }
		}

		public PointF ExistingImagesLocation
		{
			get { return existingImagesLocation; }
			set { existingImagesLocation = value; }
		}

		public bool SuggestsCanvasExpansion
		{
			get { return suggestsCanvasExpansion; }
			set { suggestsCanvasExpansion = value; }
		}

		public CanvasChangeAfterIncomingImageMetrics(PointF zeroPoint, Size canvasSize, PointF imageLocation, PointF existingImagesLocation, bool suggestsCanvasExpansion)
		{
			this.zeroPoint = zeroPoint;
			this.canvasSize = canvasSize;
			this.imageLocation = imageLocation;
			this.existingImagesLocation = existingImagesLocation;
			this.suggestsCanvasExpansion = suggestsCanvasExpansion;
		}

		public void Assert(Bitmap image, bool existingImage = false)
		{
			PointF offset = (existingImage) ? existingImagesLocation : imageLocation;
			double minX = zeroPoint.X + offset.X;
			double maxX = minX + image.Width;
			double minY = zeroPoint.Y + offset.Y;
			double maxY = minY + image.Height;
			System.Diagnostics.Debug.Assert(minX >= 0);
			System.Diagnostics.Debug.Assert(maxX <= canvasSize.Width);
			System.Diagnostics.Debug.Assert(minY >= 0);
			System.Diagnostics.Debug.Assert(maxY <= canvasSize.Height);
		}
	}
}
