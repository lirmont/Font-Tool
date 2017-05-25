using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace FontTool
{
	public partial class VirtualKeyboard
	{
		// Zoom in/zoom out button handlers and gutter handlers.
		private void zoomInButton_Click(object sender, EventArgs e)
		{
			zoomFactor += 1f;
			//
			forceRedraw();
		}

		private void zoomOutButton_Click(object sender, EventArgs e)
		{
			if (zoomFactor >= 2f)
				zoomFactor -= 1f;
			//
			forceRedraw();
		}

		private void gutterXNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown control = (NumericUpDown)sender;
			if (control != null)
				GutterX = (int)control.Value;
		}

		private void gutterYNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown control = (NumericUpDown)sender;
			if (control != null)
				GutterY = (int)control.Value;
		}

		// Right-hand radio button handlers.
		private void imageRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			forceRedraw();
		}

		private void imageAndPointsRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			forceRedraw();
		}

		private void geometryRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			forceRedraw();
		}

		private void overlapRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			forceRedraw();
		}

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			// Test clipboard.
			pasteToolStripMenuItem.Enabled = Clipboard.ContainsText();
			// Test string.
			clearToolStripMenuItem.Enabled = copyToolStripMenuItem.Enabled = CharactersInString.Count > 0 ? true : false;
		}

		// Copy sub-menu handlers.
		private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
		{
			performDefaultCopyOnString();
		}

		private void utf16ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(UTF16Text, TextDataFormat.UnicodeText);
		}

		private void utfUEscapeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<string> stringParts = new List<string>();
			foreach (ulong code in CharactersInString)
				stringParts.Add(string.Format(@"\u{0:X}", code));
			Clipboard.SetText(string.Join("", stringParts.ToArray()));
		}

		private void utfXEscapeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<string> stringParts = new List<string>();
			foreach (ulong code in CharactersInString)
				stringParts.Add(string.Format(@"\x{0:X}", code));
			Clipboard.SetText(string.Join("", stringParts.ToArray()));
		}

		// Paste sub-menu handlers.
		private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void defaultPasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			performDefaultPasteOnString();
		}

		private void utf16PasteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string s = Clipboard.GetText(TextDataFormat.UnicodeText);
			byte[] bytes = Encoding.Unicode.GetBytes(s);
			foreach (byte b in bytes)
				addCharacterToString((ulong)b);
		}

		private void utfPasteUEscapeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string s = Clipboard.GetText(TextDataFormat.UnicodeText);
			MatchCollection collection = Regex.Matches(s, @"(\\[uU][a-fA-F0-9]*)|(.)");
			foreach (Match match in collection)
			{
				string thisString = match.Value;
				if (thisString.Length == 1)
					addCharacterToString((ulong)thisString[0]);
				else if (thisString.Length > 2)
				{
					string subString = thisString.Remove(0, 2);
					ulong value = Convert.ToUInt64(subString, 16);
					addCharacterToString(value);
				}
			}
		}

		private void utfPasteXEscapeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string s = Clipboard.GetText(TextDataFormat.UnicodeText);
			MatchCollection collection = Regex.Matches(s, @"(\\[xX][a-fA-F0-9][a-fA-F0-9][a-fA-F0-9][a-fA-F0-9])|(.)");
			foreach (Match match in collection)
			{
				string thisString = match.Value;
				if (thisString.Length == 1)
					addCharacterToString((ulong)thisString[0]);
				else if (thisString.Length > 2)
				{
					string subString = thisString.Remove(0, 2);
					ulong value = Convert.ToUInt64(subString, 16);
					addCharacterToString(value);
				}
			}
		}

		// Save.
		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.DoEvents();
			//
			performSaveOnString();
		}

		// Export.
		private void exportToPNGToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "PNG Images|*.png";
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Application.DoEvents();
				// Width, height pass.
				int paddingX, paddingY;
				Bitmap bitmap = getEmptyBitmapForDrawing(out paddingX, out paddingY);
				using (Graphics g = Graphics.FromImage(bitmap))
				{
					int lineIndex = 0;
					translateToLeftOfLine(lineIndex, paddingX, paddingY, bitmap, g);
					// Draw image.
					foreach (ulong id in CharactersInString)
					{
						if (id != '\n')
						{
							if (GeometryDisplayLists.Contains(id))
							{
								DisplayList list = GeometryDisplayLists[id];
								OffsetableUnicodeCharacter offset = list.Character.FinalOffset;
								Bitmap thisBitmap = list.CachedPreviewImage;
								if (thisBitmap != null && thisBitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined)
								{
									// Undo this change later.
									g.TranslateTransform(offset.OffsetX, -offset.OffsetY - thisBitmap.Height);
									g.DrawImage(thisBitmap, Point.Empty);
									g.TranslateTransform(-offset.OffsetX, offset.OffsetY + thisBitmap.Height);
									g.TranslateTransform(offset.OffsetWidth, 0);
								}
								else
									g.TranslateTransform(offset.OffsetX + offset.OffsetWidth, 0);
							}
							else
								g.TranslateTransform(parent.ActiveFontInActiveSize.DefaultWidth, 0);
						} else {
							lineIndex += 1;
							translateToLeftOfLine(lineIndex, paddingX, paddingY, bitmap, g);
						}
					}
				}
				// Get rid of excess transparent pixels.
				bitmap = SupportFunctions.TrimBitmap(bitmap);
				// Save the image.
				bitmap.Save(dlg.FileName);
			}
		}

		private Bitmap getEmptyBitmapForDrawing(out int paddingX, out int paddingY)
		{
			float x = 0, y = 0;
			float runningX = 0;
			int minimumPaddingX = 10, minimumPaddingY = 10;
			paddingX = minimumPaddingX;
			paddingY = minimumPaddingY;
			//
			bool firstLetter = true;
			int lineIndex = 0;
			float verticalLeadIn = 0;
			foreach (ulong id in CharactersInString)
			{
				DisplayList d = null;
				if (GeometryDisplayLists.Contains(id))
					d = GeometryDisplayLists[id];
				if (id == '\n')
				{
					// Line has ended. If line is longer than other lines, x is this line's length.
					if (runningX > x)
						x = runningX;
					// Reset line.
					runningX = 0;
					lineIndex += 1;
					verticalLeadIn = lineIndex * LineHeight;
					firstLetter = true;
				}
				else
				{
					if (d != null)
					{
						DerivedCharacter character = d.Character;
						if (firstLetter)
							paddingX = Math.Max(paddingX, minimumPaddingX + (int)Math.Ceiling(Math.Abs(character.FinalOffset.OffsetX)));
						Bitmap thisBitmap = d.CachedPreviewImage;
						if (thisBitmap != null && thisBitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined)
						{
							y = Math.Max(verticalLeadIn + 2 * Math.Abs(thisBitmap.Height + character.FinalOffset.OffsetY), y);
							runningX += Math.Max(thisBitmap.Width, thisBitmap.Width + character.FinalOffset.OffsetWidth);
						}
						else
						{
							y = Math.Max(verticalLeadIn + 2 * Math.Abs(character.FinalOffset.OffsetY), 0);
							runningX += character.FinalOffset.OffsetWidth;
						}
						//
						runningX += character.FinalOffset.OffsetX;
					}
					else
						runningX += parent.ActiveFontInActiveSize.DefaultWidth;
					//
					firstLetter = false;
				}
			}
			// Line has ended. If line is longer than other lines, x is this line's length.
			if (runningX > x)
				x = runningX;
			// TODO: Figure it out exactly. For now, doubling the size prevents a cut off right edge.
			return new Bitmap((int)Math.Ceiling(x + paddingX * 2) * 2, (int)Math.Ceiling(y + paddingY * 2) * 2);
		}


		private void translateToLeftOfLine(int lineIndex, int paddingX, int paddingY, Bitmap bitmap, Graphics g)
		{
			g.ResetTransform();
			// Shift left and down from top-left corner.
			if (LineHeight > 0)
				g.TranslateTransform(paddingX, paddingY + (lineIndex + 1) * LineHeight);
			else
				g.TranslateTransform(paddingX, bitmap.Height / 2);
		}

		// Clear.
		private void clearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CharactersInString.Clear();
			Letters.Clear();
			Intersections.Clear();
			distanceFromEndOfString = 0;
			lineCount = 1;
			forceRedraw();
		}
	}
}
