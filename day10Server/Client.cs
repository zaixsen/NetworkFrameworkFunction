using ExamDay10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace examDay10Server
{
    public class Client
    {
        public Socket socket;
        public PlayerData myPlayerData = new PlayerData();

        public Client(Socket socket)
        {
            this.socket = socket;
        }

        public byte[] data = new byte[1024];
    }
}
