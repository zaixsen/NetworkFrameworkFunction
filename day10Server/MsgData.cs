using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examDay10Server
{
    public class MsgData
    {
        public Client client;
        public byte[] data = new byte[1024];

        public MsgData(Client client, byte[] data)
        {
            this.client = client;
            this.data = data;
        }
    }
}
