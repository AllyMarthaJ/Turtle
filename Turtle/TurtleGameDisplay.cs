using System;
using System.Text;
using System.Text.RegularExpressions;
using Turtle.Generators;
using Turtle.Graphics;

namespace Turtle {
	public partial class TurtleGame {
		private const int BLOCK_WIDTH = 5;
		private const int BLOCK_HEIGHT = 3;
		private string block;
		Random rnd = new Random ();

		public void Play ()
		{
			int turn = 1;
			bool hasWon = false;
			var position = Console.GetCursorPosition ();

			while (turn < currentGenerator.MaxTurns && !hasWon) {
				// render
				drawGrid (turn);
				// user input
				Console.ReadKey (true);
			}
		}

		private void drawGrid (int turn)
		{
			if (String.IsNullOrWhiteSpace (this.block))
				this.block = getBlock (BLOCK_WIDTH, BLOCK_HEIGHT);

			var emptyBlockFormat = new Format (backgroundColor: new Color (TerminalMode.Background, rnd.Next (1, 25)),
							   foregroundColor: new Color (TerminalMode.Foreground, 24));

			FormattedString emptyBlock = block;
			emptyBlock.Formats.Add (new Regex (".*"), emptyBlockFormat);

			for (int j = 0; j < this.currentGenerator.MaxTurns; j++) {
				for (int i = 0; i <= this.Solution.Length; i++) {

					// ConsoleHelpers.WriteInPlace(emptyBlock, new Offset(x: 10, y: 10));
					// ConsoleHelpers.WriteInPlace(emptyBlock, new Position(x: 10)); // or new Position(x: 10, y:10);
					ConsoleHelpers.WriteInPlace (emptyBlock, new Offset(i * (BLOCK_WIDTH + 2), j * (BLOCK_HEIGHT + 1)));

					//FormattedString h = "h";
					//h.Formats.Add (new Regex (".*"), emptyBlockFormat);

				}

				//if (j == turn - 1) {
				//	Console.CursorTop++;
				//	Console.Write (" < ");
				//	Console.CursorTop--;
				//}
			}
		}

		private string getBlock (int width, int height)
		{
			StringBuilder sb = new StringBuilder ();

			string line = new string (' ', width);

			for (int i = 0; i < height; i++) {
				sb.AppendLine (line);
			}

			return sb.ToString ();
		}
	}
}

