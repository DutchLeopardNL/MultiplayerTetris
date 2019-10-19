using ServerProject.Communication;
using System;


namespace ServerProject
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			Server server = new Server(10001);
			server.Start();

			Console.ReadKey();
			server.Stop();
		}
	}
}
