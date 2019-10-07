using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Server.Communication
{
	public class ServerClient
	{
		private TcpClient client;
		private Server server;
		private NetworkStream stream;
		private byte[] buffer;
		private byte[] totalBuffer;

		public ServerClient(TcpClient client, Server server)
		{
			this.client = client;
			this.server = server;
			this.stream = this.client.GetStream();
			this.buffer = new byte[1024];
			this.totalBuffer = new byte[0];

			this.stream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(OnRead), null);
		}

		private void OnRead(IAsyncResult ar)
		{
			int byteRead = this.stream.EndRead(ar);

			this.stream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(OnRead), null);
		}

		public void HandlePacket()
		{

		}

		public void Write(string message)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(message);
			this.stream.Write(bytes, 0, bytes.Length);
		}
	}
}
