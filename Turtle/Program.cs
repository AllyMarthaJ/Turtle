using System.Text.RegularExpressions;
using Turtle;
using Turtle.Generators;
using Turtle.Graphics;

public class Program {
	public static void Main (string [] args)
	{
		var generator = new ExampleGenerator ();
		var game = new TurtleGame (generator);
		game.Play ();
		//Console.WriteLine ("\x1b[31mHello\nWorld\x1b[0m");

		//var wonGame = false;
		//var turns = 1;
		//var gen = new ExampleGenerator ();
		//var game = new TurtleGame (gen);
		//game.HardMode = true;
		////var fc = Console.ForegroundColor;
		//var bc = Console.BackgroundColor;
		//Console.WriteLine (game.Solution);
		//while (!wonGame || turns < gen.MaxTurns) {
		//	var input = Console.ReadLine ();
		//	var validInput = game.ValidateAndUpdateUpstream (input);
		//	foreach (var h in validInput) {
		//		var newBc = h == HintMode.BadCharacter ? ConsoleColor.Red :
		//			    h == HintMode.BadPosition ? ConsoleColor.Yellow : ConsoleColor.Green;
		//		Console.BackgroundColor = newBc;
		//		Console.Write ("#");
		//		Console.BackgroundColor = bc;
		//	}
		//	Console.WriteLine ();
		//}
	}
}