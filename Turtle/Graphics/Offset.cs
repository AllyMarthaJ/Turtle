using System;
using System.Text;

namespace Turtle.Graphics {
	public struct Offset {
		public int? X { get; set; }
		public int? Y { get; set; }

		public Offset (int? x = null, int? y = null)
		{
			this.X = x;
			this.Y = y;
		}

		public StringBuilder [] GetOffsetBuilders ()
		{
			bool hasX = X.HasValue && X.Value != 0;
			bool hasY = Y.HasValue && Y.Value != 0;

			StringBuilder [] builders = new StringBuilder [
				hasX && hasY ? 2 :
				hasX || hasY ? 1 : 0];

			int pos = 0;
			if (hasX) {
				builders [pos] = new StringBuilder ();
				builders [pos].Append ($"{Math.Abs (X.Value)};");
				builders [pos].Append (X.Value >= 0 ? 'C' : 'D');

				pos++;
			}

			if (hasY) {
				builders [pos] = new StringBuilder ();
				builders [pos].Append ($"{Math.Abs (Y.Value)};");
				builders [pos].Append (Y.Value >= 0 ? 'B' : 'A');

				pos++;
			}

			return builders;
		}
	}
}

