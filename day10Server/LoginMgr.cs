using examDay10Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day10Server
{
    internal class LoginMgr : Singleton<LoginMgr>
    {
        List<string> _names = new List<string>();

        public void Init()
        {
            MessageCenter.Ins.AddLisenter(MsgId.CS_LOGIN, OnLogin);
        }

        private void OnLogin(MsgData data)
        {
            string username = UTF8Encoding.UTF8.GetString(data.data);
            if (!_names.Contains(username))
            {
                data.client.myPlayerData.UserId = username;
                _names.Add(username);
                NetMgr.Ins.SendClient(data.client, MsgId.SC_LOGIN, UTF32Encoding.UTF8.GetBytes("成功"));
            }
            else
            {
                NetMgr.Ins.SendClient(data.client, MsgId.SC_LOGIN, UTF32Encoding.UTF8.GetBytes("有相同用户名"));
            }
        }


    }
}
