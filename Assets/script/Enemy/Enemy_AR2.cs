using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AR2 : MonoBehaviour
{
    public string targetObjectName; // 대상 이름 string 변수

    public static bool AttackRange = false;

    GameObject targetObject; // 타겟 지정

    void Start()
    {
        targetObject = GameObject.Find(targetObjectName); // 타겟을 찾는 코드
    }

    void OnTriggerStay2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player")) // 플레이어가 범위 안에 들어오면 AttackRange 활성화
        {
            AttackRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) // 나가면 range 비활성화
    {
        if (collision.gameObject.tag == ("Player"))
        {
            AttackRange = false;
        }
    }
}
