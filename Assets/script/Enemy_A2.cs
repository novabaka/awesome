using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_A2 : MonoBehaviour
{
    public Player PlayerScript;

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player_Hit")) // 플레이어가 범위 안에 들어오면
        {
            if (Chase2.Attacking == true)
            {
                PlayerScript.Player_Dead();
            }
        }
    }
}
