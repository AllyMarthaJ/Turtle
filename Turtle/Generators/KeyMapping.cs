using System;
using System.Diagnostics.CodeAnalysis;

namespace Turtle.Generators
{
	public struct KeyMapping
	{
		public int Index { get; set; }
		public char Key { get; set; }

		public KeyMapping(int index, char key)
		{
			this.Index = index;
			this.Key = key;
		}

		public override bool Equals ([NotNullWhen (true)] object? obj)
		{
			if (obj is not KeyMapping) {
				return false;
			}

			var mapping = (KeyMapping)obj;

			return mapping.Index == this.Index && mapping.Key == this.Key;
		}

		public override int GetHashCode () => (Index, Key).GetHashCode ();

		public static bool operator==(KeyMapping left, KeyMapping right)
		{
			return left.Equals (right);
		}

		public static bool operator!=(KeyMapping left, KeyMapping right)
		{
			return !left.Equals (right);
		}
	}
}

