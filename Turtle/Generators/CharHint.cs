using System;
using System.Diagnostics.CodeAnalysis;

namespace Turtle.Generators
{
	public struct CharHint
	{
		public char Entry { get; set; }
		public HintMode Hint { get; set; }

		public CharHint(char entry, HintMode hint)
		{
			this.Entry = entry;
			this.Hint = hint;
		}

		public override bool Equals ([NotNullWhen (true)] object? obj)
		{
			if (obj is not CharHint) {
				return false;
			}

			var mapping = (CharHint)obj;

			return mapping.Entry == this.Entry && mapping.Hint == this.Hint;
		}

		public override int GetHashCode () => (Entry, Hint).GetHashCode ();

		public static bool operator==(CharHint left, CharHint right)
		{
			return left.Equals (right);
		}

		public static bool operator!=(CharHint left, CharHint right)
		{
			return !left.Equals (right);
		}
	}
}

