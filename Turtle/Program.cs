using System.Text.RegularExpressions;
using Turtle;
using Turtle.Generators;
using Turtle.Graphics;

public class Program {
	public static void Main (string [] args)
	{
		ConsoleHelpers.AlternateScreen = true;
		var generator = new ExampleGenerator ();
		var game = new TurtleGame (generator, 0);
		game.Play ();
		Console.ReadKey (true);
		ConsoleHelpers.AlternateScreen = false;
	}
}