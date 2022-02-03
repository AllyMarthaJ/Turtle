using System;
using Turtle.Generators;

namespace Turtle {
	public class TurtleGame {
		private IGenerator currentGenerator;
		private Random rnd;

		public Dictionary<KeyMapping, HintMode> AllHints { get; set; }

		public int Seed { get; }

		public TurtleGame (IGenerator generator) : this (generator, (DateTime.UtcNow - DateTime.UnixEpoch).Milliseconds) { }

		public TurtleGame(IGenerator generator, int seed)
		{
			this.currentGenerator = generator;
			this.rnd = new Random (seed);

			this.AllHints = new Dictionary<KeyMapping, HintMode> ();

			this.Seed = seed;
		}
	}
}

