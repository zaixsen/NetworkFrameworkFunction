using Google.Protobuf;
using PlayerDataProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherPlayer : MonoBehaviour
{
    private PlayerData otherPlayerData;
    Transform trs_hp;
    Slider sli_hp;
    Animator animator;

    private void Start()
    {
        trs_hp = Instantiate(Resources.Load<GameObject>("HpSlider"), GameObject.Find("Canvas/Hplay").transform).transform;
        sli_hp = trs_hp.GetComponent<Slider>();
        sli_hp.maxValue = otherPlayerData.MaxHp;
        sli_hp.value = otherPlayerData.NowHp;
    }

    private void Update()
    {
        trs_hp.position = Camera.main.WorldToScreenPoint(transform.position) + Vector3.up * 120;

    }

    public void SetOtherPlayer(PlayerData player)
    {
        animator = GetComponent<Animator>();
        otherPlayerData = player;
    }
    public void SetHp()
    {
        sli_hp.maxValue = otherPlayerData.MaxHp;
        sli_hp.value = otherPlayerData.NowHp;
    }
    public void SetState(PlayerData player)
    {
        otherPlayerData = player;
        Vector3 pos = new Vector3(player.Posx, 0, player.Posz);
        Vector3 angle = new Vector3(0, player.Roty, 0);
        transform.position = pos;
        transform.eulerAngles = angle;

        switch (player.AniState)
        {
            case AnimitorState.Idle:
                animator.SetBool("run", false);
                break;
            case AnimitorState.Move:
                animator.SetBool("run", true);
                break;
            case AnimitorState.Atk:
                animator.SetTrigger("Atk");
                break;
            default:
                break;
        }
    }

    void Atk()
    {
        GameObject bullet = Instantiate(Resources.Load<GameObject>("Pop"));
        Vector3 dir = transform.forward + transform.up;
        bullet.GetComponent<PopItem>().SetForce(dir, 1600);
        bullet.transform.position = transform.position + Vector3.up + transform.forward * 0.5f;
        bullet.transform.eulerAngles = transform.eulerAngles;
    }
    public void DestoryPlayer()
    {
        Destroy(trs_hp.gameObject);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Destroy(collision.collider.gameObject);
        }
    }
}
