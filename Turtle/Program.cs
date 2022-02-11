using System.Text.RegularExpressions;
using Turtle;
using Turtle.Env;
using Turtle.Generators;
using Turtle.Graphics;

public class Program {
	public static async Task Main (string [] args)
	{
		// fetch our generator
		var gf = new GeneratorFactory (new HttpClient ());

		var source = await gf.GetGeneratorSource (VARS.GAME);
		var generator = await gf.CompileGeneratorSource (source);

		// setting up game environment
		int verticalIncrement = 1;
		ConsoleHelpers.AlternateScreen = true;

		AppDomain.CurrentDomain.ProcessExit += (o, e) => {
			ConsoleHelpers.AlternateScreen = false;
		};

		try {
			// game behaviour and display config
			TurtleGame game = VARS.SEED == 0 ?
				new TurtleGame (generator, VARS.HARD_MODE) :
				new (generator, VARS.SEED, VARS.HARD_MODE);

			if (VARS.SHOW_TITLE)
				verticalIncrement += game.DrawStyledTurtle (new Offset (1, verticalIncrement)) + 1;
			if (VARS.SHOW_KEYBOARD)
				verticalIncrement += game.DrawKeyboard (new Offset (1, verticalIncrement)) + 1;

			var gameResult = game.Play (new Offset (2, verticalIncrement));

			Console.ReadKey (true);

			ConsoleHelpers.AlternateScreen = false;
			Console.WriteLine (game.GetGameStateResult (gameResult));
		} catch (Exception ex) {
			Console.WriteLine ("Whoops! Something went wrong. Here's some information for you:");
			Console.WriteLine ("Message: " + ex.Message);
			Console.WriteLine ("More details: " + ex.InnerException ?? "None available.");
		}
	}
}