using System;
namespace Turtle.Graphics
{
	public static class AnsiConsole
	{
		private static char currentEncoding = '\x1b';

		private static char [] encodings = new [] { '\u001B', '\x1B' };

		public static void SetAnsiEncoding (AnsiEncoding encoding)
		{
			currentEncoding = encodings [(int)encoding];
		}

		public static void Write(string value)
		{
			var lines = value.Split (Environment.NewLine);
		}
	}

	public enum AnsiEncoding {
		u0001B,
		x1B,
		e
	}
}

