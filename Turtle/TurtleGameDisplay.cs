using System;
using System.Text;
using System.Text.RegularExpressions;
using Turtle.Env;
using Turtle.Generators;
using Turtle.Graphics;

namespace Turtle {
	public partial class TurtleGame {
		private string block = String.Empty;
		Random rnd = new ();

		private readonly Format emptyBlockFormat = new (backgroundColor: VARS.DEFAULT_BACK, foregroundColor: VARS.DEFAULT_FORE, bold: true);
		private readonly Format goodPosBlockFormat = new (backgroundColor: VARS.GOOD_CHAR_BACK, foregroundColor: VARS.GOOD_CHAR_FORE, bold: true);
		private readonly Format badPosBlockFormat = new (backgroundColor: VARS.BAD_POS_BACK, foregroundColor: VARS.BAD_POS_FORE, bold: true);
		private readonly Format badCharBlockFormat = new (backgroundColor: VARS.BAD_CHAR_BACK, foregroundColor: VARS.BAD_CHAR_FORE, bold: true);

		public int Play (Offset offset)
		{
			int turn = 1;
			bool hasWon = false;

			// render
			int keyboardOffset = VARS.SHOW_KEYBOARD ? drawKeyboard (offset) + 1 : 0;
			var finalOffset = new Offset (offset.X, offset.Y + keyboardOffset);

			int height = drawGrid (turn, String.Empty, finalOffset);

			while (turn <= currentGenerator.MaxTurns && !hasWon) {
				// handle character input
				bool validWord = false;
				StringBuilder wordBuilder = new ();

				while (!validWord) {
					var result = handleInput (ref wordBuilder, turn);

					validWord = result.validWord;
					hasWon = result.hasWon;

					// render
					keyboardOffset = VARS.SHOW_KEYBOARD ? drawKeyboard (offset) : 0;
					drawGrid (turn, wordBuilder.ToString (), finalOffset);
				}

				turn++;
			}

			FormattedString gameMessage = hasWon ?
				"[Congratulations, you've won!]" :
				$"[Better luck next time. The word was {this.Solutions [0]}.]";
			gameMessage.Formats.Add (new Regex ("[\\[\\]]"), new Format (
					faint: true
				));
			gameMessage.Draw (offset.X, height + 1);

			return hasWon ? turn - 1 : -1;
		}

		private int drawKeyboard (Offset offset)
		{
			var rows = this.currentGenerator.Keys.Select (k => k.rowNum).Distinct ().ToArray ();

			FormattedString fmtKey = new ("Keys   :");
			fmtKey.Draw (offset.X, offset.Y);

			for (int i = 0; i < rows.Length; i++) {
				// where y is rows[i]
				var keys = this.currentGenerator.Keys.Where (k => k.rowNum == rows [i]).ToArray ();

				for (int x = 0; x < keys.Length; x++) {
					FormattedString key = $"{{{keys [x].displayKey.ToString ()}}}";
					if (this.GoodPositions.Contains (keys [x].displayKey)) {
						key.Formats.Add (new Regex ("\\w"), new Format (
							backgroundColor: VARS.GOOD_CHAR_BACK,
							foregroundColor: VARS.GOOD_CHAR_FORE
							));
					} else if (this.MustContain.Contains (keys [x].displayKey)) {
						key.Formats.Add (new Regex ("\\w"), new Format (
							backgroundColor: VARS.BAD_POS_BACK,
							foregroundColor: VARS.BAD_POS_FORE
							));
					} else if (this.NeverContains.Contains (keys [x].displayKey)) {
						key.Formats.Add (new Regex ("\\w"), new Format (
							backgroundColor: VARS.BAD_CHAR_BACK,
							foregroundColor: VARS.BAD_CHAR_FORE
							));
					}
					key.Formats.Add (new Regex ("[{}]"), new Format (
							faint: true
						));

					key.Draw (4 * x + offset.X + 10, rows [i] + offset.Y);
				}
			}

			return rows.Length;
		}


