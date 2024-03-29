using PlayerDataProtocol;
using Google.Protobuf;

namespace Exercise3._28Server
{
    class PlayerStateMgr : Singleton<PlayerStateMgr>
    {
        public void Init()
        {
            MessageCenter.Ins.AddLisenter(MsgId.CS_BEFORE_PLAYWER, OnBeforePlayer);
            MessageCenter.Ins.AddLisenter(MsgId.CS_PLAYER_STATE, OnUpdatePlayerState);
            MessageCenter.Ins.AddLisenter(MsgId.CS_PLAYER_DOWN, OnPlayerDown);
        }

        private void OnPlayerDown(MsgData obj)
        {
            NetMgr.Ins.SendAllClientNoSelf(obj.client.playerData.Username, MsgId.SC_PLAYER_DOWN, obj.client.playerData.ToByteArray());
        }

        private void OnUpdatePlayerState(MsgData obj)
        {
            //自己的数据
            PlayerData playerData = PlayerData.Parser.ParseFrom(obj.content);

            obj.client.SetPlayerData(playerData);

            NetMgr.Ins.SendAllClientNoSelf(playerData.Username, MsgId.SC_PLAYER_STATE, playerData.ToByteArray());
        }

        private void OnBeforePlayer(MsgData obj)
        {
            OnLinePlayer onLinePlayer = new OnLinePlayer();
            //获取已经上线的玩家
            onLinePlayer = NetMgr.Ins.GetOnLinePlayerNoSelf(obj.client.userData.Username);
            if (onLinePlayer != null)
            {
                NetMgr.Ins.SendClient(obj.client, MsgId.SC_BEFORE_PLAYWER, onLinePlayer.ToByteArray());
            }
        }
    }
}
