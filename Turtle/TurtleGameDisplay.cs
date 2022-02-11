using System;
using System.Text;
using System.Text.RegularExpressions;
using Turtle.Generators;
using Turtle.Graphics;

namespace Turtle {
	public partial class TurtleGame {
		private const int BLOCK_WIDTH = 5;
		private const int BLOCK_HEIGHT = 3;
		private string block = String.Empty;
		Random rnd = new Random ();

		public void Play ()
		{
			int turn = 1;
			bool hasWon = false;

			while (turn <= currentGenerator.MaxTurns && !hasWon) {
				// render
				drawGrid (turn);
				// user input
				Console.ReadKey (true);

				turn++;
			}
		}

		private void drawGrid (int turn)
		{
			if (String.IsNullOrWhiteSpace (this.block))
				this.block = getBlock (BLOCK_WIDTH, BLOCK_HEIGHT);

			var emptyBlockFormat = new Format (backgroundColor: new Color (TerminalMode.Background, 4),
							   foregroundColor: new Color (TerminalMode.Foreground, 24));

			FormattedString emptyBlock = block;
			emptyBlock.Formats.Add (new Regex (".*"), emptyBlockFormat);

			for (int y = 0; y < this.currentGenerator.MaxTurns; y++) {
				for (int x = 0; x < this.Solution.Length; x++) {
					int blockX = x * (BLOCK_WIDTH + 2);
					int blockY = y * (BLOCK_HEIGHT + 1);

					ConsoleHelpers.WriteInPlace (emptyBlock, new Offset (blockX, blockY));

					FormattedString h = ".";
					h.Formats.Add (new Regex (".*"), emptyBlockFormat);

					ConsoleHelpers.WriteInPlace (h, new Offset (blockX + 2, blockY + 1));
				}
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

