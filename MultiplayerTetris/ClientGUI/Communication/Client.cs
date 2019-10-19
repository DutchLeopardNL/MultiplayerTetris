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
		private string eof;
		public MainWindow MainWindow { get; set; }
		public string PlayerID { get; set; }

        public Client()
        {
            this.client = new TcpClient();
            this.buffer = new byte[1024];
			this.eof = "##";
			this.PlayerID = null;
		}

        public async Task Connect(string host, int port)
        {
            await this.client.ConnectAsync(host, port);
			this.stream = this.client.GetStream();

            this.stream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(OnRead), null);
        }

        private void OnRead(IAsyncResult ar)
        {
			try
			{
				int count = this.stream.EndRead(ar);
				string input = Encrypter.Decrypt(this.buffer.SubArray(0, count), "password123");

				while (input.Contains(this.eof))
				{
					string packet = input.Substring(0, input.IndexOf(this.eof));
					input = input.Substring(input.IndexOf(this.eof) + this.eof.Length);

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
			this.MainWindow.result.Text = (message + "\n");
		}

		public void HandlePacket(string packet)
        {
			Console.WriteLine($"Server send: {packet}");

			if (packet.Contains("player") && this.PlayerID == null)
			{
				this.PlayerID = packet;
			}
			else if (packet.Contains("result"))
			{
				int winLoseTie = int.Parse(packet.Split(new[] { "::" }, StringSplitOptions.None)[1]);
				switch (winLoseTie)
				{
					case -1:
						if (this.PlayerID == "player1")
						{
							this.MainWindow.result.Dispatcher.Invoke(new UpdateTextCallback(UpdateText), "You won!");                 
						}
						else
						{
							this.MainWindow.result.Dispatcher.Invoke(new UpdateTextCallback(UpdateText), "You lost.");               
                        }
						break;
					case 0:
						this.MainWindow.result.Dispatcher.Invoke(new UpdateTextCallback(UpdateText), "Tie!"); 
                        break;
					case 1:
						if (this.PlayerID == "player1")
						{
							this.MainWindow.result.Dispatcher.Invoke(new UpdateTextCallback(UpdateText), "You lost.");
                        }
						else
						{
							this.MainWindow.result.Dispatcher.Invoke(new UpdateTextCallback(UpdateText), "You won!");
                        }
						break;
				}
			}
        }

        public void Write(string message)
        {
			byte[] bytes = Encrypter.Encrypt($"{message}{this.eof}", "password123");
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
