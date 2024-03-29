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
            InitRole();
            MessageCenter.Ins.AddLisenter(MsgId.CS_LOGIN, OnLogin);
        }

        private void InitRole()
        {
            for (int i = 0; i < 10; i++)
            {
                roles.Add("player_" + i);
            }
        }

        private void OnLogin(MsgData obj)
        {
            UserData userData = UserData.Parser.ParseFrom(obj.content);
            if (dic_users.ContainsKey(userData.Username))
            {
                Console.WriteLine("已存在" + userData.Username);
                return;
            }

            obj.client.userData = userData;
            obj.client.playerData.Username = userData.Username;
            obj.client.playerData.Path = roles[random.Next(0, roles.Count)];

            NetMgr.Ins.SendClient(obj.client, MsgId.SC_LOGIN, obj.client.playerData.ToByteArray());
            NetMgr.Ins.SendAllClientNoSelf(obj.client.playerData.Username, MsgId.SC_OTHER_PLAYWE_ONLINE, obj.client.playerData.ToByteArray());
        }
    }
}
