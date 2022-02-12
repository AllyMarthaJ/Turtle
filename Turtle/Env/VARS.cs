using System;
using Turtle.Graphics;

namespace Turtle.Env
{
	public static class VARS
	{
		// this is used to format our strings and is dependent entirely on terminal, hence env variable.
		internal static string ANSI_PREFIX = Environment.GetEnvironmentVariable ("ANSI_PREFIX") ?? "\x1b[";

		// depends on terminal, integrated into partial display class
		internal static int BLOCK_WIDTH = Int32.Parse (Environment.GetEnvironmentVariable ("BLOCK_WIDTH") ?? "5");
		internal static int BLOCK_HEIGHT = Int32.Parse (Environment.GetEnvironmentVariable ("BLOCK_HEIGHT") ?? "3");

		// preferences, set when game start
		internal static bool SHOW_TITLE = bool.Parse (Environment.GetEnvironmentVariable ("SHOW_TITLE") ?? "true");
		internal static bool SHOW_KEYBOARD = bool.Parse (Environment.GetEnvironmentVariable ("SHOW_KEYBOARD") ?? "true");
		internal static bool HARD_MODE = bool.Parse (Environment.GetEnvironmentVariable ("HARD_MODE") ?? "false");

		// color scheme
		internal static Color BAD_CHAR_BACK = Color.Parse (TerminalMode.Background, Environment.GetEnvironmentVariable ("BAD_CHAR_BACK") ?? "rgb6bit(5,3,3)");
		internal static Color BAD_CHAR_FORE = Color.Parse (TerminalMode.Foreground, Environment.GetEnvironmentVariable ("BAD_CHAR_FORE") ?? "legacy(Black)");
		internal static Color BAD_POS_BACK = Color.Parse (TerminalMode.Background, Environment.GetEnvironmentVariable ("BAD_POS_BACK") ?? "rgb6bit(5,4,2)");
		internal static Color BAD_POS_FORE = Color.Parse (TerminalMode.Foreground, Environment.GetEnvironmentVariable ("BAD_POS_FORE") ?? "legacy(Black)");
		internal static Color GOOD_CHAR_BACK = Color.Parse (TerminalMode.Background, Environment.GetEnvironmentVariable ("GOOD_CHAR_BACK") ?? "rgb6bit(3,4,1)");
		internal static Color GOOD_CHAR_FORE = Color.Parse (TerminalMode.Foreground, Environment.GetEnvironmentVariable ("GOOD_CHAR_FORE") ?? "legacy(Black)");
		internal static Color DEFAULT_BACK = Color.Parse (TerminalMode.Background, Environment.GetEnvironmentVariable ("DEFAULT_BACK") ?? "gray(4)");
		internal static Color DEFAULT_FORE = Color.Parse (TerminalMode.Foreground, Environment.GetEnvironmentVariable ("DEFAULT_FORE") ?? "legacy(White)");

		// fetch repo
		internal static string REPO = Environment.GetEnvironmentVariable ("REPO") ?? "https://raw.githubusercontent.com/AllyMarthaJ/OpenTurtleGenerators/master/generators.json";
		internal static bool STORE_GENERATOR = bool.Parse (Environment.GetEnvironmentVariable ("STORE_GENERATOR") ?? "true");
		// game data
		internal static string GAME = Environment.GetEnvironmentVariable ("GAME") ?? "example";
		internal static int SEED = Int32.Parse (Environment.GetEnvironmentVariable ("SEED") ?? "0");

		// you don't even need to ask
		internal static string TITLE = @" _              _   _      
| |            | | | |         _____     ____
| |_ _   _ _ __| |_| | ___    /      \  |  o | 
| __| | | | '__| __| |/ _ \  |        |/ ___\| 
| |_| |_| | |  | |_| |  __/  |_________/     
 \__|\__,_|_|   \__|_|\___|  |_|_| |_|_|
                                                        
Author :  https://github.com/AllyMarthaJ/     : me
          https://github.com/Ebony-Ayers/     : bunny, xoxo
          {author}: {generator}
ASCII  :  http://www.figlet.org/              : also see Fonty2
          http://asciiart.eu/";

		internal static void OverrideEnvironment(string? AnsiPrefix = null,
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
				       string? Repo = null,
				       bool? StoreGenerator = null,
				       string? Game = null,
				       int? Seed = null)
		{
			ANSI_PREFIX = AnsiPrefix ?? ANSI_PREFIX;

			BLOCK_WIDTH = BlockWidth ?? BLOCK_WIDTH;
			BLOCK_HEIGHT = BlockHeight ?? BLOCK_HEIGHT;

			SHOW_TITLE = ShowTitle ?? SHOW_TITLE;
			SHOW_KEYBOARD = ShowKeyboard ?? SHOW_KEYBOARD;
			HARD_MODE = HardMode ?? HARD_MODE;

			BAD_CHAR_BACK = BadCharBack == null ? BAD_CHAR_BACK : Color.Parse (TerminalMode.Background, BadCharBack);
			BAD_CHAR_FORE = BadCharFore == null ? BAD_CHAR_FORE : Color.Parse (TerminalMode.Background, BadCharFore);
			BAD_POS_BACK = BadPosBack == null ? BAD_POS_BACK : Color.Parse (TerminalMode.Background, BadPosBack);
			BAD_POS_FORE = BadPosFore == null ? BAD_POS_FORE : Color.Parse (TerminalMode.Background, BadPosFore);
			GOOD_CHAR_BACK = GoodCharBack == null ? GOOD_CHAR_BACK : Color.Parse (TerminalMode.Background, GoodCharBack);
			GOOD_CHAR_FORE = GoodCharFore == null ? GOOD_CHAR_FORE : Color.Parse (TerminalMode.Background, GoodCharFore);
			DEFAULT_BACK = DefaultBack == null ? DEFAULT_BACK : Color.Parse (TerminalMode.Background, DefaultBack);
			DEFAULT_FORE = DefaultFore == null ? DEFAULT_FORE : Color.Parse (TerminalMode.Background, DefaultFore);

			REPO = Repo ?? REPO;
			STORE_GENERATOR = StoreGenerator ?? STORE_GENERATOR;

			GAME = Game ?? GAME;
			SEED = Seed ?? SEED;
		}
	}
}

