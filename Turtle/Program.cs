using System.Text.RegularExpressions;
using Turtle;
using Turtle.Env;
using Turtle.Generators;
using Turtle.Graphics;

public class Program {
	public static void Main (string [] args)
	{
		int verticalIncrement = 1;
		ConsoleHelpers.AlternateScreen = true;

		var game = new TurtleGame (new ExampleGenerator (), 0);

		// game behaviour and display config
		game.HardMode = VARS.HARD_MODE;
		if (VARS.SHOW_TITLE)
			verticalIncrement += game.DrawStyledTurtle (new Offset (1, verticalIncrement)) + 1;
		if (VARS.SHOW_KEYBOARD)
			verticalIncrement += game.DrawKeyboard (new Offset (1, verticalIncrement)) + 1;

		game.Play (new Offset(2, verticalIncrement));

		Console.ReadKey (true);

		ConsoleHelpers.AlternateScreen = false;
	}
}