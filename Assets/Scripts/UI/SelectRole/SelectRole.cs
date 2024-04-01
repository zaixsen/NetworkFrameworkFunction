using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google.Protobuf;
using PlayerDataProtocol;
using UnityEngine.SceneManagement;

public class SelectRole : MonoBehaviour
{
    public List<Toggle> toggles = new List<Toggle>();
    List<string> roles = new List<string>();
    public Transform trs_RolePos;
    public Button btn_enterGame;
    public InputField ipt_nickname;
    int roleIndex = 0;

    private void Start()
    {
        InitRole();

        for (int i = 0; i < toggles.Count; i++)
        {
            int index = i;
            toggles[i].onValueChanged.AddListener((x) =>
            {
                if (x)
                {
                    CreatRole(index);
                }
            });
        }

        btn_enterGame.onClick.AddListener(() =>
        {
            PlayerModel.Ins.MyPlayerData.Path = roles[roleIndex];

            if (ipt_nickname.text != "")
            {
                PlayerModel.Ins.Nickneme = ipt_nickname.text;
                NetMgr.Ins.SendServer(MsgId.CS_ENTER_GAME, UTF8Encoding.UTF8.GetBytes(roles[roleIndex]));

                SceneManager.LoadScene("Game");
            }
        });
    }

    private void CreatRole(int index)
    {
        if (trs_RolePos.childCount > 0)
            Destroy(trs_RolePos.GetChild(0).gameObject);

        roleIndex = index;
        GameObject player = Instantiate(Resources.Load<GameObject>("player/" + roles[index]), trs_RolePos);
        player.transform.localScale = Vector3.one * 400;
    }

    private void InitRole()
    {
        for (int i = 0; i < 10; i++)
        {
            roles.Add("player_" + i);
        }
    }

}
