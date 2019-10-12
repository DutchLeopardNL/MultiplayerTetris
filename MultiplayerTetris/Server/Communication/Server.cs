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
		private Dictionary<ServerClient,int> clients;
        private int p = 0;

		public Server(int port)
		{
			this.listener = new TcpListener(IPAddress.Any, port);
			this.clients = new Dictionary<ServerClient,int>();
		}

		public void Start()
		{
			this.listener.Start();
			this.listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
            Console.WriteLine("Server started listening....");
		}

		public void Stop()
		{
            foreach (KeyValuePair<ServerClient, int> client in this.clients)
            {
                
				client.Key.Disconnect();
			}
			this.listener.Stop();
		}
        //Adds a new client to the Dictionary where p will be used as ID
		private void OnConnect(IAsyncResult ar)
		{
             
			TcpClient newClient = this.listener.EndAcceptTcpClient(ar);
			this.clients.Add(new ServerClient(newClient, this),p);
            p++;
            Console.WriteLine("A new client Connected " + p);
			this.listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
            Console.WriteLine(clients.Count);
		}

		public void Broadcast(string message)
		{
			foreach (KeyValuePair<ServerClient,int> client in this.clients)
			{
				client.Key.Write(message);
			}
		}
        public void SendToClient(string message, int clientID)
        {
            foreach  (KeyValuePair<ServerClient, int> client in this.clients)
            {
                if (client.Value == clientID)
                {
                    client.Key.Write(message);
                }
            }
        }
	}
}
