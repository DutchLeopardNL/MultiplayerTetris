using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using ServerProject.GameLogics;
using System.Threading;

namespace ServerProject.Communication
{
    public class Server
    {
        private TcpListener listener;
        private SPSLogics onlinegame;
        private List<ServerClient> clients;
        public Dictionary<string, GameLogics.Weapon> attackchoice { get; set; }
		private bool running;

        public Server(int port)
        {
			this.onlinegame = new SPSLogics();
            this.listener = new TcpListener(IPAddress.Any, port);
            this.clients = new List<ServerClient>();
            this.attackchoice = new Dictionary<string, GameLogics.Weapon>();
			this.running = true;
        }

        public void Start()
        {
            this.listener.Start();
            this.listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
            Console.WriteLine("Server started listening....");

			new Thread(new ThreadStart(Listen)).Start();
        }
        private void Listen()
        {
			while (running)
			{
				if (attackchoice.Count == 2)
				{
					int result = onlinegame.PlayGame(attackchoice.Values.ElementAt(0), attackchoice.Values.ElementAt(1));
					this.Broadcast($"result::{result}");

					attackchoice.Clear();
				}

				Thread.Sleep(1000);
			}
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
            string playerID = this.clients.Count == 0 ? "player1" : "player2"; //Detemen if the connected client is player1 or player2

            this.clients.Add(new ServerClient(newClient, this));
            Console.WriteLine("A new client Connected");
            this.Broadcast(playerID);
            this.listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }

        public void Broadcast(string message)
        {
            foreach (var client in this.clients)
            {
                client.Write(message);
            }
        }

		public void SaveScores()
		{
			foreach (var client in this.clients)
			{
				string hostName = client.hostName;
				//TODO: Implement FileIO to read/write scores per hostname
			}
		}
    }
}
