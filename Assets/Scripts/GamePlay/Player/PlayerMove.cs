using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using PlayerDataProtocol;
using UnityEngine.UI;
using System;

public class PlayerMove : MonoBehaviour
{
    Animator animator;
    Transform trs_hp;
    Slider sli_hp;
    public int force = 1600;
    private void Start()
    {
        animator = GetComponent<Animator>();
        trs_hp = Instantiate(Resources.Load<GameObject>("HpSlider"), GameObject.Find("Canvas/Hplay").transform).transform;

        sli_hp = trs_hp.GetComponent<Slider>();
        sli_hp.maxValue = PlayerModel.Ins.MyPlayerData.MaxHp;
        sli_hp.value = PlayerModel.Ins.MyPlayerData.NowHp;
    }
    bool isSendMsg = false;

    private void Update()
    {
        trs_hp.position = Camera.main.WorldToScreenPoint(transform.position) + Vector3.up * 120;
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Atk");

            PlayerModel.Ins.SetPlayerData(transform.position, transform.eulerAngles, AnimitorState.Atk);

        }
    }

    public void SetHp()
    {
        sli_hp.value = PlayerModel.Ins.MyPlayerData.NowHp;
    }

    void Atk()
    {
        GameObject bullet = Instantiate(Resources.Load<GameObject>("Pop"));
        Vector3 dir = transform.forward + transform.up;
        bullet.GetComponent<PopItem>().SetForce(dir, force);
        bullet.transform.position = transform.position + Vector3.up + transform.forward * 0.5f;
        bullet.transform.eulerAngles = transform.eulerAngles;
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 pos = new Vector3(x, 0, y);

        if (pos != Vector3.zero)
        {
            animator.SetBool("run", true);
            isSendMsg = true;
            transform.LookAt(transform.position + pos);
            transform.Translate(Vector3.forward * Time.deltaTime * 2);

            PlayerModel.Ins.SetPlayerData(transform.position, transform.eulerAngles, AnimitorState.Move);
        }
        else if (pos == Vector3.zero && isSendMsg)
        {
            PlayerModel.Ins.SetPlayerData(transform.position, transform.eulerAngles, AnimitorState.Idle);

            animator.SetBool("run", false);
            isSendMsg = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Destroy(collision.collider.gameObject);
            PlayerModel.Ins.MyPlayerData.NowHp--;
            //sli_hp.value = PlayerModel.Ins.MyPlayerData.NowHp;
            NetMgr.Ins.SendServer(MsgId.CS_PLAYER_HIT, PlayerModel.Ins.MyPlayerData.ToByteArray());
        }
    }
}
