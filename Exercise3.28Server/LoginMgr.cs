using PlayerDataProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;

namespace Exercise3._28Server
{
    class LoginMgr : Singleton<LoginMgr>
    {
        Dictionary<string, UserData> dic_users = new Dictionary<string, UserData>();
        List<string> roles = new List<string>();
        Random random = new Random();

        public void Init()
        {
            MessageCenter.Ins.AddLisenter(MsgId.CS_LOGIN, OnLogin);
            MessageCenter.Ins.AddLisenter(MsgId.CS_REGISTER, OnRegister);
        }

        private void OnRegister(MsgData obj)
        {
            UserData userData = UserData.Parser.ParseFrom(obj.content);
            string str = "";
            if (dic_users.ContainsKey(userData.Username))
            {
                str = "已经存在此账户！！！";
            }
            else
            {
                dic_users.Add(userData.Username, userData);
                str = "注册成功！！！";
            }
            NetMgr.Ins.SendClient(obj.client, MsgId.SC_REGISTER, UTF8Encoding.UTF8.GetBytes(str));
        }

        private void OnLogin(MsgData obj)
        {
            UserData userData = UserData.Parser.ParseFrom(obj.content);

            if (!dic_users.ContainsKey(userData.Username))
            {
                NetMgr.Ins.SendClient(obj.client, MsgId.SC_LOGIN, UTF8Encoding.UTF8.GetBytes("无此账户！！！"));
                return;
            }

            obj.client.userData = userData;
            obj.client.playerData.Username = userData.Username;
            obj.client.playerData.NowHp = 3;
            obj.client.playerData.MaxHp = 3;

            //obj.client.playerData.Path = roles[random.Next(0, roles.Count)];

            NetMgr.Ins.SendClient(obj.client, MsgId.SC_LOGIN, UTF8Encoding.UTF8.GetBytes("密码正确"));

            //NetMgr.Ins.SendAllClientNoSelf(obj.client.playerData.Username, MsgId.SC_OTHER_PLAYWE_ONLINE, obj.client.playerData.ToByteArray());
        }
    }
}
