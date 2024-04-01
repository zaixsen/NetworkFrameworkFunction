using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        NetMgr.Ins.Init();
    }


    private void Update()
    {
        NetMgr.Ins.NetUpdate();
    }

    private void OnApplicationQuit()
    {
        NetMgr.Ins.SendServer(MsgId.CS_PLAYER_DOWN, new byte[0]);
    }
}
