using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientGUI.Communication;

namespace TestClient
{
	class Program
	{
		static void Main(string[] args)
		{
			//Client client = new Client();
			//client.Connect("localhost", 10001);

			string input = "";
			while (input != "stop")
			{
				input = Console.ReadLine();
				client.Write(input);
			}

			client.Disconnect();
		}
	}
}
