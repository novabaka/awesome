using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightKnight : Enemy
{
    bool isGuard = false;
    float GuardAttackDT = 0f;
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
    public GameObject GuardAttackCollider;

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
        GuardAttackCollider.SetActive(false);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        DeathUse = true;

        StartCoroutine(CalcGuardDT());
        StartCoroutine(FSM());
    }

    IEnumerator CalcGuardDT()
    {
        while (true)
        {
            yield return null;
            if (GuardAttackDT > 0)
            {
                GuardAttackDT -= Time.deltaTime;
            }
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

                    if (Vector2.Distance(transform.position, PlayerData.Instance.Player.transform.position) > 3.5f)
                    {
                        if (Vector2.Distance(transform.position, PlayerData.Instance.Player.transform.position) < 8.5f)
                        {
                            if (!IsPlayerDir())
                            {
                                if (ABTime == 0)
                                {
                                    EnemyFlip();
                                }
                            }
                        }
                    }
                }
                else if (atking)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                }

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

                if (isGround && canAtk)
                {
                    if (Vector2.Distance(transform.position, PlayerData.Instance.Player.transform.position) < 7.5f)
                    {
                        if (Physics2D.OverlapCircle(wallCheck[0].position, 0.01f, layerMask2))
                        {
                            RandomAtk = Random.Range(1, 5);
                            
                            if (RandomAtk == 1 || RandomAtk == 2 || RandomAtk == 3)
                            {
                                isGuard = false;
                                currentState = State.Attack;
                            }
                            else if (RandomAtk == 4)
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
    void AttackStart()
    {
        if (!isGuard)
        {
            theAudio.PlayOneShot(sound[1]);
            AttackBoxCollider.SetActive(true);
        }
        else if (isGuard)
        {
            GuardBoxCollider.SetActive(true);
            nowGuarding = true;
        }
    }
    void AttackEnd()
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
            nowGuarding = false;
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

    void FixedUpdate()
    {
        if (Hit_ing)
        {
            Hit_ing = false;
            AttackEnd();
        }
        
        if (GuardAttackD)
        {
            if (GuardAttackDT <= 0)
            {
                GuardAttackDT = 0.3f;
                GuardAttackD = false;
                StartCoroutine(GBA(0.05f));
            }
        }

        if (EndCheck.EndChecking)
        {
            EndCheck_ing = true;
            EndCheck.LKnightCount++;
            Destroy(gameObject);
        }
    }

    IEnumerator GBA(float gwt)
    {
        GuardAttackCollider.SetActive(true);
        yield return new WaitForSeconds(gwt);
        GuardAttackCollider.SetActive(false);
    }
}
