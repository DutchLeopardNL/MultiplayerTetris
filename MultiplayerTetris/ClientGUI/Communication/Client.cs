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
        private byte[] totalBuffer;

        public Client()
        {
            this.client = new TcpClient();
            this.buffer = new byte[1024];
            this.totalBuffer = new byte[0];
        }

        public void Connect(string host, int port)
        {
            this.client.Connect(host, port);
            this.stream = client.GetStream();

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
            this.stream.Flush();
        }

        public void Disconnect()
        {

            stream.Close();
            client.Close();
        }
    }
}
