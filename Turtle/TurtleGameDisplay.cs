using System;
using System.Text;
using System.Text.RegularExpressions;
using Turtle.Env;
using Turtle.Generators;
using Turtle.Graphics;

namespace Turtle {
	public partial class TurtleGame {
		private string block = String.Empty;
		Random rnd = new Random ();

		private readonly Format emptyBlockFormat = new (backgroundColor: new Color (TerminalMode.Background, 4),
								foregroundColor: new Color (TerminalMode.Foreground, 24), bold: true);
		private readonly Format goodPosBlockFormat = new (backgroundColor: new Color (TerminalMode.Background, 3, 4, 1, false),
							       foregroundColor: new Color (TerminalMode.Foreground, ColorName.Black), bold: true);
		private readonly Format badPosBlockFormat = new (backgroundColor: new Color (TerminalMode.Background, 5, 4, 2, false),
							       foregroundColor: new Color (TerminalMode.Foreground, ColorName.Black), bold: true);
		private readonly Format badCharBlockFormat = new (backgroundColor: new Color (TerminalMode.Background, 5, 3, 3, false),
							       foregroundColor: new Color (TerminalMode.Foreground, ColorName.Black), bold: true);

		public bool Play (Offset offset)
		{
			int turn = 1;
			bool hasWon = false;

			// render
			drawGrid (turn, String.Empty, offset);

			while (turn <= currentGenerator.MaxTurns && !hasWon) {
				// handle character input
				bool validWord = false;
				StringBuilder wordBuilder = new ();

				while (!validWord) {
					var result = handleInput (ref wordBuilder, turn);

					validWord = result.validWord;
					hasWon = result.hasWon;

					// render
					drawGrid (turn, wordBuilder.ToString (), offset);
				}

				turn++;
			}

			return hasWon;
		}

		public int DrawKeyboard(Offset offset)
		{
			var rows = this.currentGenerator.Keys.Select (k => k.rowNum).Distinct ().ToArray ();

			FormattedString fmtKey = new ("Keys   :");
			fmtKey.Formats.Add (new Regex ("[\\[\\]]"), new Format (
					faint: true
				));
			fmtKey.Draw (offset.X, offset.Y);

			for (int i = 0; i < rows.Length; i++) {
				// where y is rows[i]
				var keys = this.currentGenerator.Keys.Where (k => k.rowNum == rows [i]).ToArray();

				for (int x = 0; x < keys.Length; x++) {
					fmtKey.RawValue = $"[{keys [x].displayKey.ToString()}]";
					fmtKey.Draw (4 * x + offset.X + 9, rows [i] + offset.Y);
				}
			}

			return rows.Length;
		}


		public int DrawStyledTurtle (Offset offset)
		{
			FormattedString turtle = VARS.TITLE;

			turtle.Formats.Add (new Regex ("(?<![a-zA-Z(:/)])[\\\\/|]"), new Format (
					bold: true,
					foregroundColor: new Color (TerminalMode.Foreground, 5, 3, 5, false)
				));
			turtle.Formats.Add (new Regex ("_"), new Format (
					bold: true,
					foregroundColor: new Color (TerminalMode.Foreground, 4, 3, 5, false),
					underline: true
				));
			turtle.Formats.Add (new Regex ("http(.*)/"), new Format (
					underline: true
				));

			turtle.Draw (offset.X, offset.Y);

			return VARS.TITLE.Split (Environment.NewLine).Length;
		}

		private (bool validWord, bool hasWon) handleInput (ref StringBuilder wordBuilder, int turn)
		{
			// user input
			var input = Console.ReadKey (true);

			bool validWord = false, hasWon = false;

			// check if valid key
			if (this.currentGenerator.Keys.Where (k => k.inputKey == input.Key).Count () > 0) {
				if (wordBuilder.Length >= this.Solution.Length) {
					return (validWord, hasWon);
				}

				wordBuilder.Append (Char.ToLower (input.KeyChar));
			} else if (input.Key == ConsoleKey.Delete || input.Key == ConsoleKey.Backspace) {
				if (wordBuilder.Length == 0) {
					return (validWord, hasWon);
				}

				wordBuilder.Remove (wordBuilder.Length - 1, 1);
			} else if (input.Key == ConsoleKey.Enter) {
				// submit word
				var wordResult = this.ValidateAndUpdateUpstream (wordBuilder.ToString ());

				if (wordResult.Length == 0) {
					// only clear the buffer if we've written a full word
					if (wordBuilder.Length != this.Solution.Length) {
						return (validWord, hasWon);
					}
					wordBuilder.Clear ();

				} else {
					// we've got a valid word!
					validWord = true;

					if (wordBuilder.ToString ().Equals (this.Solution, StringComparison.OrdinalIgnoreCase)) {
						hasWon = true;
					}

					// chuck the check in the gamestate
					this.GameState [turn - 1] = wordResult;
				}
			}

			return (validWord, hasWon);
		}

		// TODO: Instead of rendering entire grid in parts, a StringBuilder will store the entire grid.
		private void drawGrid (int turn, string currentWord, Offset stdOffset)
		{
			if (String.IsNullOrWhiteSpace (this.block))
				this.block = getBlock (VARS.BLOCK_WIDTH, VARS.BLOCK_HEIGHT);

			FormattedString emptyBlock = this.block;
			emptyBlock.Formats.Add (new Regex (".*"), emptyBlockFormat);

			for (int y = 0; y < this.currentGenerator.MaxTurns; y++) {
				for (int x = 0; x < this.Solution.Length; x++) {
					int blockX = x * (VARS.BLOCK_WIDTH + 2) + stdOffset.X;
					int blockY = y * (VARS.BLOCK_HEIGHT + 1) + stdOffset.Y;

					int wcOffsetX = VARS.BLOCK_WIDTH / 2;
					int wcOffsetY = VARS.BLOCK_HEIGHT / 2;

					if (this.GameState [y] == null) {
						emptyBlock.Draw (blockX, blockY);

						// drawing the current turn
						if (y == turn - 1 && x < currentWord.Length) {
							FormattedString wc = currentWord [x].ToString ();
							wc.Formats.Add (new Regex (".*"), emptyBlockFormat);

							wc.Draw (blockX + wcOffsetX, blockY + wcOffsetY);
						}
					} else {
						// drawing all previous results
						FormattedString block = this.block;
						block.Formats.Add (new Regex (".*"), getHintBlock (this.GameState [y] [x].Hint));

						block.Draw (blockX, blockY);

						FormattedString wc = this.GameState [y] [x].Entry.ToString ();
						wc.Formats.Add (new Regex (".*"), getHintBlock (this.GameState [y] [x].Hint));

						wc.Draw (blockX + wcOffsetX, blockY + wcOffsetY);
					}
				}
			}
		}

		private string getBlock (int width, int height)
		{
			StringBuilder sb = new StringBuilder ();

			string line = new (' ', width);

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

