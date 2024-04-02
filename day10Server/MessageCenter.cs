using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examDay10Server
{
    public class MessageCenter : Singleton<MessageCenter>
    {
        Dictionary<int, Action<MsgData>> dic_messages = new Dictionary<int, Action<MsgData>>();

        public void AddLisenter(int msgId, Action<MsgData> action)
        {
            if (dic_messages.ContainsKey(msgId))
            {
                dic_messages[msgId] += action;
            }
            else
            {
                dic_messages.Add(msgId, action);
            }
        }

        public void Dispatch(int msgId, MsgData msgData)
        {
            if (dic_messages.ContainsKey(msgId))
            {
                dic_messages[msgId]?.Invoke(msgData);
            }
        }
    }
}
