using System;
using Turtle.Generators;

namespace Turtle {
	public class TurtleGame {
		private IGenerator currentGenerator;
		private Random rnd;

		public int Seed { get; }

		public string Solution { get; }

		public bool HardMode { get; set; }

		// gamestate
		public char [] GoodPositions { get; }
		public List<char> MustContain { get; }
		public List<char> NeverContain { get; }

		public TurtleGame (IGenerator generator, bool hard = false) :
			this (generator, (DateTime.UtcNow - DateTime.UnixEpoch).Milliseconds, hard)
		{ }

		public TurtleGame (IGenerator generator, int seed, bool hard = false)
		{
			this.currentGenerator = generator;
			this.rnd = new Random (seed);

			this.Seed = seed;
			this.Solution = this.currentGenerator.GenerateSolution (seed);
			this.HardMode = hard;

			this.GoodPositions = new char [this.Solution.Length];
			this.MustContain = new List<char> ();
			this.NeverContain = new List<char> ();
		}

		public HintMode [] ValidateAndUpdateUpstream (string input)
		{
			var hintResponse = this.currentGenerator.ValidateInput (input);

			if (hintResponse.Length == 0) {
				return Array.Empty<HintMode> ();
			}

			if (this.HardMode) {
				for (int i = 0; i < hintResponse.Length; i++) {
					if (this.GoodPositions [i] != hintResponse [i].Entry)
						return Array.Empty<HintMode> ();
					if (this.NeverContain.Contains (hintResponse [i].Entry))
						return Array.Empty<HintMode> ();
				}

				for (int i = 0; i < hintResponse.Length; i++) {
					switch (hintResponse [i].Hint) {
					case HintMode.BadCharacter:
						if (!this.NeverContain.Contains (hintResponse [i].Entry))
							this.NeverContain.Add (hintResponse [i].Entry);
						break;
					case HintMode.BadPosition:
						if (!this.MustContain.Contains (hintResponse [i].Entry))
							this.MustContain.Add (hintResponse [i].Entry);
						break;
					case HintMode.GoodPosition:
						this.GoodPositions [i] = hintResponse [i].Entry;
						break;
					}
				}
			}

			return hintResponse.Select (e => e.Hint).ToArray();
		}
	}
}

