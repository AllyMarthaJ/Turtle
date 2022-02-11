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

		private readonly Format emptyBlockFormat = new (backgroundColor: new Color (TerminalMode.Background, 4),
								foregroundColor: new Color (TerminalMode.Foreground, 24));
		private readonly Format goodPosBlockFormat = new (backgroundColor: new Color (TerminalMode.Background, ColorName.BrightGreen),
							       foregroundColor: new Color (TerminalMode.Foreground, ColorName.Black));
		private readonly Format badPosBlockFormat = new (backgroundColor: new Color (TerminalMode.Background, ColorName.BrightYellow),
							       foregroundColor: new Color (TerminalMode.Foreground, ColorName.Black));
		private readonly Format badCharBlockFormat = new (backgroundColor: new Color (TerminalMode.Background, ColorName.BrightRed),
							       foregroundColor: new Color (TerminalMode.Foreground, ColorName.Black));

		public void Play ()
		{
			int turn = 1;
			bool hasWon = false;

			while (turn <= currentGenerator.MaxTurns && !hasWon) {
				// handle character input
				bool validWord = false;
				StringBuilder wordBuilder = new ();

				drawGrid (turn, wordBuilder.ToString ());

				while (!validWord) {
					// user input
					var input = Console.ReadKey (true);

					// check if valid key
					if (this.currentGenerator.Keys.Where (k => k.inputKey == input.Key).Count () > 0) {
						wordBuilder.Append (Char.ToLower (input.KeyChar));
					} else if ((input.Key == ConsoleKey.Delete || input.Key == ConsoleKey.Backspace) && wordBuilder.Length > 0) {
						wordBuilder.Remove (wordBuilder.Length - 1, 1);
					} else if (input.Key == ConsoleKey.Enter) {
						// submit word
						var wordResult = this.ValidateAndUpdateUpstream (wordBuilder.ToString ());

						if (wordResult.Length == 0) {
							if (wordBuilder.Length == this.Solution.Length) {
								wordBuilder.Clear ();
							}
						} else {
							validWord = true;
							this.GameState [turn - 1] = wordResult;
						}
					}

					// render
					drawGrid (turn, wordBuilder.ToString ());
				}

				turn++;
			}
		}

		private void drawGrid (int turn, string currentWord)
		{
			if (String.IsNullOrWhiteSpace (this.block))
				this.block = getBlock (BLOCK_WIDTH, BLOCK_HEIGHT);


			FormattedString emptyBlock = this.block;
			emptyBlock.Formats.Add (new Regex (".*"), emptyBlockFormat);

			for (int y = 0; y < this.currentGenerator.MaxTurns; y++) {
				for (int x = 0; x < this.Solution.Length; x++) {
					int blockX = x * (BLOCK_WIDTH + 2);
					int blockY = y * (BLOCK_HEIGHT + 1);

					if (this.GameState [y] == null) {
						ConsoleHelpers.WriteInPlace (emptyBlock, new Offset (blockX, blockY));
						if (y == turn - 1 && x < currentWord.Length) {
							FormattedString wc = currentWord [x].ToString ();
							wc.Formats.Add (new Regex (".*"), emptyBlockFormat);

							ConsoleHelpers.WriteInPlace (wc, new Offset (blockX + 2, blockY + 1));
						}
					} else {
						FormattedString block = this.block;
						block.Formats.Add (new Regex (".*"), getHintBlock (this.GameState [y] [x].Hint));

						ConsoleHelpers.WriteInPlace (block, new Offset (blockX, blockY));

						FormattedString wc = this.GameState [y] [x].Entry.ToString ();
						wc.Formats.Add (new Regex (".*"), getHintBlock (this.GameState [y] [x].Hint));

						ConsoleHelpers.WriteInPlace (wc, new Offset (blockX + 2, blockY + 1));
					}
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

		private Format getHintBlock (HintMode c) => c switch {
			HintMode.BadCharacter => this.badCharBlockFormat,
			HintMode.BadPosition => this.badPosBlockFormat,
			HintMode.GoodPosition => this.goodPosBlockFormat
		};
	}
}

