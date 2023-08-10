using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueKnight : Enemy
{
    public Transform[] wallCheck;

    public GameObject AttackBoxCollider;

    public float range = 7.5f;

    private void Awake()
    {
        base.Awake();
        moveSpeed = 2f;
        enemyHp = 2;
        atkCoolTime += 0.8f;
        atkCoolTimeCalc = atkCoolTime;
        AttackBoxCollider.SetActive(false);
        DeathUse = true;
    }

    void Update()
    {
        if (!isHit && !Attack_ing && !DeathOn)
        {
            rb.velocity = new Vector2(-transform.localScale.x * moveSpeed, rb.velocity.y);

            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            if (Physics2D.OverlapCircle(wallCheck[0].position, 0.01f, layerMask))
            {
                EnemyFlip();
                ABTime = 1.5f;
            }

            if (Physics2D.OverlapCircle(wallCheck[0].position, 0.01f, layerMask3))
            {
                EnemyFlip();
                ABTime = 1.5f;
            }

            if (Vector2.Distance(transform.position, PlayerData.Instance.Player.transform.position) < range)
            {
                if (!IsPlayerDir() && canAtk)
                {
                    if (ABTime == 0)
                    {
                        EnemyFlip();
                    }
                }
            }

            if (Physics2D.OverlapCircle(wallCheck[0].position, 0.01f, layerMask2))
            {
                if (canAtk)
                {
                    canAtk = false;
                    Anim.SetTrigger("Attack");
                    Attack_ing = true;
                }
            }
        }
        else if (Attack_ing)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    void Attacking()
    {
        theAudio.PlayOneShot(sound[1]);
        AttackBoxCollider.SetActive(true);
    }
    void AttackEnd()
    {
        AttackBoxCollider.SetActive(false);
        Attack_ing = false;
    }
    void FixedUpdate()
    {
        if (Hit_ing)
        {
            Hit_ing = false;
            AttackEnd();
        }

        if (EndCheck.EndChecking)
        {
            EndCheck_ing = true;
            EndCheck.BKnightCount++;
            Destroy(gameObject);
        }
    }
}
