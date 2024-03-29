using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise3._28Server
{
    class MsgId
    {
        public const int CS_LOGIN = 1001;
        public const int SC_LOGIN = 1002;

        //其他玩家上线
        public const int SC_OTHER_PLAYWE_ONLINE = 1003;

        //上线之前的玩家
        public const int CS_BEFORE_PLAYWER = 1004;
        public const int SC_BEFORE_PLAYWER = 1005;

        //玩家状态
        public const int CS_PLAYER_STATE = 1006;
        public const int SC_PLAYER_STATE = 1007;

        //玩家下线
        public const int CS_PLAYER_DOWN = 1008;
        public const int SC_PLAYER_DOWN = 1009;
    }
}
