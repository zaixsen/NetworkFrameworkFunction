using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerDataProtocol;
using Google.Protobuf;
using System;

public class PlayerModel : Singleton<PlayerModel>
{
    private PlayerData myPlayerData = new PlayerData();
    private string nickneme;
    public string Nickneme { get => nickneme; set => nickneme = value; }
    public PlayerData MyPlayerData { get => myPlayerData; set => myPlayerData = value; }

    public void SetMyPlayer(PlayerData player)
    {
        myPlayerData = player;
    }

    public void SetPlayerHp(int maxHp, int nowHp)
    {
        myPlayerData.MaxHp = maxHp;
        myPlayerData.NowHp = nowHp;
    }

    public void SetPlayerData(Vector3 position, Vector3 eulerAngles, AnimitorState state)
    {
        MyPlayerData.Posx = position.x;
        MyPlayerData.Posz = position.z;
        MyPlayerData.Roty = eulerAngles.y;
        MyPlayerData.AniState = state;

        NetMgr.Ins.SendServer(MsgId.CS_PLAYER_STATE, MyPlayerData.ToByteArray());
    }

    public string GetRolePath()
    {
        return MyPlayerData.Path;
    }
}
