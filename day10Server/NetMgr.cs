using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace examDay10Server
{
    public class NetMgr : Singleton<NetMgr>
    {
        Socket socket;
        //       端口号  客户端
        Dictionary<int, Client> dic_clients = new Dictionary<int, Client>();

        public void Init()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(new IPEndPoint(IPAddress.Any, 9999));

            socket.Listen(10);
            socket.BeginAccept(OnAsyAccept, null);
        }

        private void OnAsyAccept(IAsyncResult ar)
        {
            try
            {
                Socket s = socket.EndAccept(ar);

                Client client = new Client(s);
                Console.WriteLine("连接成功！！！");
                s.BeginReceive(client.data, 0, client.data.Length, SocketFlags.None, OnAsyReceive, client);

                IPEndPoint iPEndPoint = s.RemoteEndPoint as IPEndPoint;

                dic_clients.Add(iPEndPoint.Port, client);

                socket.BeginAccept(OnAsyAccept, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void OnAsyReceive(IAsyncResult ar)
        {
            try
            {
                Client cli = ar.AsyncState as Client;

                int len = cli.socket.EndReceive(ar);

                if (len == 0)
                {
                    cli.socket.Shutdown(SocketShutdown.Both);
                    cli.socket.Close();
                    return;
                }

                byte[] rData = cli.data.Take(len).ToArray();

                while (rData.Length > 4)
                {
                    int head = BitConverter.ToInt32(rData, 0);
                    int msghId = BitConverter.ToInt32(rData, 4);

                    byte[] content = rData.Skip(8).Take(head - 4).ToArray();

                    MsgData msgData = new MsgData(cli, content);

                    MessageCenter.Ins.Dispatch(msghId, msgData);

                    rData = rData.Skip(head + 4).ToArray();
                }

                cli.socket.BeginReceive(cli.data, 0, cli.data.Length, SocketFlags.None, OnAsyReceive, cli);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void SendAllClientNotSelf(Client client, int msgId, byte[] data)
        {
            foreach (var item in dic_clients.Values)
            {
                if (client != item)
                {
                    SendClient(item, msgId, data);
                }
            }
        }
        public void SendAllClient(int msgId, byte[] data)
        {
            foreach (var item in dic_clients.Values)
            {
                SendClient(item, msgId, data);
            }
        }

        public void SendRoomList(List<Client> roomClients, int msgId, byte[] data)
        {
            for (int i = 0; i < roomClients.Count; i++)
            {
                SendClient(roomClients[i], msgId, data);
            }
        }

        public void SendClient(Client client, int msgId, byte[] data)
        {
            int head = 4 + data.Length;
            byte[] content = BitConverter.GetBytes(head).Concat(BitConverter.GetBytes(msgId)).Concat(data).ToArray();
            client.socket.BeginSend(content, 0, content.Length, SocketFlags.None, OnAsySend, client);
        }

        void OnAsySend(IAsyncResult ar)
        {
            try
            {
                Client cli = ar.AsyncState as Client;
                cli.socket.EndSend(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
