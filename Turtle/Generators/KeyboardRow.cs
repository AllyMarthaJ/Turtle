using System;
namespace Turtle.Generators {
	public class KeyboardRow {
		public (char displayKey, ConsoleKey inputKey, int rowNum)[] Keys { get; set; }

		public KeyboardRow (params (char displayKey, ConsoleKey inputKey, int rowNum)[] keys)
		{
			this.Keys = keys;
		}
	}
}

