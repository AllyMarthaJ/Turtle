using System;
using System.Text.RegularExpressions;

namespace Turtle.Graphics {

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

	public class Color {
		// declarative 
		public ColorMode Mode { get; set; }
		public TerminalMode TerminalMode { get; set; }

		// TrueColor + Rgb6bit (0-255 or 0-5)
		public int R { get; set; }
		public int G { get; set; }
		public int B { get; set; }

		// Legacy + Gray (0-23)
		private int basicValue;
		public int BasicValue {
			get {

				return this.basicValue + this.getOffset ();
			}
			set {
				this.basicValue = value;
			}
		}

		public Color (TerminalMode termMode, ColorName legacyColorName)
		{
			this.Mode = ColorMode.Legacy16;
			this.TerminalMode = termMode;
			this.basicValue = (int)legacyColorName;
		}

		public Color (TerminalMode termMode, int r, int g, int b, bool isTrueColor = true)
		{
			this.Mode = isTrueColor ? ColorMode.TrueColor : ColorMode.Rgb6bit;
			this.TerminalMode = termMode;

			this.R = r;
			this.G = g;
			this.B = b;
		}

		public Color (TerminalMode termMode, int grayscale)
		{
			this.Mode = ColorMode.Grayscale;
			this.TerminalMode = termMode;

			this.basicValue = grayscale;
		}


		private int getOffset () => this.Mode == ColorMode.Grayscale ? 232 : 0;

		public override string ToString ()
		{
			int code;

			switch (this.Mode) {
			case ColorMode.Legacy16:
				return String.Format ("{0}8;5;{1};", (int)this.TerminalMode, this.BasicValue);
			case ColorMode.Rgb6bit:
				// 0-5 for all 3 values
				code = 36 * this.R + 6 * this.G + this.B + 16;
				return String.Format ("{0}8;5;{1};", (int)this.TerminalMode, code);
			case ColorMode.Grayscale:
				return String.Format ("{0}8;5;{1};", (int)this.TerminalMode, this.BasicValue);
			case ColorMode.TrueColor:
				return String.Format ("{0}8;2;{1};{2};{3};", (int)this.TerminalMode, this.R, this.G, this.B);
			default:
				throw new Exception ("If this were possible, I would be a moron");
			}
		}

		public static implicit operator string (Color c) => c.ToString ();

		public static Color Parse (TerminalMode mode, string s)
		{
			var m = Regex.Match (s, "([\\w]+)\\((.*?)\\)");
			if (m.Groups.Count < 3)
				throw new ArgumentException ("Invalid color command.");
			var command = m.Groups [1].Value.ToLower ();
			var args = m.Groups [2].Value.Split (",");

			switch (command) {
			case "rgb6bit":
				return new Color (mode, Int32.Parse (args [0]), Int32.Parse (args [1]), Int32.Parse (args [2]), false);
			case "true":
				return new Color (mode, Int32.Parse (args [0]), Int32.Parse (args [1]), Int32.Parse (args [2]), true);
			case "gray":
				return new Color (mode, Int32.Parse (args [0]));
			case "legacy":
				return new Color (mode, Enum.Parse<ColorName> (args [0]));
			default:
				throw new Exception ("Invalid color command");
			}
		}
	}
}

