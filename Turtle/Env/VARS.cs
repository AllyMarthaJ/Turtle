using System;
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

		// you don't even need to ask
		internal static readonly string TITLE = @" _              _   _      
| |            | | | |         _____     ____
| |_ _   _ _ __| |_| | ___    /      \  |  o | 
| __| | | | '__| __| |/ _ \  |        |/ ___\| 
| |_| |_| | |  | |_| |  __/  |_________/     
 \__|\__,_|_|   \__|_|\___|  |_|_| |_|_|
                                                        
Author :  https://github.com/AllyMarthaJ/     : me
          https://github.com/Ebony-Ayers/     : bunny, xoxo
ASCII  :  http://www.figlet.org/              : also see Fonty2
          http://asciiart.eu/";
	}
}

