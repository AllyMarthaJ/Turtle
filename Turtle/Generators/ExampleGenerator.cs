using System;
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

		public int MaxTurns { get; } = 6;

		public (char displayKey, ConsoleKey inputKey, int rowNum) [] Keys { get; set; }

		public string Name { get; } = "Example Generator";
		public string Author { get; } = "Ally";

		public ExampleGenerator ()
		{
			this.setKeys ();
		}

		private void setKeys ()
		{
			this.Keys = new (char displayKey, ConsoleKey inputKey, int rowNum) [26];

			for (int i = 0; i < 26; i++) {
				this.Keys [i] = ((char)('a' + i), Enum.Parse<ConsoleKey> (((char)('A' + i)).ToString ()), i / 8);
			}
		}

		public string GenerateSolution (int seed)
		{
			var generator = new Random (seed);
			//Console.WriteLine ("Bunny is always right. I should listen to her more often");
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

