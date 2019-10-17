using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using ServerProject.Data;
using System.IO;

namespace ClientGUI.Communication
{
    public class Client
    {
        private TcpClient client;
        private NetworkStream stream;
        private byte[] buffer;
        private string totalBuffer;
		public MainWindow mainWindow { get; set; }
		public string playerID { get; set; }

        public Client()
        {
			this.playerID = null;
            this.client = new TcpClient();
            this.buffer = new byte[1024];
            this.totalBuffer = string.Empty;
		}

        public void Connect(string host, int port)
        {
            this.client.Connect(host, port);
			this.stream = this.client.GetStream();

            this.stream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(OnRead), null);
        }

        private void OnRead(IAsyncResult ar)
        {
			try
			{
				int count = this.stream.EndRead(ar);
				string input = Encrypter.Decrypt(this.buffer.SubArray(0, count), "password123");

				string regex = "##";

				while (input.Contains(regex))
				{
					string packet = input.Substring(0, input.IndexOf(regex));
					input = input.Substring(input.IndexOf(regex) + regex.Length);

					this.HandlePacket(packet);
				}

				this.stream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(OnRead), null);
			}
			catch (IOException)
			{
				this.Disconnect();

				Console.WriteLine("Server shut down");
			}
        }

		public delegate void UpdateTextCallback(string message);

		private void UpdateText(string message)
		{
			this.mainWindow.result.Text = (message + "\n");
		}

		public void HandlePacket(string packet)
        {
			Console.WriteLine($"Server send: {packet}");

			if (packet.Contains("player") && this.playerID == null)
			{
				this.playerID = packet;
			}
			else if (packet.Contains("result"))
			{
				int winLoseTie = int.Parse(packet.Split(new[] { "::" }, StringSplitOptions.None)[1]);
				switch (winLoseTie)
				{
					case -1:
						if (this.playerID == "player1")
						{
							this.mainWindow.result.Dispatcher.Invoke(new UpdateTextCallback(UpdateText), "You won!");                 
						}
						else
						{
							this.mainWindow.result.Dispatcher.Invoke(new UpdateTextCallback(UpdateText), "You lost.");               
                        }
						break;
					case 0:
						this.mainWindow.result.Dispatcher.Invoke(new UpdateTextCallback(UpdateText), "Tie!"); 
                        break;
					case 1:
						if (this.playerID == "player1")
						{
							this.mainWindow.result.Dispatcher.Invoke(new UpdateTextCallback(UpdateText), "You lost.");
                        }
						else
						{
							this.mainWindow.result.Dispatcher.Invoke(new UpdateTextCallback(UpdateText), "You won!");
                        }
						break;
				}
			}
        }

        public void Write(string message)
        {
			string regex = "##";
			byte[] bytes = Encrypter.Encrypt($"{message}{regex}", "password123");
			this.stream.Write(bytes, 0, bytes.Length);
            this.stream.Flush();
        }

        public void Disconnect()
        {
            this.stream.Close();
            this.client.Close();
        }
    }
}
