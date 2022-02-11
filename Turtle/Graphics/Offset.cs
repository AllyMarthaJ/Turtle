using System;
using System.Text;

namespace Turtle.Graphics {
	public struct Offset {
		public int X { get; set; }
		public int Y { get; set; }

		public Offset (int x = 0, int y = 0)
		{
			this.X = x;
			this.Y = y;
		}

		public StringBuilder [] GetOffsetBuilders ()
		{
			StringBuilder [] builders = new StringBuilder [
				this.X != 0 && this.Y != 0 ? 2 :
				this.X != 0 || this.Y != 0 ? 1 : 0];

			int pos = 0;
			if (this.X != 0) {
				builders [pos] = new StringBuilder ();
				builders [pos].Append ($"{Math.Abs (this.X)};");
				builders [pos].Append (this.X >= 0 ? 'C' : 'D');

				pos++;
			}

			if (this.Y != 0) {
				builders [pos] = new StringBuilder ();
				builders [pos].Append ($"{Math.Abs (this.Y)};");
				builders [pos].Append (this.Y >= 0 ? 'B' : 'A');

				pos++;
			}

			return builders;
		}
	}
}

