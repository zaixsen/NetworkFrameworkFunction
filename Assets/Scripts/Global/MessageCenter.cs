using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesssageCenter : Singleton<MesssageCenter>
{
    Dictionary<int, Action<byte[]>> dic_msg = new Dictionary<int, Action<byte[]>>();

    public void AddLisenter(int msgId, Action<byte[]> action)
    {
        if (dic_msg.ContainsKey(msgId))
        {
            dic_msg[msgId] += action;
        }
        else
        {
            dic_msg.Add(msgId, action);
        }
    }

    public void Dispatch(int msgId, byte[] msgData)
    {
        if (dic_msg.ContainsKey(msgId))
        {
            dic_msg[msgId]?.Invoke(msgData);
        }
    }
}