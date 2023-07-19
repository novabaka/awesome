using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rangers : Enemy
{
    public enum State
    {
        Idle,
        Run,
        Attack,
    };
    public State currentState = State.Idle;

    public Transform[] wallCheck;
    public Transform genPoint;
    public GameObject Bullet;

    WaitForSeconds Delay1000 = new WaitForSeconds(1f);

    void Awake()
    {
        base.Awake();
        moveSpeed = 1f;
        jumpPower = 15f;
        enemyHp = 2;
        atkCoolTime = 3f;
        atkCoolTimeCalc = atkCoolTime;
        DeathUse = false;

        StartCoroutine(FSM());
    }

    IEnumerator FSM()
    {
        while (true)
        {
            yield return StartCoroutine(currentState.ToString());
        }
    }

    IEnumerator Idle()
    {
        yield return null;
        MyAnimSetTrigger("Idle");

        if (Random.value > 0.5f)
        {
            EnemyFlip();
        }
        yield return Delay1000;
        currentState = State.Run;
    }
    IEnumerator Run()
    {
        yield return null;
        float runTime = Random.Range(2f, 4f);
        while (runTime >= 0f)
        {
            runTime -= Time.deltaTime;
            MyAnimSetTrigger("Run");
            if (!isHit && !DeathOn)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.velocity = new Vector2(-transform.localScale.x * moveSpeed, rb.velocity.y);

                if (Physics2D.OverlapCircle(wallCheck[1].position, 0.01f, layerMask))
                {
                    EnemyFlip();
                }
                if (canAtk)
                {
                    if (Vector2.Distance(transform.position, PlayerData.Instance.Player.transform.position) < 15f)
                    {
                        if (!IsPlayerDir())
                        {
                            EnemyFlip();
                        }
                        currentState = State.Attack;
                        break;
                    }
                }
            }
            yield return null;
        }
        if (currentState != State.Attack)
        {
            if (Random.value > 0.5f)
            {
                EnemyFlip();
            }
            else
            {
                currentState = State.Idle;
            }
        }
    }
    IEnumerator Attack()
    {
        yield return null;

        canAtk = false;

        MyAnimSetTrigger("Attack");

        yield return Delay1000;
        currentState = State.Idle;
    }

    void Fire()
    {
        GameObject bulletClone = Instantiate(Bullet, genPoint.position, transform.rotation);
        bulletClone.GetComponent<Rigidbody2D>().velocity = transform.right * -transform.localScale.x * 10f;
        bulletClone.transform.localScale = new Vector2(transform.localScale.x, 1f);
    }
}
