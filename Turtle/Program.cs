using Turtle;
using Turtle.Env;
using Turtle.Generators;
using Turtle.Graphics;

public class Program {
	/// <summary>
	/// Turtle, a total not-clone of a game that was purchased by NYT recently. See README.
	/// </summary>
	/// <param name="AnsiPrefix">Describes what characters to use.</param>
	/// <param name="BlockWidth">Width of each grid block.</param>
	/// <param name="BlockHeight">Height of each grid block.</param>
	/// <param name="ShowTitle">Shows or hides the title.</param>
	/// <param name="ShowKeyboard">Shows or hides the keyboard.</param>
	/// <param name="HardMode">Enables or disables hardmode.</param>
	/// <param name="BadCharBack">The background colour for bad characters.</param>
	/// <param name="BadCharFore">The foreground colour for bad characters.</param>
	/// <param name="BadPosBack">The background colour for bad positions.</param>
	/// <param name="BadPosFore">The foreground colour for bad positions.</param>
	/// <param name="GoodCharBack">The background colour for good characters.</param>
	/// <param name="GoodCharFore">The foreground colour for good characters.</param>
	/// <param name="DefaultBack">The background colour for empty blocks.</param>
	/// <param name="DefaultFore">The foreground colour for empty blocks.</param>
	/// <param name="Repo">The repo to fetch named games from.</param>
	/// <param name="StoreGenerator">Whether or not to save named fetched games.</param>
	/// <param name="Game">The name of the generator to use/game to play.</param>
	/// <param name="Seed">The seed to use. 0 for today's.</param>
	/// <param name="ListGames">Lists all games from given repositories.</param>
	/// <param name="FetchGame">Fetches game information by name.</param>
	/// <returns></returns>
	public static async Task Main (string? AnsiPrefix = null,
				       int? BlockWidth = null,
				       int? BlockHeight = null,
				       bool? ShowTitle = null,
				       bool? ShowKeyboard = null,
				       bool? HardMode = null,
				       string? BadCharBack = null,
				       string? BadCharFore = null,
				       string? BadPosBack = null,
				       string? BadPosFore = null,
				       string? GoodCharBack = null,
				       string? GoodCharFore = null,
				       string? DefaultBack = null,
				       string? DefaultFore = null,
				       string[]? Repo = null,
				       bool? StoreGenerator = null,
				       string? Game = null,
				       int? Seed = null,
				       bool ListGames = false,
				       string? FetchGame = null)
	{
		// loading environment
		var gf = new GeneratorFactory (new HttpClient ());

		if (!await handleCommandLineArgs (gf, AnsiPrefix,
						BlockWidth, BlockHeight,
						ShowTitle, ShowKeyboard, HardMode,
						BadCharBack, BadCharFore,
						BadPosBack, BadPosFore,
						GoodCharBack, GoodCharFore,
						DefaultBack, DefaultFore,
						Repo, StoreGenerator,
						Game, Seed,
						ListGames, FetchGame))
			return;

		// setting up screen processing
		int verticalIncrement = 1;
		ConsoleHelpers.AlternateScreen = true;

		AppDomain.CurrentDomain.ProcessExit += (o, e) => {
			ConsoleHelpers.AlternateScreen = false;
		};

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

			var gameResult = game.Play (new Offset (1, verticalIncrement));

			// game's finished, let the person relish in their win/loss
			Console.ReadKey (true);

			// return to normal and print out an emoji friendly result
			ConsoleHelpers.AlternateScreen = false;
			Console.WriteLine (game.GetGameStateResult (gameResult));
		} catch (Exception ex) {
			ConsoleHelpers.AlternateScreen = false;

			Console.WriteLine ("Whoops! Something went wrong. Here's some information for you:");
			Console.WriteLine ("Message: " + ex.Message);
			Console.WriteLine ("More details: " + ex.InnerException ?? "None available.");
		}
	}

	private static async Task<bool> handleCommandLineArgs (GeneratorFactory gf,
						   string? AnsiPrefix = null,
						   int? BlockWidth = null,
						   int? BlockHeight = null,
						   bool? ShowTitle = null,
						   bool? ShowKeyboard = null,
						   bool? HardMode = null,
						   string? BadCharBack = null,
						   string? BadCharFore = null,
						   string? BadPosBack = null,
						   string? BadPosFore = null,
						   string? GoodCharBack = null,
						   string? GoodCharFore = null,
						   string? DefaultBack = null,
						   string? DefaultFore = null,
						   string[]? Repo = null,
						   bool? StoreGenerator = null,
						   string? Game = null,
						   int? Seed = null,
						   bool ListGames = false,
						   string? FetchGame = null)
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
					  Game, Seed);


		if (ListGames) {
			try {
				foreach (var game in await gf.FetchRepositories ()) {
					Console.WriteLine ($"{game.Key}, {game.Value}");
				}
				return false;
			} catch { return false; }
		}

		if (FetchGame != null) {
			try {
				var source = await gf.GetGeneratorSource (FetchGame);

				var generator = await gf.CompileGeneratorSource (source);

				Console.WriteLine ("Source   : " + FetchGame);
				Console.WriteLine ("Name     : " + generator.Name);
				Console.WriteLine ("Author   : " + generator.Author);

				return false;
			} catch { return false; }
		}

		return true;
	}
}