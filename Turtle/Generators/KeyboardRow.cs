using System;
namespace Turtle.Generators {
	public class KeyboardRow {
		public (char displayKey, ConsoleKey inputKey)[] Keys { get; set; }

		public KeyboardRow (params (char displayKey, ConsoleKey inputKey)[] keys)
		{
			this.Keys = keys;
		}
	}
}

