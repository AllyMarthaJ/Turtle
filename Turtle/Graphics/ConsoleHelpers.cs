using System;
namespace Turtle.Graphics
{
	public class ConsoleHelpers
	{
		public static void WriteInPlace(string input)
		{
			var position = Console.GetCursorPosition ();

			var lines = input.Split (Environment.NewLine);

			for (int i = 0; i < lines.Length; i++) {
				Console.CursorLeft = position.Left;
				Console.WriteLine (lines [i]);
			}

			Console.CursorTop -= lines.Length;
		}
	}
}