		public int DrawStyledTurtle (Offset offset)
		{
			var author = this.currentGenerator.Author.Length < 34 ? this.currentGenerator.Author :
				this.currentGenerator.Author.Substring (0, 34);
			FormattedString turtle = VARS.TITLE
				.Replace ("{author}", author + new string (' ', 36 - author.Length))
				.Replace ("{generator}", this.currentGenerator.Name);

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

		public string GetGameStateResult (int turn)
		{
			var stateBuilder = new StringBuilder ();
			var finalTurn = turn == -1 ? "X" : turn.ToString ();
			stateBuilder.AppendLine ($"Turtle {this.Seed} ({this.currentGenerator.Name}) {finalTurn}/{this.currentGenerator.MaxTurns}");
			stateBuilder.AppendLine ();

			foreach (var gameLine in this.GameState) {
				if (gameLine == null) continue;

				var line = gameLine.Select (l => this.getEmojiHint (l.Hint));

				stateBuilder.AppendLine (String.Join ("", line));
			}

			return stateBuilder.ToString ();
		}

		private (bool validWord, bool hasWon) handleInput (ref StringBuilder wordBuilder, int turn)
		{
			// user input
			var input = Console.ReadKey (true);

			bool validWord = false, hasWon = false;

			// check if valid key
			if (this.currentGenerator.Keys.Where (k => k.inputKey == input.Key).Count () > 0) {
				if (wordBuilder.Length >= this.Solutions [0].Length) {
					return (validWord, hasWon);
				}

				wordBuilder.Append (Char.ToLower (input.KeyChar));
			} else if (input.Key == ConsoleKey.Delete || input.Key == ConsoleKey.Backspace) {
				if (wordBuilder.Length == 0) {
					return (validWord, hasWon);
				}

				wordBuilder.Remove (wordBuilder.Length - 1, 1);
			} else if (input.Key == ConsoleKey.Escape) {
				Environment.Exit (0);
			} else if (input.Key == ConsoleKey.Enter) {
				// submit word
				var wordResult = this.ValidateAndUpdateUpstream (wordBuilder.ToString ());

				if (wordResult.Length == 0) {
					// only clear the buffer if we've written a full word
					if (wordBuilder.Length != this.Solutions [0].Length) {
						return (validWord, hasWon);
					}
					wordBuilder.Clear ();

				} else {
					// we've got a valid word!
					validWord = true;

					foreach (var solution in this.Solutions) {
						if (wordBuilder.ToString ().Equals (solution, StringComparison.OrdinalIgnoreCase)) {
							hasWon = true;
						}
					}

					if (hasWon) {
						// multiple solutions, switch to primary solution
						wordResult = this.CompareUpstream (this.Solutions [0]);
					}

					// chuck the check in the gamestate
					this.GameState [turn - 1] = wordResult;
				}
			}

			return (validWord, hasWon);
		}

		// TODO: Instead of rendering entire grid in parts, a StringBuilder will store the entire grid.
		private int drawGrid (int turn, string currentWord, Offset stdOffset)
		{
			if (String.IsNullOrWhiteSpace (this.block))
				this.block = getBlock (VARS.BLOCK_WIDTH, VARS.BLOCK_HEIGHT);

			FormattedString emptyBlock = this.block;
			emptyBlock.Formats.Add (new Regex (".*"), emptyBlockFormat);

			for (int y = 0; y < this.currentGenerator.MaxTurns; y++) {
				for (int x = 0; x < this.Solutions [0].Length; x++) {
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

			return this.currentGenerator.MaxTurns * (VARS.BLOCK_HEIGHT + 1) + stdOffset.Y;
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

		private string getEmojiHint (HintMode c) => c switch {
			HintMode.BadCharacter => "⬛",
			HintMode.BadPosition => "🟨",
			HintMode.GoodPosition => "🟩",
			_ => "⬛"
		};
	}
}

