using Turtle;
using Turtle.Generators;

public class Program {
	public static void Main (string [] args)
	{
		var wonGame = false;
		var turns = 1;
		var gen = new ExampleGenerator ();
		var game = new TurtleGame (gen);
		game.HardMode = true;
		var bc = Console.BackgroundColor;
		//var fc = Console.ForegroundColor;
		Console.WriteLine (game.Solution);
		while (!wonGame || turns < gen.MaxTurns) {
			var input = Console.ReadLine ();
			var validInput = game.ValidateAndUpdateUpstream (input);
			foreach (var h in validInput) {
				var newBc = h == HintMode.BadCharacter ? ConsoleColor.Red :
					    h == HintMode.BadPosition ? ConsoleColor.Yellow : ConsoleColor.Green;
				Console.BackgroundColor = newBc;
				Console.Write ("#");
				Console.BackgroundColor = bc;
			}
			Console.WriteLine ();
		}
	}
}