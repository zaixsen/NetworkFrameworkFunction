
using System;
using System.Collections;
using System.Collections.Generic;

namespace Exercise3._28Server
{
    class MessageCenter : Singleton<MessageCenter>
    {
        Dictionary<int, Action<MsgData>> dic_msg = new Dictionary<int, Action<MsgData>>();

        public void AddLisenter(int msgId, Action<MsgData> action)
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

        public void Dispatch(int msgId, MsgData msgData)
        {
            if (dic_msg.ContainsKey(msgId))
            {
                dic_msg[msgId]?.Invoke(msgData);
            }
        }
    }
}