using System;
using System.Text;

namespace Turtle.Graphics {
	public struct Format {
		public Color BackgroundColor { get; set; }
		public Color ForegroundColor { get; set; }

		public bool Bold { get; set; }
		public bool Underline { get; set; }
		public bool Italic { get; set; }

		public StringBuilder GetFormatBuilder()
		{
			StringBuilder sb = new ();

			if (this.BackgroundColor != null) {
				sb.Append (this.BackgroundColor);
			}

			if (this.ForegroundColor.HasValue) {
				sb.Append (this.ForegroundColor);
			}

			if (this.Bold) {
				sb.Append ("1;");
			}

			if (this.Italic) {
				sb.Append ("3;");
			}
		}
	}
	//colorizor9000("Bunny is always right", color: LegacyColor(3))

	public class Color {
		// declarative 
		public ColorMode Mode { get; set; }
		public TerminalMode TerminalMode { get; set; }

		// TrueColor + Rgb6bit
		public int R { get; set; }
		public int G { get; set; }
		public int B { get; set; }

		// Legacy + Gray
		private int basicValue;
		public int BasicValue {
			get {

				return this.basicValue + this.getOffset ();
			}
			set {
				this.basicValue = value;
			}
		}

		public Color(ColorMode mode, TerminalMode termMode, ColorName legacyColorName)
		{
			this.Mode = mode;
			this.TerminalMode = termMode;
			this.BasicValue = (int)legacyColorName;
		}


		private int getOffset () => this.Mode == ColorMode.Grayscale ? 232 : 0;

		public static implicit operator string (Color c) => c.ToString ();

		public override string ToString ()
		{
			int code;

			switch (this.Mode) {
			case ColorMode.Legacy16:
				return String.Format ("{0}8;5;{1};", this.TerminalMode, this.basicValue);
			case ColorMode.Rgb6bit:
				code = 36 * this.R + 6 * this.G + this.B + 16;
				return String.Format ("{0}8;5;{0};", this.TerminalMode, code);
			case ColorMode.Grayscale:
				code = this.basicValue + this.getOffset ();
				return String.Format ("{0}8;5;{0};", this.TerminalMode, code);
			case ColorMode.TrueColor:
				return String.Format ("{0}8;2;{1};{2};{3};", this.TerminalMode, this.R, this.G, this.B);
			default:
				throw new Exception ("If this were possible, I would be a moron");
			}
		}
	}

	public enum ColorName {
		Black = 0,
		Red = 1,
		Green = 2,
		Yellow = 3,
		Blue = 4,
		Magenta = 5,
		Cyan = 6,
		White = 7,
		BrightBlack = 8,
		BrightRed = 9,
		BrightGreen = 10,
		BrightYellow = 11,
		BrightBlue = 12,
		BrightMagenta = 13,
		BrightCyan = 14,
		BrightWhite = 15,
	}

	public enum ColorMode {
		Legacy16,
		Rgb6bit,
		Grayscale,
		TrueColor
	}

	public enum TerminalMode {
		Foreground = 3,
		Background = 4
	}
}

