using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ClientGUI.Communication
{
    public class Client
    {
        private TcpClient client;
        private NetworkStream stream;
        private byte[] buffer;
        private string totalBuffer;
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
			this.stream = client.GetStream();

            this.stream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(OnRead), null);
        }

        private void OnRead(IAsyncResult ar)
        {
            int bytesRead = this.stream.EndRead(ar);
			string input = Encoding.ASCII.GetString(buffer, 0, bytesRead);

			string regex = "##";

			while (input.Contains(regex))
			{
				string packet = input.Substring(0, input.IndexOf(regex));
				input = input.Substring(input.IndexOf(regex) + regex.Length);

				this.HandlePacket(packet);
			}

			this.stream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(OnRead), null);
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
							Console.WriteLine("I won bitches");
						}
						else
						{
							Console.WriteLine("Lost");
						}
						break;
					case 0:
						Console.WriteLine("It's a tie");
						break;
					case 1:
						if (this.playerID == "player1")
						{
							Console.WriteLine("Lost");
						}
						else
						{
							Console.WriteLine("I won bitches");
						}
						break;
				}
			}
        }

        public void Write(string message)
        {
			string regex = "##";
			byte[] bytes = Encoding.ASCII.GetBytes($"{message}{regex}");
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
