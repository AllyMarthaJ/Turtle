using Turtle;
using Turtle.Generators;

public class Program {
	public static void Main (string [] args)
	{
		for (int i = 0; i < 16; i++) {
			for (int j = 0; j < 16; j++) {
				int code = i * 16 + j;
				Console.Write ($"\x1b[38;5;{code}m # \x1b[0m");
			}
			Console.WriteLine ();
		}
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