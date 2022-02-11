using System;
namespace Turtle.Env
{
	public static class VARS
	{
		public static readonly string ANSI_PREFIX = Environment.GetEnvironmentVariable ("ANSI_PREFIX") ?? "\x1b[";

		public static readonly int BLOCK_WIDTH = Int32.Parse (Environment.GetEnvironmentVariable ("BLOCK_WIDTH") ?? "5");
		public static readonly int BLOCK_HEIGHT = Int32.Parse (Environment.GetEnvironmentVariable ("BLOCK_HEIGHT") ?? "3");
	}
}

