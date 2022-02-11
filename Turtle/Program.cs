using System.Text.RegularExpressions;
using Turtle;
using Turtle.Generators;
using Turtle.Graphics;

public class Program {
	public static void Main (string [] args)
	{
		//var generator = new ExampleGenerator ();
		//var game = new TurtleGame (generator);
		//game.Play ();

		FormattedString fs = "potato\nlorem\nipsum";
		fs.Formats.Add (new Regex(".*"), new Format (backgroundColor: new Color (TerminalMode.Background, ColorName.Red)));

		ConsoleHelpers.WriteInPlace (fs, new Offset (3, 3));
	}
}