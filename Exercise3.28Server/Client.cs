using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using PlayerDataProtocol;
using Google.Protobuf;

namespace Exercise3._28Server
{
    class Client
    {
        public Socket cliSocket;
        public byte[] data = new byte[1024];

        public UserData userData;
        public PlayerData playerData = new PlayerData();

        public Client(Socket cli)
        {
            this.cliSocket = cli;
        }

        public void SetPlayerData(PlayerData player)
        {
            playerData.Posx = player.Posx;
            playerData.Posz = player.Posz;
            playerData.Roty = player.Roty;
            playerData.AniState = player.AniState;
            playerData.MaxHp = player.MaxHp;
            playerData.NowHp = player.NowHp;
        }
    }
}
