using System;
namespace Turtle.Graphics {
	public class ConsoleHelpers {
		public const string ANSI_PREFIX = "\x1b[";

		public static void WriteInPlace (FormattedString input, Offset offset)
		{
			Offset initOffset = new Offset (offset.X, offset.Y);

			string formattedInput = input;
			var rawLines = input.RawValue.Split (Environment.NewLine);
			var fmLines = formattedInput.Split (Environment.NewLine);

			for (int i = 0; i < fmLines.Length; i++) {
				var offsetLine = InPlaceOffset (fmLines [i], offset);

				Console.Write (offsetLine);

				offset = new Offset (-rawLines [i].Length, 1);
			}

			var ret = InPlaceOffset ("", new Offset (
					-rawLines [rawLines.Length - 1].Length - initOffset.X,
					-rawLines.Length + 1 - initOffset.Y))
				;
			Console.Write (ret);
		}

		public static string InPlaceOffset (string str, Offset offset)
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

