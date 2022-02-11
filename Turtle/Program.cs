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

		Random rnd = new ();
		for (int i = 0; i < 10; i++) {
			FormattedString fs = ".";
			fs.Formats.Add (new Regex ("[\\w\\W]+"), new Format (backgroundColor: new Color (TerminalMode.Background, rnd.Next (1, 25))));

			ConsoleHelpers.WriteInPlace (fs, new Offset (i, 3));
		}
		Console.ReadKey ();

		//FormattedString fs = "potato\nlorem\nipsum";
		//fs.Formats.Add (new Regex("[\\w\\W]+"), new Format (backgroundColor: new Color (TerminalMode.Background, ColorName.Red)));
		//string s = fs;
		//Console.Write ("\n\n\n\n\n\n");
		//Console.SetCursorPosition (Console.CursorLeft, Console.CursorTop - 6);
		//ConsoleHelpers.WriteInPlace (fs, new Offset (3, 3));
		//ConsoleHelpers.WriteInPlace ("Potato", new Offset ());
		//Console.ReadKey (true);
	}
}