using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HB : MonoBehaviour
{
    public static bool Hit = false;

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player_Attack")) // 플레이어가 범위 안에 들어오면 Hit 활성화
        {
            Hit = true;
        }
    }

    void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player_Attack")) // 플레이어가 범위 밖으로 나가면 Hit 비활성화
        {
            Hit = false;
        }
    }
}
