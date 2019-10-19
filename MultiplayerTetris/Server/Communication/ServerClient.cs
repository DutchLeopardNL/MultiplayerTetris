using ServerProject.Data;
using ServerProject.GameLogics;
using System;
using System.IO;
using System.Net.Sockets;

namespace ServerProject.Communication
{
	public class ServerClient
	{
		private readonly TcpClient client;
		private readonly Server server;
		private readonly NetworkStream stream;
		private readonly byte[] buffer;
		private readonly string eof;
		private static readonly object lockObject = new object();
		public string Name { get; set; }
		public string PlayerID { get; set; }

		public ServerClient(TcpClient client, Server server, string playerID)
		{
			this.client = client;
			this.server = server;
			this.stream = this.client.GetStream();
			this.buffer = new byte[1024];
			this.eof = "##";
			this.PlayerID = playerID;

			this.stream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(this.OnRead), null);
		}

		/// <summary>
		/// This method is the callback from the BeginRead method.
		/// When the stream closes, the object is being removed.
		/// </summary>
		/// <param name="ar"></param>
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

				this.stream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(this.OnRead), null);
			}
			catch (IOException)
			{
				this.server.Clients.Remove(this);
				this.Disconnect();

				Console.WriteLine("Client disconnected");
			}

		}

		/// <summary>
		/// Decodes a packet coming from the client.
		/// </summary>
		/// <param name="packet"></param>
		public void HandlePacket(string packet)
		{
			Console.WriteLine($"Received from client: {packet}");

			if (packet.Contains("player"))
			{
				string[] nameAndWeapon = packet.Split(new[] { "::" }, StringSplitOptions.None);
				string name = nameAndWeapon[0];
				string weapon = nameAndWeapon[1];

				lock (lockObject)
				{
					this.server.Attackchoice.Add(name, (Weapon)Enum.Parse(typeof(Weapon), weapon));
				}

				Console.WriteLine($"Name: {name} answered: {weapon}");
				Console.WriteLine(this.server.Attackchoice.Count);
			}
			else if (packet.Contains("name"))
			{
				this.Name = packet.Split(new[] { "::" }, StringSplitOptions.None)[1];
			}
		}

		/// <summary>
		/// Write to the client.
		/// </summary>
		/// <param name="message"></param>
		public void Write(string message)
		{
			byte[] bytes = Encrypter.Encrypt($"{message}{this.eof}", "password123");
			this.stream.Write(bytes, 0, bytes.Length);
			this.stream.Flush();
		}

		/// <summary>
		/// Disconnects the communication
		/// </summary>
		public void Disconnect()
		{
			this.stream.Close();
			this.client.Close();
		}
	}
}
