using System;
using ServerProject.Communication;


namespace ServerProject
{
	class Program
	{
		static void Main(string[] args)
		{
			Server server = new Server(10001);
			server.Start();

			Console.ReadKey();
			server.Stop();
		}
	}
}
