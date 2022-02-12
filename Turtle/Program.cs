using Turtle;
using Turtle.Env;
using Turtle.Generators;
using Turtle.Graphics;

public class Program {
	public static async Task Main (string? AnsiPrefix = null,
				       int? BlockWidth = null,
				       int? BlockHeight = null,
				       bool? ShowTitle = null,
				       bool? ShowKeyboard = null,
				       bool? HardMode =null,
				       string? BadCharBack = null,
				       string? BadCharFore = null,
				       string? BadPosBack = null,
				       string? BadPosFore = null,
				       string? GoodCharBack =null,
				       string? GoodCharFore = null,
				       string? DefaultBack = null,
				       string? DefaultFore = null,
				       string? Repo = null,
				       bool? StoreGenerator = null,
				       string? Game = null,
				       int? Seed = null)
	{
		// loads command line arguments instead of environment variables, where applicable
		VARS.OverrideEnvironment (AnsiPrefix,
					  BlockWidth, BlockHeight,
					  ShowTitle, ShowKeyboard, HardMode,
					  BadCharBack, BadCharFore,
					  BadPosBack, BadPosFore,
					  GoodCharBack, GoodCharFore,
					  DefaultBack, DefaultFore,
					  Repo, StoreGenerator,
					  Game, Seed);

		// setting up screen processing
		int verticalIncrement = 1;
		ConsoleHelpers.AlternateScreen = true;

		AppDomain.CurrentDomain.ProcessExit += (o, e) => {
			ConsoleHelpers.AlternateScreen = false;
		};

		var gf = new GeneratorFactory (new HttpClient ());
		IGenerator generator;

		try {
			// fetch generator for game
			var source = await gf.GetGeneratorSource (VARS.GAME);

			generator = await gf.CompileGeneratorSource (source);

			// set up game behaviour and display config
			TurtleGame game = VARS.SEED == 0 ?
				new TurtleGame (generator, VARS.HARD_MODE) :
				new (generator, VARS.SEED, VARS.HARD_MODE);

			if (VARS.SHOW_TITLE)
				verticalIncrement += game.DrawStyledTurtle (new Offset (1, verticalIncrement)) + 1;
			if (VARS.SHOW_KEYBOARD)
				verticalIncrement += game.DrawKeyboard (new Offset (1, verticalIncrement)) + 1;

			var gameResult = game.Play (new Offset (2, verticalIncrement));

			// game's finished, let the person relish in their win/loss
			Console.ReadKey (true);

			// return to normal and print out an emoji friendly result
			ConsoleHelpers.AlternateScreen = false;
			Console.WriteLine (game.GetGameStateResult (gameResult));
		} catch (Exception ex) {
			Console.WriteLine ("Whoops! Something went wrong. Here's some information for you:");
			Console.WriteLine ("Message: " + ex.Message);
			Console.WriteLine ("More details: " + ex.InnerException ?? "None available.");
		}
	}
}