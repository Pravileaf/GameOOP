using System;

namespace Sample1_04
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			using (Game1 game = new Game1())
			{
				game.Run();
			}
		}
	}
}

