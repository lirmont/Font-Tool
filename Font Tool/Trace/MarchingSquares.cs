// Licensed under the creative commons Atrribution 3.0 license
// You are free to share, copy, distribute, and transmit this work
// You are free to alter this work
// If you use this work, please attribute it somewhere in the supporting
// documentation of your work. (A mention in a readme or credits, for example.)
// Please leave a comment or email me if you use this work, so I am compelled
// to produce more of its kind.
// 2010 - Phillip Spiess

using System.Collections.Generic;
using System.Drawing;
using System;

public class MarchingSquare
{
	// A simple enumeration to represent the direction we just moved, and the direction we will next move.
	private enum StepDirection
	{
		None,
		Up,
		Left,
		Down,
		Right
	}

	class EncounteredState
	{
		private Point point;

		public Point Point
		{
			get { return point;}
			set { point = value;}
		}
	
		private StepDirection choice;

		public StepDirection Choice
		{
			get { return choice;}
			set { choice = value;}
		}

		public EncounteredState(Point point, StepDirection choice)
		{
			this.point = point;
			this.choice = choice;
		}
	}

	// Some variables to make our function calls a little smaller, probably shouldn't expose these to the public though.

	// Holds the color information for our texture.
	private Color[] colorData;

	// Our texture.
	private Bitmap texture;

	private StepDirection previousStep;

	// Store previous direction choices on iffy states for later comparison.
	private List<EncounteredState> encounters;

	// Our next step direction:
	private StepDirection nextStep;

	private static List<KeyValuePair<int, int>> scans = new List<KeyValuePair<int, int>>
	{
		new KeyValuePair<int, int> (0, 0),
		// Right
		new KeyValuePair<int, int> (1, 1),
		new KeyValuePair<int, int> (1, 0),
		new KeyValuePair<int, int> (1, -1),
		// Middle
		new KeyValuePair<int, int> (0, -1),
		new KeyValuePair<int, int> (-1, 1),
		// Left
		new KeyValuePair<int, int> (-1, -1),
		new KeyValuePair<int, int> (-1, 0),
		new KeyValuePair<int, int> (-1, 1),
			
	};

	public MarchingSquare(Bitmap image)
	{
		texture = image;
		Size thisSize = image.Size;
		// Create an array large enough to hold our texture data.
		colorData = new Color[thisSize.Height * thisSize.Width];
		// Get our color information out of the texture and into our traversable array.
		BitmapProcessing.FastBitmap bmp = new BitmapProcessing.FastBitmap(texture);
		bmp.LockImage();
		{
			for (int x = 0; x < thisSize.Width; x++)
			{
				for (int y = 0; y < thisSize.Height; y++)
				{
					Color pixel = bmp.GetPixel(x, y);
					colorData[x + y * thisSize.Width] = pixel;
				}
			}
		}
		bmp.UnlockImage();
	}

	// Takes a texture and returns a list of pixels that define the perimeter of the upper-left most object in that texture, using alpha==0 to determine the boundary.
	public List<PointF> DoMarch(Action<PointF> handler, PointF? startPoint = null)
	{
		encounters = new List<EncounteredState>();
		// Find the start points.
		PointF perimeterStart = (startPoint == null) ? FindStartPoint() : startPoint.Value;
		int sx = (int)Math.Round(perimeterStart.X, MidpointRounding.AwayFromZero);
		int sy = (int)Math.Round(perimeterStart.Y, MidpointRounding.AwayFromZero);
		AvoidIllegalStateWhenUsingKnownGoodPoint(ref sx, ref sy);
		// Return the list of points.
		return WalkPerimeter(sx, sy, handler);
	}

	private void AvoidIllegalStateWhenUsingKnownGoodPoint(ref int sx, ref int sy)
	{
		// Make sure known good start points actually lead to outlines.
		StepDirection firstStep = StepDirection.None;
		foreach (KeyValuePair<int, int> offset in scans)
		{
			int xIndex = sx + offset.Key;
			int yIndex = sy + offset.Value;
			PointF thisPoint = PointF.Empty;
			firstStep = Step(xIndex, yIndex, out thisPoint);
			if (firstStep != StepDirection.None)
			{
				sx = xIndex;
				sy = yIndex;
				break;
			}
		}
	}

	// Finds the first pixel in the perimeter of the image
	public PointF FindStartPoint(Point? startLocation = null)
	{
		int startPixel = 0;
		if (startLocation != null)
			startPixel = startLocation.Value.X + startLocation.Value.Y * texture.Width;
		// Scan along the whole image
		for (int pixel = startPixel; pixel < colorData.Length; pixel++)
		{
			// If the pixel is not entirely transparent we've found a start point.
			if (colorData[pixel].A > 0)
				return new PointF(pixel % texture.Width, pixel / texture.Width);
		}
		// If we get here we've scanned the whole image and found nothing.
		return PointF.Empty;
	}

