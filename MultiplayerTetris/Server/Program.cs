using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ServerProject.Communication;
using ServerProject.Data;


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
