using System;
using System.Text;

namespace Turtle.Graphics {
	public struct Format {
		public Color BackgroundColor { get; set; }
		public Color ForegroundColor { get; set; }

		public bool Bold { get; set; }
		public bool Underline { get; set; }
		public bool Italic { get; set; }
		public bool Faint { get; set; }
		public bool Reverse { get; set; }
		public bool Blink { get; set; }
		public bool ResetAfter { get; set; }

		public Format(Color backgroundColor = null,
			      Color foregroundColor = null,
			      bool bold = false,
			      bool underline = false,
			      bool italic = false,
			      bool faint = false,
			      bool reverse = false,
			      bool blink = false,
			      bool resetAfter = true)
		{
			this.BackgroundColor = backgroundColor;
			this.ForegroundColor = foregroundColor;
			this.Bold = bold;
			this.Underline = underline;
			this.Italic = italic;
			this.Faint = faint;
			this.Reverse = reverse;
			this.Blink = blink;
			this.ResetAfter = resetAfter;
		}

		public StringBuilder GetFormatBuilder()
		{
			StringBuilder sb = new ();

			if (this.Bold) sb.Append ("1;");
			if (this.Italic) sb.Append ("3;");
			if (this.Faint) sb.Append ("2;");
			if (this.Underline) sb.Append ("4;");
			if (this.Blink) sb.Append ("5;");
			if (this.Reverse) sb.Append ("7;");
			if (this.BackgroundColor != null) sb.Append (this.BackgroundColor);
			if (this.ForegroundColor != null) sb.Append (this.ForegroundColor);

			if (sb.Length == 0) {
				sb.Append ("0;");
			}
			
			sb.Remove (sb.Length - 1, 1);
			sb.Append ('m');

			return sb;
		}
	}
}

