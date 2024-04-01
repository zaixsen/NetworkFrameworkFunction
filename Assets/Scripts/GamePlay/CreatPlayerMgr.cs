using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerDataProtocol;
using Google.Protobuf;

public class CreatPlayerMgr : MonoBehaviour
{
    Dictionary<string, OtherPlayer> dic_otherPlayers = new Dictionary<string, OtherPlayer>();
    GameObject player;

    private void Start()
    {
        MesssageCenter.Ins.AddLisenter(MsgId.SC_OTHER_PLAYWE_ONLINE, OnOtherPlayerOnline);
        MesssageCenter.Ins.AddLisenter(MsgId.SC_BEFORE_PLAYWER, OnOtherBeforePlayerOnline);
        MesssageCenter.Ins.AddLisenter(MsgId.SC_PLAYER_STATE, OnOtherStateUpdate);
        MesssageCenter.Ins.AddLisenter(MsgId.SC_PLAYER_DOWN, OnOtherPlayerDown);
        MesssageCenter.Ins.AddLisenter(MsgId.SC_PLAYER_HIT, OnPlayerHit);

        CreatSelf();
    }

    private void OnPlayerHit(byte[] obj)
    {
        PlayerData playerData = PlayerData.Parser.ParseFrom(obj);
        if (PlayerModel.Ins.MyPlayerData.Username == playerData.Username)
            player.GetComponent<PlayerMove>().SetHp();

        if (dic_otherPlayers.ContainsKey(playerData.Username))
        {
            dic_otherPlayers[playerData.Username].SetOtherPlayer(playerData);
            dic_otherPlayers[playerData.Username].SetHp();
        }
    }

    private void OnOtherPlayerDown(byte[] obj)
    {
        PlayerData otherPlayerData = PlayerData.Parser.ParseFrom(obj);

        if (dic_otherPlayers.ContainsKey(otherPlayerData.Username))
        {
            dic_otherPlayers[otherPlayerData.Username].DestoryPlayer();

            dic_otherPlayers.Remove(otherPlayerData.Username);
        }
    }

    /// <summary>
    /// 游戏玩家状态更新
    /// </summary>
    /// <param name="obj"></param>
    private void OnOtherStateUpdate(byte[] obj)
    {
        PlayerData otherPlayerData = PlayerData.Parser.ParseFrom(obj);


        if (dic_otherPlayers.ContainsKey(otherPlayerData.Username))
        {
            dic_otherPlayers[otherPlayerData.Username].SetState(otherPlayerData);
        }
    }

    private void CreatSelf()
    {
        player = Instantiate(Resources.Load<GameObject>("player/" + PlayerModel.Ins.GetRolePath()));
        player.AddComponent<PlayerMove>();

        NetMgr.Ins.SendServer(MsgId.CS_BEFORE_PLAYWER, new byte[0]);
    }

    private void OnOtherBeforePlayerOnline(byte[] obj)
    {
        OnLinePlayer onLinePlayer = OnLinePlayer.Parser.ParseFrom(obj);

        for (int i = 0; i < onLinePlayer.PlayerData.Count; i++)
        {
            CreatOtherPlayer(onLinePlayer.PlayerData[i]);
        }
    }

    /// <summary>
    /// 创建其他玩家
    /// </summary>
    /// <param name="playerData"></param>
    private void CreatOtherPlayer(PlayerData playerData)
    {
        GameObject otherPlayer = Instantiate(Resources.Load<GameObject>("player/" + playerData.Path));
        OtherPlayer other = otherPlayer.AddComponent<OtherPlayer>();

        other.SetOtherPlayer(playerData);
        other.SetState(playerData);

        dic_otherPlayers.Add(playerData.Username, other);
    }

    private void OnOtherPlayerOnline(byte[] obj)
    {
        PlayerData otherPlayerData = PlayerData.Parser.ParseFrom(obj);
        CreatOtherPlayer(otherPlayerData);
    }

}
