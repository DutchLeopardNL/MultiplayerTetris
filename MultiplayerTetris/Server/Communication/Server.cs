using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using ServerProject.GameLogics;
using ServerProject.Data;
using System.Threading;

namespace ServerProject.Communication
{
    public class Server
    {
		public static string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}/RockPaperScissors";

		private TcpListener listener;
        private SPSLogics onlinegame;
		private bool running;
		public List<ServerClient> Clients { get; set; }
        public Dictionary<string, Weapon> Attackchoice { get; set; }
		public Dictionary<string, int> Score { get; set; }

        public Server(int port)
        {
            this.listener = new TcpListener(IPAddress.Any, port);
			this.onlinegame = new SPSLogics();
			this.running = true;
			this.Clients = new List<ServerClient>();
			this.Attackchoice = new Dictionary<string, Weapon>();
		}

		/// <summary>
		/// Starts the server
		/// </summary>
        public void Start()
        {
            this.listener.Start();
            this.listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
            Console.WriteLine("Server started listening....");

			new Thread(new ThreadStart(Listen)).Start();
        }

		/// <summary>
		/// Disconnects all the connected clients and stops the server.
		/// </summary>
		public void Stop()
		{
			foreach (var client in this.Clients)
			{
				client.Disconnect();
			}
			this.running = false;
			this.listener.Stop();
		}

		/// <summary>
		/// This method is running in an other thread. It is checkin if their is a game played. 
		/// It also handles the result of this game.
		/// </summary>
		private void Listen()
        {
			while (this.running)
			{
                if (this.Attackchoice.Count == 2)
                {
                    int result = this.onlinegame.PlayGame(Attackchoice["player1"], Attackchoice["player2"]);
					this.Broadcast($"result::{result}");

					this.SaveScores(result);
					this.Attackchoice.Clear();
				}

				Thread.Sleep(1000);
			}
        }

		/// <summary>
		/// This method is the callback from the BeginAcceptTcpClient method.
		/// </summary>
		/// <param name="ar"></param>
        private void OnConnect(IAsyncResult ar)
		{ 
			if (this.Clients.Count < 2)
			{
				TcpClient newClient = this.listener.EndAcceptTcpClient(ar);

				string playerID = this.Clients.Count == 0 ? "player1" : "player2";
				this.Clients.Add(new ServerClient(newClient, this, playerID));
				Console.WriteLine("A new client Connected");
				this.Broadcast(playerID);
			}
            
            this.listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }

		/// <summary>
		/// Send a message to all clients connected.
		/// </summary>
		/// <param name="message"></param>
        public void Broadcast(string message)
        {
            foreach (var client in this.Clients)
            {
                client.Write(message);
            }
        }

		/// <summary>
		/// Save the this.Scores dictionary. The total wins are stored within this dictionary. 
		/// This object is written to your own documents.
		/// </summary>
		/// <param name="result"></param>
		private void SaveScores(int result)
		{
			this.Score = FileIO.Read(path, "scores.txt");
			
			foreach (var client in this.Clients)
			{
				if (!this.Score.Keys.Contains(client.Name))
				{
					this.Score[client.Name] = 0;
				}

				if ((client.PlayerID == "player1" && result == -1) || (client.PlayerID == "player2" && result == 1))
				{
					this.Score[client.Name]++;
				}
			}

			FileIO.Write(path, "scores.txt", this.Score);
		}

    }
}
