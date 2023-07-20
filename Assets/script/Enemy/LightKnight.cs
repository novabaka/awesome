using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightKnight : Enemy
{
    int RandomAtk = 0;
    public enum State
    {
        Idle,
        Run,
        Attack
    };
    public State currentState = State.Idle;

    public GameObject AttackBoxCollider;
    public GameObject GuardBoxCollider;

    bool atking = false;

    public Transform[] wallCheck;
    WaitForSeconds Delay500 = new WaitForSeconds(0.5f);

    void Awake()
    {
        base.Awake();
        moveSpeed = 3f;
        jumpPower = 25f;
        enemyHp = 3;
        atkCoolTime += 0.8f;
        atkCoolTimeCalc = atkCoolTime;
        AttackBoxCollider.SetActive(false);
        GuardBoxCollider.SetActive(false);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        DeathUse = true;

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
        yield return Delay500;
        currentState = State.Run;
    }
    IEnumerator Run()
    {
        yield return null;
        float runTime = Random.Range(2f, 4f);
        while (runTime >= 0f)
        {
            runTime -= Time.deltaTime;
            if (!isHit && !DeathOn)
            {
                if (!atking)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    MyAnimSetTrigger("Run");
                    rb.velocity = new Vector2(-transform.localScale.x * moveSpeed, rb.velocity.y);
                }
                else if (atking)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                }

                
                if (Physics2D.OverlapCircle(wallCheck[0].position, 0.01f, layerMask))
                {
                    EnemyFlip();
                }

                if (isGround && canAtk)
                {
                    if (Vector2.Distance(transform.position, PlayerData.Instance.Player.transform.position) < 7.5f)
                    {
                        if (currentState != State.Attack)
                        {
                            if (!IsPlayerDir())
                            {
                                EnemyFlip();
                            }
                        }
                        if (Physics2D.OverlapCircle(wallCheck[0].position, 0.01f, layerMask2))
                        {
                            RandomAtk = Random.Range(1, 4);
                            
                            if (RandomAtk == 1 || RandomAtk == 2)
                            {
                                isGuard = false;
                                currentState = State.Attack;
                            }
                            else if (RandomAtk == 3)
                            {
                                isGuard = true;
                                currentState = State.Attack;
                            }
                        }
                        break;
                    }
                }
            }
            yield return null;
        }
    }
    IEnumerator Attack()
    {
        yield return null;
        if (!isHit && isGround && canAtk)
        {
            if (!isGuard)
            {
                atking = true;
                canAtk = false;
                MyAnimSetTrigger("Attack");
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else if (isGuard)
            {
                atking = true;
                canAtk = false;
                MyAnimSetTrigger("Guard");
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
        else
        {
            currentState = State.Run;
        }
    }
    private void AttackStart()
    {
        if (!isGuard)
        {
            AttackBoxCollider.SetActive(true);
        }
        else if (isGuard)
        {
            GuardBoxCollider.SetActive(true);
        }
    }
    private void AttackEnd()
    {
        if (!isGuard)
        {
            AttackBoxCollider.SetActive(false);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else if (isGuard)
        {
            GuardBoxCollider.SetActive(false);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        atking = false;
        currentState = State.Idle;
    }

    void Update()
    {
        GroundCheck();
        if (!isHit && isGround && !IsPlayingAnim("Run"))
        {
            MyAnimSetTrigger("Idle");
        }
    }
}
