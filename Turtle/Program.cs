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
		fs.Formats.Add (new Regex("[\\w\\W]+"), new Format (backgroundColor: new Color (TerminalMode.Background, ColorName.Red)));
		string s = fs;
		Console.Write ("\n\n\n\n\n\n");
		Console.SetCursorPosition (Console.CursorLeft, Console.CursorTop -= 3);
		ConsoleHelpers.WriteInPlace (fs, new Offset (3, 3));

	}
}