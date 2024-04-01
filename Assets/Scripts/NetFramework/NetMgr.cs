using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class NetMgr : Singleton<NetMgr>
{
    Socket socket;
    byte[] data = new byte[1024];
    Queue<byte[]> messages = new Queue<byte[]>();

    public void Init()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        socket.BeginConnect("127.0.0.1", 9999, OnConnect, null);
    }

    private void OnConnect(IAsyncResult ar)
    {
        try
        {
            socket.EndConnect(ar);

            socket.BeginReceive(data, 0, data.Length, SocketFlags.None, OnAsyReceive, null);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    private void OnAsyReceive(IAsyncResult ar)
    {
        try
        {
            int len = socket.EndReceive(ar);

            if (len > 0)
            {
                byte[] rData = data.Take(len).ToArray();

                while (rData.Length > 4)
                {
                    int head = BitConverter.ToInt32(rData, 0);
                    byte[] content = new byte[head];
                    Buffer.BlockCopy(rData, 4, content, 0, head);

                    messages.Enqueue(content);

                    rData = rData.Skip(head + 4).ToArray();
                }
            }
            socket.BeginReceive(data, 0, data.Length, SocketFlags.None, OnAsyReceive, null);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public void SendServer(int msgId, byte[] content)
    {
        int head = 4 + content.Length;
        byte[] data = BitConverter.GetBytes(head).Concat(BitConverter.GetBytes(msgId)).Concat(content).ToArray();
        socket.BeginSend(data, 0, data.Length, SocketFlags.None, OnSend, null);
    }

    private void OnSend(IAsyncResult ar)
    {
        try
        {
            socket.EndSend(ar);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public void NetUpdate()
    {
        if (messages.Count > 0)
        {
            byte[] content = messages.Dequeue();
            int msgId = BitConverter.ToInt32(content, 0);

            content = content.Skip(4).ToArray();
            MesssageCenter.Ins.Dispatch(msgId, content);
        }
    }
}
