using System;
namespace Turtle.Generators {
	public interface IGenerator {
		public int MaxTurns { get; }

		public string Name { get; }
		public string Author { get; }

		public (char displayKey, ConsoleKey inputKey, int rowNum) [] Keys { get; set; }

		/// <summary>
		/// Stateless input validation.
		///
		/// All external hint validation (e.g. hard mode) should be done elsewhere, this should
		/// simply return a dictionary of hints. 
		/// </summary>
		/// <param name="currentInput">The most recently entered input</param>
		/// <returns>Returns all hints, 0 if invalid input.</returns>
		public bool ValidateInput (string currentInput);

		public string GenerateSolution (int seed);
	}
}

