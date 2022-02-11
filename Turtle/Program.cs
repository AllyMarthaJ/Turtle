using System.Text.RegularExpressions;
using Turtle;
using Turtle.Env;
using Turtle.Generators;
using Turtle.Graphics;

public class Program {
	public static async Task Main (string [] args)
	{
		var gf = new GeneratorFactory (new HttpClient ());
		var source = await gf.GetGeneratorSource ("example");
		var gen = await gf.CompileGeneratorSource (source);

		int verticalIncrement = 1;
		ConsoleHelpers.AlternateScreen = true;

		var game = new TurtleGame (gen, 0);

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