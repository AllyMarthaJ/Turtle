using System;
namespace Turtle.Graphics {
	public class ConsoleHelpers {
		public const string ANSI_PREFIX = "\x1b[";

		public static void WriteInPlace (FormattedString input, Offset offset)
		{
			//var position = Console.GetCursorPosition ();

			//var lines = input.Split (Environment.NewLine);

			//for (int i = 0; i < lines.Length; i++) {
			//	Console.CursorLeft = position.Left;
			//	Console.WriteLine (lines [i]);
			//}

			//Console.CursorTop -= lines.Length;

			string formattedInput = input;
			var rawLines = input.RawValue.Split (Environment.NewLine);
			var fmLines = formattedInput.Split (Environment.NewLine);

			for (int i = 0; i < fmLines.Length; i++) {
				var offsetLine = InPlaceOffset (fmLines [i], offset);

				Console.Write (offsetLine);

				offset = new Offset (-rawLines [i].Length, 1);
			}

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

