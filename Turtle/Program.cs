using System.Text.RegularExpressions;
using Turtle;
using Turtle.Generators;
using Turtle.Graphics;

public class Program {
	public static void Main (string [] args)
	{
		ConsoleHelpers.AlternateScreen = true;
		var generator = new ExampleGenerator ();
		var game = new TurtleGame (generator);
		game.Play ();
		ConsoleHelpers.AlternateScreen = false;
		Console.CancelKeyPress += (o, e) => {
			e.Cancel = true;
			Console.WriteLine ("HELLO");
			ConsoleHelpers.AlternateScreen = false;
		};

		//var progress = new int [10];

		//Random rnd = new ();
		//while (true) { 
		//	for (int y = 0; y < 20; y++) {
		//		for (int x = 0; x < 40; x++) {
		//			FormattedString fs = ((char)((rnd.Next(2) == 1 ? 'A' : 'a') + rnd.Next(26))).ToString();
		//			fs.Formats.Add (new Regex ("[\\w\\W]+"), new Format (backgroundColor: new Color (TerminalMode.Background, rnd.Next (1, 25))));

		//			ConsoleHelpers.WriteInPlace (fs, new Offset (x, y));
		//		}
		//	}
		//	Thread.Sleep (50);
		//}
		Console.ReadKey ();
	}
}