	// Performs the main while loop of the algorithm
	private List<PointF> WalkPerimeter(int startX, int startY, Action<PointF> handler)
	{
		// Do some sanity checking, so we aren't walking outside the image
		if (startX < 0)
			startX = 0;
		if (startX > texture.Width)
			startX = texture.Width;
		if (startY < 0)
			startY = 0;
		if (startY > texture.Height)
			startY = texture.Height;

		// Set up our return list
		List<PointF> pointList = new List<PointF>();

		// Our current x and y positions, initialized to the init values passed in
		int x = startX;
		int y = startY;

		// The main while loop, continues stepping until we return to our initial points.
		do
		{
			// Evaluate the state, and set up the next direction.
			PointF thisPoint = PointF.Empty;
			Step(x, y, out thisPoint);

			// If our current point is within our image add it to the list of points
			if (thisPoint.X >= 0 && thisPoint.X < texture.Width && thisPoint.Y >= 0 && thisPoint.Y < texture.Height)
			{
				pointList.Add(thisPoint);
				handler(thisPoint);
			}

			switch (nextStep)
			{
				case StepDirection.Up: y--; break;
				case StepDirection.Left: x--; break;
				case StepDirection.Down: y++; break;
				case StepDirection.Right: x++; break;
				default:
					break;
			}
		} while (x != startX || y != startY);
		//
		return pointList;
	}

	// Determines and sets the state of the 4 pixels that represent our current state, and sets our current and previous directions.
	private StepDirection Step(int x, int y, out PointF onLine)
	{
		// Scan our 4 pixel area.
		bool upLeftIsSolid = IsPixelSolid(x - 1, y - 1);
		bool upRightIsSolid = IsPixelSolid(x, y - 1);
		bool downLeftIsSolid = IsPixelSolid(x - 1, y);
		bool downRightIsSolid = IsPixelSolid(x, y);
		previousStep = nextStep;
		// Determine which state the machine is in. State is additively determined by the constant state number of the state wherein the condition is the only condition shows up in alone: (1) only upper left, or (2) only upper right, or (4) only lower right, or (8) only lower left. After this addition, the state will contain a number between 0 (0b0000) and 15 (0b1111) representing the state.
		int state = 0;
		if (upLeftIsSolid)
			state += 1;
		if (upRightIsSolid)
			state += 2;
		if (downRightIsSolid)
			state += 4;
		if (downLeftIsSolid)
			state += 8;
		// Prepare to send back only something that is actually on the image (prevent overdraw).
		onLine = new PointF(x, y);
		// Determine direction from finite state.
		switch (state)
		{
			case 1:
				nextStep = StepDirection.Up;
				break;
			case 2:
				nextStep = StepDirection.Right;
				break;
			case 3:
				nextStep = StepDirection.Right;
				break;
			case 4:
				nextStep = StepDirection.Down;
				break;
			case 5:
				{
					// By default, this step should go up. However, in a hole, this will circle the hole infinitely, so negate the step.
					EncounteredState encounteredState = encounters.FindLast(item => item.Point.X == x && item.Point.Y == y);
					if (encounteredState != null)
						nextStep = (encounteredState.Choice == StepDirection.Down) ? StepDirection.Up : StepDirection.Down;
					else
					{
						if (previousStep == StepDirection.Right)
							nextStep = StepDirection.Down;
						else
							nextStep = StepDirection.Up;
					}
					//
					encounters.Add(new EncounteredState(new Point(x, y), nextStep));
				}
				//
				break;
			case 6:
				nextStep = StepDirection.Down;
				break;
			case 7:
				nextStep = StepDirection.Down;
				break;
			case 8:
				nextStep = StepDirection.Left;
				break;
			case 9:
				nextStep = StepDirection.Up;
				break;
			case 10:
				{
					// By default, this step should go left. However, in a hole, this will circle the hole infinitely, so negate the step.
					EncounteredState encounteredState = encounters.FindLast(item => item.Point.X == x && item.Point.Y == y);
					if (encounteredState != null)
						nextStep = (encounteredState.Choice == StepDirection.Left) ? StepDirection.Right : StepDirection.Left;
					else
					{
						if (previousStep == StepDirection.Up)
							nextStep = StepDirection.Right;
						else
							nextStep = StepDirection.Left;
					}
					//
					encounters.Add(new EncounteredState(new Point(x, y), nextStep));
				}
				//
				break;
			case 11:
				nextStep = StepDirection.Right;
				break;
			case 12:
				nextStep = StepDirection.Left;
				break;
			case 13:
				nextStep = StepDirection.Up;
				break;
			case 14:
				nextStep = StepDirection.Left;
				break;
			default:
				nextStep = StepDirection.None;
				break;
		}
		return nextStep;
	}

	// Determines if a single pixel is solid (we test against alpha values, you can write your own test if you want to test for a different color).
	private bool IsPixelSolid(int x, int y)
	{
		// Make sure we don't pick a point outside our image boundary!
		if (x < 0 || y < 0 || x >= texture.Width || y >= texture.Height)
			return false;

		// Check the color value of the pixel. If it isn't 100% transparent, it is solid
		if (colorData[x + y * texture.Width].A > 0)
			return true;

		// Otherwise, it's not solid
		return false;
	}
}