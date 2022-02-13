using System;
using Turtle.Generators;

namespace Turtle {
	public partial class TurtleGame {
		protected readonly IGenerator currentGenerator;

		public int Seed { get; }

		/// <summary>
		/// Solutions works as follows: The primary solution, Solutions [0],
		/// gives information about hinting and so forth. The remainder of these
		/// are simply accepted solutions in the game winning logic.
		/// </summary>
		public string [] Solutions { get; }

		public bool HardMode { get; set; }

		// gamestate
		public char [] GoodPositions { get; }
		public List<char> MustContain { get; }
		public List<char> NeverContains { get; }
		public CharHint [] [] GameState { get; }

		public TurtleGame (IGenerator generator, bool hard = false) :
			this (generator, (int)(DateTime.Today.ToUniversalTime () - DateTime.UnixEpoch).TotalSeconds, hard)
		{ }

		public TurtleGame (IGenerator generator, int seed, bool hard = false)
		{
			this.currentGenerator = generator;

			this.Seed = seed;
			this.Solutions = this.currentGenerator.GenerateSolutions (seed);
			this.HardMode = hard;

			this.GoodPositions = new char [this.Solutions [0].Length];
			this.MustContain = new List<char> ();
			this.NeverContains = new List<char> ();
			this.GameState = new CharHint [this.currentGenerator.MaxTurns] [];
		}

		public CharHint [] ValidateAndUpdateUpstream (string input)
		{
			// length validation
			if (input.Length != this.Solutions [0].Length) {
				return Array.Empty<CharHint> ();
			}

			// generator input validation
			if (!this.currentGenerator.ValidateInput (input)) {
				return Array.Empty<CharHint> ();
			}

			var hints = this.CompareUpstream (input);

			// hardmode validation
			if (!UpdateWithHardmode (input, hints)) {
				return Array.Empty<CharHint> ();
			}

			return hints;
		}

		public CharHint [] CompareUpstream (string input)
		{
			if (input.Length != Solutions [0].Length) {
				return Array.Empty<CharHint> ();
			}

			// a position-insensitive copy of our solution
			var deductSolution = new List<char> (this.Solutions [0]);

			// a position-sensitive record of our hints
			var hints = new CharHint [this.Solutions [0].Length];

			for (int i = 0; i < input.Length; i++) {
				if (this.Solutions [0] [i] == input [i] && deductSolution.Contains (input [i])) {
					// 1. check equal position
					// 2. going from left-to-right means we might have already
					// used a letter but in the wrong place, so we don't want to give the impression
					// of duplicate letters. ensure we haven't used our letter already
					hints [i] = new CharHint (input [i], HintMode.GoodPosition);
					deductSolution.Remove (input [i]);
				} else if (deductSolution.Contains (input [i])) {
					// our position-insensitive solution contains it, but
					// it's not in the right position (covered by previous case)
					hints [i] = new CharHint (input [i], HintMode.BadPosition);
					deductSolution.Remove (input [i]);
				} else {
					// we don't know what this is at all!
					hints [i] = new CharHint (input [i], HintMode.BadCharacter);
				}
			}

			return hints;
		}

		public bool UpdateWithHardmode (string input, CharHint [] hints)
		{
			// validation on hardmode
			// NOTE: **we used to have an equivalent deny list**
			// reasons we don't do this: tracking duplicate letters, tracking duplicate inputs.
			if (this.HardMode) {
				for (int i = 0; i < hints.Length; i++) {
					if (this.GoodPositions [i] != '\0' && this.GoodPositions [i] != hints [i].Entry)
						return false;

					foreach (var l in this.MustContain) {
						if (!input.Contains (l))
							return false;
					}
				}
			}

			// updating our tracked records with hardmode
			for (int i = 0; i < hints.Length; i++) {
				switch (hints [i].Hint) {
				case HintMode.BadPosition:
					if (!this.MustContain.Contains (hints [i].Entry))
						this.MustContain.Add (hints [i].Entry);
					break;
				case HintMode.GoodPosition:
					this.GoodPositions [i] = hints [i].Entry;
					break;
				case HintMode.BadCharacter:
					if (!this.NeverContains.Contains (hints [i].Entry))
						this.NeverContains.Add (hints [i].Entry);
					break;
				}
			}

			return true;
		}
	}
}

