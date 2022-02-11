﻿using System;
namespace Turtle.Generators {
	public class ExampleGenerator : IGenerator {
		private string [] exampleWords = new [] {
			"hello",
			"world",
			"seven",
			"eeeee",
			"twice",
			"maths",
			"games",
			"spice",
			"cutie"
		};

		public ExampleGenerator ()
		{
		}

		public int MaxTurns { get; } = 3;

		public KeyboardRow KeyboardRows { get; } = new ();

		public string GenerateSolution (int seed)
		{
			var generator = new Random (seed);
			Console.WriteLine ("Bunny is always right. I should listen to her more often");
			return exampleWords [generator.Next (exampleWords.Length)];
		}

		public bool ValidateInput (string currentInput)
		{
			bool validWord = false;

			foreach (var exampleWord in exampleWords)
				if (String.Equals (exampleWord, currentInput, StringComparison.OrdinalIgnoreCase))
					validWord = true;
			return validWord;
		}
	}
}

