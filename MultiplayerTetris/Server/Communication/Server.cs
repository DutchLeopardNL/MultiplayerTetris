using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using ServerProject.GameLogics;
using ServerProject.Data;
using System.Threading;

namespace ServerProject.Communication
{
    public class Server
    {
        private TcpListener listener;
        private SPSLogics onlinegame;
		private List<PlayerScore> playerScores;
		private bool running;
		public List<ServerClient> clients { get; set; }
        public Dictionary<string, Weapon> attackchoice { get; set; }

        public Server(int port)
        {
			this.onlinegame = new SPSLogics();
            this.listener = new TcpListener(IPAddress.Any, port);
            this.clients = new List<ServerClient>();
            this.attackchoice = new Dictionary<string, GameLogics.Weapon>();
			this.playerScores = new List<PlayerScore>();
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
			while (this.running)
			{
                if (this.attackchoice.Count == 2)
                {
                    int result = onlinegame.PlayGame(attackchoice["player1"], attackchoice["player2"]);
					this.Broadcast($"result::{result}");

					this.SaveScores();
					this.attackchoice.Clear();
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
            string playerID = this.clients.Count == 0 ? "player1" : "player2"; //Determen if the connected client is player1 or player2

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
				string hostName = client.Name;
				//TODO: Implement FileIO to read/write scores per hostname
			}
		}

    }
}
