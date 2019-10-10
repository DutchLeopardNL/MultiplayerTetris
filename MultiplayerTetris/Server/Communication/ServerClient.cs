using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ServerProject.Communication
{
	public class ServerClient
	{
		private TcpClient client;
		private Server server;
		private NetworkStream stream;
		private byte[] buffer;

		public ServerClient(TcpClient client, Server server)
		{
			this.client = client;
			this.server = server;
			this.stream = this.client.GetStream();
			this.buffer = new byte[1024];

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
			Console.WriteLine($"Received from client: {packet}");
			this.server.Broadcast($"{packet}##");
		}

		public void Write(string message)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(message);
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
