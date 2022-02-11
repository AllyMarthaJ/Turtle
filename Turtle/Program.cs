using System.Text.RegularExpressions;
using Turtle;
using Turtle.Env;
using Turtle.Generators;
using Turtle.Graphics;

public class Program {
	public static void Main (string [] args)
	{
		Offset gameOffset = new Offset (VARS.SHOW_TITLE ? 2 : 1, VARS.SHOW_TITLE ? 13 : 1);

		ConsoleHelpers.AlternateScreen = true;

		if (VARS.SHOW_TITLE)
			styledTurtle ().Draw (1, 1);

		var game = new TurtleGame (new ExampleGenerator (), 0);
		game.Play (gameOffset);

		Console.ReadKey (true);

		ConsoleHelpers.AlternateScreen = false;
	}

	private static FormattedString styledTurtle ()
	{
		FormattedString turtle = VARS.TITLE;

		turtle.Formats.Add (new Regex ("(?<![a-zA-Z(:/)])[\\\\/|]"), new Format (
				bold: true,
				foregroundColor: new Color (TerminalMode.Foreground, 5, 3, 5, false)
			));
		turtle.Formats.Add (new Regex ("_"), new Format (
				bold: true,
				foregroundColor: new Color (TerminalMode.Foreground, 4, 3, 5, false),
				underline: true
			));

		turtle.Formats.Add (new Regex ("http(.*)/"), new Format (
				underline: true
			));
		return turtle;
	}
}