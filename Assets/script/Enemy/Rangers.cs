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
    public GameObject MeleeAttackBox;

    WaitForSeconds Delay1000 = new WaitForSeconds(1f);
    WaitForSeconds Delay300 = new WaitForSeconds(0.3f);

    void Awake()
    {
        base.Awake();
        moveSpeed = 1f;
        jumpPower = 15f;
        enemyHp = 2;
        atkCoolTime = 3.5f;
        atkCoolTimeCalc = atkCoolTime;
        DeathUse = false;
        MeleeAttackBox.SetActive(false);

        StartCoroutine(FSM());
    }

    void Update()
    {
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

                if ((Physics2D.OverlapCircle(wallCheck[0].position, 0.01f, layerMask)) || (Physics2D.OverlapCircle(wallCheck[0].position, 0.01f, layerMask3)))
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
                        canAtk = false;
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

        if (Vector2.Distance(transform.position, PlayerData.Instance.Player.transform.position) < 4.1f)
        {
            if (!IsPlayerDir())
            {
                EnemyFlip();
            }
            yield return Delay300;
            MeleeAttackBox.SetActive(true);
            yield return Delay300;
            MeleeAttackBox.SetActive(false);
        }
        else if (Vector2.Distance(transform.position, PlayerData.Instance.Player.transform.position) < 15f)
        {
            MyAnimSetTrigger("Attack");
        }

        yield return Delay1000;
        currentState = State.Idle;
    }

    void Fire()
    {
        GameObject bulletClone = Instantiate(Bullet, genPoint.position, transform.rotation);
        bulletClone.GetComponent<Rigidbody2D>().velocity = transform.right * -transform.localScale.x * 10f;
        bulletClone.transform.localScale = new Vector2(transform.localScale.x, 1f);
    }

    void FixedUpdate()
    {
        if (EndCheck.EndChecking)
        {
            EndCheck_ing = true;
            EndCheck.RangerCount++;
            Destroy(gameObject);
        }
    }
}
