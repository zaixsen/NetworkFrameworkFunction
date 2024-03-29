using PlayerDataProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Exercise3._28Server
{
    class NetMgr : Singleton<NetMgr>
    {
        Socket socket;
        public List<Client> clients = new List<Client>();

        public void Init()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("服务器开启");
            socket.Bind(new IPEndPoint(IPAddress.Any, 9999));

            socket.Listen(10);

            socket.BeginAccept(OnAccept, null);
        }

        private void OnAccept(IAsyncResult ar)
        {
            try
            {
                Socket cli = socket.EndAccept(ar);

                Console.WriteLine("连接成功");

                Client client = new Client(cli);
                IPEndPoint iPEndPoint = client.cliSocket.RemoteEndPoint as IPEndPoint;

                clients.Add(client);

                cli.BeginReceive(client.data, 0, client.data.Length, SocketFlags.None, OnAsyReveive, client);

                socket.BeginAccept(OnAccept, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public OnLinePlayer GetOnLinePlayerNoSelf(string username)
        {
            OnLinePlayer onLinePlayer = new OnLinePlayer();

            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].playerData.Username != username)
                {
                    onLinePlayer.PlayerData.Add(clients[i].playerData);
                }
            }
            return onLinePlayer;
        }

        private void OnAsyReveive(IAsyncResult ar)
        {
            try
            {
                Client client = ar.AsyncState as Client;

                int len = client.cliSocket.EndReceive(ar);

                IPEndPoint iPEndPoint = client.cliSocket.RemoteEndPoint as IPEndPoint;

                if (len <= 0)
                {
                    client.cliSocket.Shutdown(SocketShutdown.Both);
                    client.cliSocket.Close();
                    clients.Remove(client);

                    return;
                }

                if (len > 0)
                {
                    byte[] rData = client.data.Take(len).ToArray();

                    while (rData.Length > 4)
                    {
                        int msgHead = BitConverter.ToInt32(client.data, 0);    //消息头
                        int mgsId = BitConverter.ToInt32(client.data, 4);      //消息号
                        byte[] content = new byte[msgHead - 4];        //内容
                        Buffer.BlockCopy(rData, 8, content, 0, content.Length);

                        //发送消息
                        MsgData msgData = new MsgData(client, content);

                        MessageCenter.Ins.Dispatch(mgsId, msgData);
                        //剩余字节长度
                        rData = rData.Skip(msgHead + 4).ToArray();
                    }
                }

                client.cliSocket.BeginReceive(client.data, 0, client.data.Length, SocketFlags.None, OnAsyReveive, client);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void SendAllClientNoSelf(string username, int msgId, byte[] content)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (username != clients[i].playerData.Username)
                {
                    SendClient(clients[i], msgId, content);
                }
            }
        }


        public void SendClient(Client client, int msgId, byte[] content)
        {
            int msgHead = 4 + content.Length;
            byte[] data = BitConverter.GetBytes(msgHead).Concat(BitConverter.GetBytes(msgId)).Concat(content).ToArray();
            client.cliSocket.BeginSend(data, 0, data.Length, SocketFlags.None, OnAsySend, client);
        }

        private void OnAsySend(IAsyncResult ar)
        {
            try
            {
                Client client = ar.AsyncState as Client;
                client.cliSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

}

