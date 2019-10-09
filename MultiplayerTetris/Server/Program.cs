using ServerProject.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace ServerProject
{
	class Program
	{
        Server server;
		static void Main(string[] args)
		{
            Server server = new Server(25565);
            server.Start();        
			Console.ReadKey();
		}
	}
}
