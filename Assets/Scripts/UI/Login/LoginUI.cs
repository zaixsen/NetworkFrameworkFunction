using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerDataProtocol;
using Google.Protobuf;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LoginUI : MonoBehaviour
{
    public GameObject go_selectRole;
    public GameObject go_tip;

    public InputField ipt_username;
    public Button btn_sureGame;
    public Button btn_registerGame;

    public void Awake()
    {
        MesssageCenter.Ins.AddLisenter(MsgId.SC_LOGIN, OnShowInfo);
        MesssageCenter.Ins.AddLisenter(MsgId.SC_REGISTER, OnShowInfo);
    }
    private void Start()
    {
        btn_sureGame.onClick.AddListener(() =>
        {
            UserData userData = new UserData();
            userData.Username = ipt_username.text;
            PlayerModel.Ins.MyPlayerData.Username = ipt_username.text;
            PlayerModel.Ins.SetPlayerHp(3, 3);
            NetMgr.Ins.SendServer(MsgId.CS_LOGIN, userData.ToByteArray());
        });

        btn_registerGame.onClick.AddListener(() =>
        {
            UserData userData = new UserData();
            userData.Username = ipt_username.text;
            NetMgr.Ins.SendServer(MsgId.CS_REGISTER, userData.ToByteArray());
        });
    }

    private void OnShowInfo(byte[] obj)
    {
        string flag = UTF8Encoding.UTF8.GetString(obj);
        if (flag == "密码正确")
        {
            gameObject.SetActive(false);
            go_selectRole.SetActive(true);
        }
        else
        {
            go_tip.SetActive(true);
            go_tip.GetComponentInChildren<Text>().text = flag;
            Invoke("Disappear", 1);
        }
    }

    private void Disappear()
    {
        go_tip.SetActive(false);
    }



    private void OnLogin(byte[] obj)
    {
        PlayerData player = PlayerData.Parser.ParseFrom(obj);
        PlayerModel.Ins.SetMyPlayer(player);
        SceneManager.LoadScene("Game");
    }
}
