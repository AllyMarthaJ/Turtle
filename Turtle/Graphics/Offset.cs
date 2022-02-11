using System;
using System.Text;

namespace Turtle.Graphics
{
	public struct Offset
	{
		public int? X { get; set; }
		public int? Y { get; set; }

		public Offset(int? x = null, int? y = null)
		{
			this.X = x;
			this.Y = y;
		}

		public StringBuilder[] GetOffsetBuilders ()
		{
			StringBuilder [] builders = new StringBuilder[X.HasValue && Y.HasValue ? 2 : 1];

			int pos = 0;
			if (X.HasValue) {
				builders [pos] = new StringBuilder ();
				builders [pos].Append ($"{Math.Abs(X.Value)};");
				builders [pos].Append (X.Value >= 0 ? 'C' : 'D');

				pos++;
			}

			if (Y.HasValue) {
				builders [pos] = new StringBuilder ();
				builders [pos].Append ($"{Math.Abs (Y.Value)};");
				builders [pos].Append (Y.Value >= 0 ? 'B' : 'A');

				pos++;
			}

			return builders;
		}
	}
}

