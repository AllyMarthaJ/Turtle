using System;
using System.Text;
using Turtle.Env;

namespace Turtle.Graphics {
	public static class ConsoleHelpers {
		private static bool alternate = false;
		public static bool AlternateScreen {
			get {
				return alternate;
			}
			set {
				StringBuilder sb = new ();
				sb.Append (VARS.ANSI_PREFIX);
				sb.Append ("?1049");
				sb.Append (value ? "h" : "l");
				Console.Write (sb.ToString ());

				alternate = value;
			}
		}

		public static void WriteInPlace (FormattedString input, Offset offset)
		{
			Offset initOffset = new Offset (offset.X, offset.Y);

			string formattedInput = input;
			var rawLines = input.RawValue.Split (Environment.NewLine);
			var fmLines = formattedInput.Split (Environment.NewLine);

			for (int i = 0; i < fmLines.Length; i++) {
				var offsetLine = GetPositionedString (fmLines [i], offset);

				Console.Write (offsetLine);

				offset = new Offset (-rawLines [i].Length, 1);
			}

			var ret = GetPositionedString ("", new Offset (
					-rawLines [rawLines.Length - 1].Length - initOffset.X,
					-rawLines.Length + 1 - initOffset.Y))
				;
			Console.Write (ret);
		}

		public static void Draw (this FormattedString input, int x = 0, int y = 0)
		{
			WriteInPlace (input, new Offset (x, y));
		}

		public static string GetPositionedString (string str, Offset offset)
		{
			foreach (var offsetBuilder in offset.GetOffsetBuilders ()) {
				offsetBuilder.Insert (0, ConsoleHelpers.ANSI_PREFIX);
				offsetBuilder.Append (str);
				str = offsetBuilder.ToString ();
			}

			return str;
		}
	}
}

