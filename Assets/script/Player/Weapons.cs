using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public static bool GuardingAtk = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("EnemyGuardBox"))
        {
            GuardingAtk = true;

            Invoke("GuardAtk", 1f);
        }
    }

    void GuardAtk()
    {
        GuardingAtk = false;
    }
}
