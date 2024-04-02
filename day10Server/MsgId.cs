using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day10Server
{
    internal class MsgId
    {
        //登录
        public const int CS_LOGIN = 1001;
        public const int SC_LOGIN = 1002;

        //更新大厅房间 客户端申请
        public const int CS_UPDATE_ROOM = 1003;
        public const int SC_UPDATE_ROOM = 1004;

        //创建房间
        public const int CS_CREAT_ROOM = 1005;
        public const int SC_CREAT_ROOM = 1006;

        //加入房间
        public const int CS_JOIN_ROOM = 1007;
        public const int SC_JOIN_ROOM = 1008;

        public const int SC_REFRESH_ROOM = 1009;
        public const int CS_QUIT_ROOM = 1010;
        public const int SC_QUIT_ROOM = 1011;


    }
}
