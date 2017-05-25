using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FontTool
{
	public class TraceRequest : IGeometryProvider
	{
		private Bitmap image = null;
		private ITracer tracer = null;

		public Bitmap Image
		{
			get { return image; }
			set { image = value; }
		}

		public ITracer Tracer
		{
			get { return tracer; }
			set { tracer = value; }
		}

		public IGeometryProvider GeometryProvider
		{
			get
			{
				return this as IGeometryProvider;
			}
		}

		public ITracer TraceProvider
		{
			get
			{
				return tracer as ITracer;
			}
		}

		public TraceRequest()
		{

		}

		public TraceRequest(Bitmap image, ITracer tracer)
		{
			this.image = image;
			this.tracer = tracer;
		}

		AutoTrace.Tracing IGeometryProvider.GetGeometry(Bitmap image)
		{
			// First, try to use the specified version. On failure, provide an empty tracing.
			try {
				// TODO: Restructure so flipping the image is unnecessary.
				Bitmap flippedImage = SupportFunctions.GetClonedBitmap(image, flipped: true);
				return TraceProvider.TraceImage(flippedImage);
			} catch {
				return FontTool.Tracer.TraceImage(image);
			}
		}
	}
}
