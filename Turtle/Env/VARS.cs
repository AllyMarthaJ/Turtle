using System;
using Turtle.Graphics;

namespace Turtle.Env
{
	public static class VARS
	{
		// this is used to format our strings and is dependent entirely on terminal, hence env variable.
		public static readonly string ANSI_PREFIX = Environment.GetEnvironmentVariable ("ANSI_PREFIX") ?? "\x1b[";

		// depends on terminal, integrated into partial display class
		public static readonly int BLOCK_WIDTH = Int32.Parse (Environment.GetEnvironmentVariable ("BLOCK_WIDTH") ?? "5");
		public static readonly int BLOCK_HEIGHT = Int32.Parse (Environment.GetEnvironmentVariable ("BLOCK_HEIGHT") ?? "3");

		// preferences, set when game start
		public static readonly bool SHOW_TITLE = bool.Parse (Environment.GetEnvironmentVariable ("SHOW_TITLE") ?? "true");
		public static readonly bool SHOW_KEYBOARD = bool.Parse (Environment.GetEnvironmentVariable ("SHOW_KEYBOARD") ?? "true");
		public static readonly bool HARD_MODE = bool.Parse (Environment.GetEnvironmentVariable ("HARD_MODE") ?? "false");

		// color scheme
		public static readonly Color BAD_CHAR_BACK = Color.Parse (TerminalMode.Background, Environment.GetEnvironmentVariable ("BAD_CHAR_BACK") ?? "rgb6bit(5,3,3)");
		public static readonly Color BAD_CHAR_FORE = Color.Parse (TerminalMode.Foreground, Environment.GetEnvironmentVariable ("BAD_CHAR_FORE") ?? "legacy(Black)");
		public static readonly Color BAD_POS_BACK = Color.Parse (TerminalMode.Background, Environment.GetEnvironmentVariable ("BAD_POS_BACK") ?? "rgb6bit(5,4,2)");
		public static readonly Color BAD_POS_FORE = Color.Parse (TerminalMode.Foreground, Environment.GetEnvironmentVariable ("BAD_POS_FORE") ?? "legacy(Black)");
		public static readonly Color GOOD_CHAR_BACK = Color.Parse (TerminalMode.Background, Environment.GetEnvironmentVariable ("GOOD_CHAR_BACK") ?? "rgb6bit(3,4,1)");
		public static readonly Color GOOD_CHAR_FORE = Color.Parse (TerminalMode.Foreground, Environment.GetEnvironmentVariable ("GOOD_CHAR_FORE") ?? "legacy(Black)");
		public static readonly Color DEFAULT_BACK = Color.Parse (TerminalMode.Background, Environment.GetEnvironmentVariable ("DEFAULT_BACK") ?? "gray(4)");
		public static readonly Color DEFAULT_FORE = Color.Parse (TerminalMode.Foreground, Environment.GetEnvironmentVariable ("DEFAULT_FORE") ?? "legacy(White)");

		// fetch repo
		public static readonly string REPO = Environment.GetEnvironmentVariable ("REPO") ?? "https://raw.githubusercontent.com/AllyMarthaJ/OpenTurtleGenerators/master/generators.json";

		// game data
		public static readonly string GAME = Environment.GetEnvironmentVariable ("GAME") ?? "example";
		public static readonly int SEED = Int32.Parse (Environment.GetEnvironmentVariable ("SEED") ?? "0");

		// you don't even need to ask
		internal static readonly string TITLE = @" _              _   _      
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
	}
}

