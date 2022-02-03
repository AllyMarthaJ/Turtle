using System;
namespace Turtle.Generators {
	public interface IGenerator {
		public KeyboardRow[] KeyboardRows { get; }

		/// <summary>
		/// Stateless input validation.
		///
		/// All external hint validation (e.g. hard mode) should be done elsewhere, this should
		/// simply return a dictionary of hints. 
		/// </summary>
		/// <param name="currentInput">The most recently entered input</param>
		/// <returns>Returns all hints, 0 if invalid input.</returns>
		public Dictionary<int, HintMode> ValidateInput (string currentInput);

		public string GenerateSolution (long seed);
	}
}

