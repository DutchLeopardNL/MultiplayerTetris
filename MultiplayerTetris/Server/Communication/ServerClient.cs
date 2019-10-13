using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ServerProject.Communication
{
	public class ServerClient
	{
		private TcpClient client;
		private Server server;
		private string hostName;
		private NetworkStream stream;
		private byte[] buffer;

		public ServerClient(TcpClient client, Server server)
		{
			this.client = client;
			this.server = server;
			this.stream = this.client.GetStream();
			this.buffer = new byte[1024];

			IPEndPoint iPEndPoint = (IPEndPoint)this.client.Client.RemoteEndPoint;
			this.hostName = Dns.GetHostEntry(iPEndPoint.Address).HostName;

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
			Console.WriteLine($"Received from {this.hostName}: {packet}");

			if (!packet.Contains("::")) return;

			string[] nameAndAnswer = packet.Split(new[] { "::" }, StringSplitOptions.None);
			Console.WriteLine($"Name: {nameAndAnswer[0]} answered {nameAndAnswer[1]}");
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
