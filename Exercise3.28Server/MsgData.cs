using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise3._28Server
{
    class MsgData
    {
        public Client client;
        public byte[] content = new byte[1024];

        public MsgData(Client client, byte[] content)
        {
            this.client = client;
            this.content = content;
        }
    }
}
