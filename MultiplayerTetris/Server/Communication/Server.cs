using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ServerProject.Communication
{
	public class Server
	{
		private TcpListener listener;
		private List<ServerClient> clients;
        

		public Server(int port)
		{
			this.listener = new TcpListener(IPAddress.Any, port);
			this.clients = new List<ServerClient>();
		}

		public void Start()
		{
			this.listener.Start();
			this.listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
            Console.WriteLine("Server started listening....");
		}

		public void Stop()
		{
			foreach (var client in this.clients)
			{
				client.Disconnect();
			}
			this.listener.Stop();
		}

		private void OnConnect(IAsyncResult ar)
		{
			TcpClient newClient = this.listener.EndAcceptTcpClient(ar);
			this.clients.Add(new ServerClient(newClient, this));
         
            Console.WriteLine("A new client Connected");
			this.listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
            Console.WriteLine(clients.Count);
		}

		public void Broadcast(string message)
		{
			foreach (var client in this.clients)
			{
				client.Write(message);
			}
		}
	}
}
