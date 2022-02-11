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
								foregroundColor: new Color (TerminalMode.Foreground, 24), bold: true);
		private readonly Format goodPosBlockFormat = new (backgroundColor: new Color (TerminalMode.Background, ColorName.BrightGreen),
							       foregroundColor: new Color (TerminalMode.Foreground, ColorName.Black), bold: true);
		private readonly Format badPosBlockFormat = new (backgroundColor: new Color (TerminalMode.Background, ColorName.BrightYellow),
							       foregroundColor: new Color (TerminalMode.Foreground, ColorName.Black), bold: true);
		private readonly Format badCharBlockFormat = new (backgroundColor: new Color (TerminalMode.Background, ColorName.BrightRed),
							       foregroundColor: new Color (TerminalMode.Foreground, ColorName.Black), bold: true);

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
						if (wordBuilder.Length >= this.Solution.Length) {
							continue;
						}

						wordBuilder.Append (Char.ToLower (input.KeyChar));
					} else if (input.Key == ConsoleKey.Delete || input.Key == ConsoleKey.Backspace) {
						if (wordBuilder.Length == 0) {
							continue;
						}
						wordBuilder.Remove (wordBuilder.Length - 1, 1);
					} else if (input.Key == ConsoleKey.Enter) {
						// submit word
						var wordResult = this.ValidateAndUpdateUpstream (wordBuilder.ToString ());

						if (wordResult.Length == 0) {
							// only clear the buffer if we've written a full word
							if (wordBuilder.Length != this.Solution.Length) {
								continue;
							}
							wordBuilder.Clear ();

						} else {
							// we've got a valid word!
							validWord = true;

							if (wordBuilder.ToString().Equals(this.Solution, StringComparison.OrdinalIgnoreCase)) {
								hasWon = true;
							}

							// chuck the check in the gamestate
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

					int wcOffsetX = BLOCK_WIDTH / 2;
					int wcOffsetY = BLOCK_HEIGHT / 2;

					if (this.GameState [y] == null) {
						ConsoleHelpers.WriteInPlace (emptyBlock, new Offset (blockX, blockY));

						// drawing the current turn
						if (y == turn - 1 && x < currentWord.Length) {
							FormattedString wc = currentWord [x].ToString ();
							wc.Formats.Add (new Regex (".*"), emptyBlockFormat);

							ConsoleHelpers.WriteInPlace (wc, new Offset (blockX + wcOffsetX, blockY + wcOffsetY));
						}
					} else {
						// drawing all previous results
						FormattedString block = this.block;
						block.Formats.Add (new Regex (".*"), getHintBlock (this.GameState [y] [x].Hint));

						ConsoleHelpers.WriteInPlace (block, new Offset (blockX, blockY));

						FormattedString wc = this.GameState [y] [x].Entry.ToString ();
						wc.Formats.Add (new Regex (".*"), getHintBlock (this.GameState [y] [x].Hint));

						ConsoleHelpers.WriteInPlace (wc, new Offset (blockX + wcOffsetX, blockY + wcOffsetY));
					}
				}
			}
		}
			
		private string getBlock (int width, int height)
		{
			StringBuilder sb = new StringBuilder ();

			string line = new(' ', width);

			for (int i = 0; i < height; i++) {
				sb.AppendLine (line);
			}

			return sb.ToString ();
		}

		private Format getHintBlock (HintMode c) => c switch {
			HintMode.BadCharacter => this.badCharBlockFormat,
			HintMode.BadPosition => this.badPosBlockFormat,
			HintMode.GoodPosition => this.goodPosBlockFormat,
			_ => this.emptyBlockFormat
		};
	}
}

