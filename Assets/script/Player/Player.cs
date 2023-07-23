using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumppower;

    bool DamLeftFlag;

    bool Dashing = false;
    bool LDashing = false;
    bool RDashing = false;
    float LDashCount = 0;
    float RDashCount = 0;
    public float Timer = 0;
    public float gauge = 0;

    public int PlayerHp;

    int Weaponnum = 0;

    bool GAtkcheck = false;

    int count1 = 0;
    int count2 = 0;
    int count3 = 0;
    int count4 = 0;

    bool IsHit = false;
    public float hitRecovery = 0.2f;
    int death = 0;

    int AttackBox = 0;
    bool AttackBoxs = false;
    bool AttackBoxss = false;

    float vx = 0;
    int leftFlip = 0;
    bool leftFlag = false;
    bool pushFlag = false;
    bool jumpFlag = false;
    bool groundFlag = false;

    bool Attacking = false;
    bool Attackmotion = false;

    Rigidbody2D rbody; // 리지드바디 가져오는 코드
    Animator anim; // 애니메이터 가져오는 코드

    public GameObject AttackBoxCollider;
    public GameObject AttackBoxTransform;
    //public GameObject AttackBoxCollider2;
    public GameObject hitBoxCollider;

    void Awake()
    {
        anim = GetComponent<Animator>();
        AttackBoxCollider.SetActive(false);
        //AttackBoxCollider2.SetActive(false);
        StartCoroutine(ResetCollider());
        StartCoroutine(Dash());
        StartCoroutine(DashGauge());
    }

    IEnumerator ResetCollider()
    {
        while (true)
        {
            yield return null;
            if (!hitBoxCollider.activeInHierarchy)
            {
                yield return new WaitForSeconds(hitRecovery);
                hitBoxCollider.SetActive(true);
            }
        }
    }

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        death = 0;
    }

    IEnumerator DashGauge()
    {
        while (true)
        {
            yield return null;
            if (gauge <= 30 && Timer <= 10)
            {
                Timer += Time.deltaTime;
                gauge += Time.deltaTime;
                gauge += Time.deltaTime;
                gauge += Time.deltaTime;
                if (gauge >= 30)
                {
                    gauge = 30;
                }
                if (Timer >= 10)
                {
                    Timer = 10;
                }
            }
            else if (gauge >= 0)
            {
                gauge = 0;
            }
        }
    }

    IEnumerator Dash()
    {
        while (true)
        {
            yield return null;
            if (!Dashing)
            {
                if (LDashing)
                {
                    LDashCount -= Time.deltaTime;
                    if (LDashCount <= 0)
                    {
                        LDashing = false;
                    }
                }
                else if (RDashing)
                {
                    RDashCount -= Time.deltaTime;
                    if (RDashCount <= 0)
                    {
                        RDashing = false;
                    }
                }
            }
        }
    }
    IEnumerator DashOn(float stayTime)
    {
        if (RDashing)
        {
            anim.SetTrigger("isDash");
            for (int i =0; i <= gauge; i++)
            {
                transform.Translate(0.15f, 0, 0);
                yield return new WaitForSeconds(stayTime);
            }
            Timer = 0;
            gauge = 0;
            RDashing = false;
            Dashing = false;
        }
        else if (LDashing)
        {
            anim.SetTrigger("isDash");
            for (int i = 0; i <= gauge; i++)
            {
                transform.Translate(-0.15f, 0, 0);
                yield return new WaitForSeconds(stayTime);
            }
            Timer = 0;
            gauge = 0;
            LDashing = false;
            Dashing = false;
        }
    }

    void Update()
    {
        vx = 0;
        if (!IsHit && !Attacking && !Dashing)
        {
            if (Input.GetKey("right"))
            {
                vx = speed;
                leftFlag = false;
                Invoke("AnimOn", 0);
            }
            if (Input.GetKey("left"))
            {
                vx = -speed;
                leftFlag = true;
                Invoke("AnimOn", 0);
            }
            if (Input.GetKeyDown("right"))
            {
                if (RDashing && Timer >= 3)
                {
                    Dashing = true;
                    StartCoroutine(DashOn(0.01f));
                }
                RDashing = true;
                LDashing = false;
                RDashCount = 0.2f;
                LDashCount = 0;
            }
            if (Input.GetKeyDown("left"))
            {
                if (LDashing && Timer >= 3)
                {
                    Dashing = true;
                    StartCoroutine(DashOn(0.01f));
                }
                LDashing = true;
                RDashing = false;
                LDashCount = 0.2f;
                RDashCount = 0;
            }
            if (Input.GetKeyUp("right"))
            {
                Invoke("AnimOff", 0);
            }
            if (Input.GetKeyUp("left"))
            {
                Invoke("AnimOff", 0);
            }
            if (Input.GetKey("space") && groundFlag)
            {
                if (pushFlag == false)
                {
                    jumpFlag = true;
                    pushFlag = true;
                }
            }
            else
            {
                pushFlag = false;
            }

            if (Input.GetKeyDown("a"))
            {
                if (!Attacking && groundFlag && !Attackmotion)
                {
                    Attacking = true;
                    anim.SetTrigger("isAttack");
                    Invoke("Attack_ing", 0.8f);

                }
            }
        }

        if (Attackmotion && Weaponnum == 0)
        {
            AttackBoxCollider.SetActive(true);
            if (AttackBox == 1)
            {
                if (count1 < 10)
                {
                    count1++;
                    AttackBoxCollider.transform.Translate(0.16f, 0, 0);
                }
            }
            if (AttackBox == 2)
            {
                if (!AttackBoxs)
                {
                    AttackBoxs = true;
                    if (leftFlag)
                    {
                        AttackBoxCollider.transform.rotation = Quaternion.Euler(0, 0, 90f);
                    }
                    if (!leftFlag)
                    {
                        AttackBoxCollider.transform.rotation = Quaternion.Euler(0, 0, -90f);
                    }
                }
                if (count2 < 10)
                {
                    count2++;
                    AttackBoxCollider.transform.Translate(0.17f, 0, 0);
                }
            }
            if (AttackBox == 4)
            {
                if (count3 < 10)
                {
                    count3++;
                    AttackBoxCollider.transform.Translate(-0.17f, 0, 0);
                }
            }
            if (AttackBox == 5)
            {
                if (!AttackBoxss)
                {
                    AttackBoxss = true;
                    AttackBoxCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                if (count4 < 10)
                {
                    count4++;
                    AttackBoxCollider.transform.Translate(-0.16f, 0, 0);
                }
            }
        }
        if (!GAtkcheck)
        {
            if (Weapons.GuardingAtk == true)
            {
                GAtkcheck = true;
                Invoke("GAtkCheck", 1f);
                if (PlayerHp > 0)
                {
                    rbody.velocity = Vector2.zero;
                    Invoke("isHitReset", 0.5f);
                    hitBoxCollider.SetActive(false);
                    anim.SetTrigger("isHurt");
                    rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                    rbody.AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
                    AttackBox2();
                }
                else if (PlayerHp <= 0)
                {
                    if (death == 0)
                    {
                        death++;
                        rbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                        anim.SetTrigger("isDeath");
                        Invoke("Death", 1.2f);
                    }
                }
            }
        }
    }

    private void Attack_ing()
    {
        Attacking = false;
    }

    private void AnimOn()
    {
        anim.SetBool("isRun", true);
    }

    private void AnimOff()
    {
        anim.SetBool("isRun", false);
    }

    void FixedUpdate()
    {
        if (!Attacking && (death == 0) && !Dashing)
        {
            rbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            rbody.velocity = new Vector2(vx, rbody.velocity.y);

            Vector3 thisScale = transform.localScale;

            if (leftFlag)
            {
                if (leftFlip == 0)
                {
                    leftFlip = 1;
                    transform.Translate(-0.8f, 0, 0);
                }
                thisScale.x = -Mathf.Abs(thisScale.x);
            }
            else
            {
                if (leftFlip == 1)
                {
                    leftFlip = 0;
                    transform.Translate(0.8f, 0, 0);
                }
                thisScale.x = Mathf.Abs(thisScale.x);
            }
            transform.localScale = thisScale;

            if (jumpFlag)
            {
                jumpFlag = false;

                rbody.velocity = new Vector2(rbody.velocity.x, 0);

                rbody.AddForce(new Vector2(0, jumppower), ForceMode2D.Impulse);
            }
        }
        else if (Attacking)
        {
            rbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("block"))
        {
            groundFlag = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        groundFlag = false;
    }

    private void AttackBox1()
    {
        AttackBox++;
        Attackmotion = true;

        if (AttackBox == 2)
        {
            AttackBoxCollider.transform.rotation = Quaternion.Euler(0, 0, -90f);
        }
        if (AttackBox == 5)
        {
            AttackBoxCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void AttackBox2()
    {
        AttackBox = 0;
        AttackBoxCollider.transform.position = AttackBoxTransform.transform.position;
        AttackBoxCollider.SetActive(false);
        Attackmotion = false;

        AttackBoxs = false;
        AttackBoxss = false;

        count1 = 0;
        count2 = 0;
        count3 = 0;
        count4 = 0;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("EnemyAttackBox") || collision.transform.CompareTag("Projectile"))
        {
            if (collision.transform.CompareTag("Projectile"))
            {
                Destroy(collision.gameObject, 0.02f);
            }

            if (!IsHit)
            {
                PlayerHp--;
                IsHit = true;
            }

            if (PlayerHp > 0)
            {
                rbody.velocity = Vector2.zero;
                Invoke("isHitReset", 0.5f);
                hitBoxCollider.SetActive(false);
                anim.SetTrigger("isHurt");
                rbody.constraints = RigidbodyConstraints2D.FreezeRotation;

                if (transform.position.x > collision.transform.position.x)
                {
                    transform.Translate(0.5f, 0, 0);
                }
                else
                {
                    transform.Translate(-0.5f, 0, 0);
                }
                AttackBox2();
            }
            else if (PlayerHp <= 0)
            {
                if (death == 0)
                {
                    death++;
                    rbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                    anim.SetTrigger("isDeath");
                    Invoke("Death", 1.2f);
                }
            }
        }
        else if (collision.transform.CompareTag("MeleeAttackBox"))
        {
            if (!IsHit)
            {
                PlayerHp--;
                IsHit = true;
            }

            if (PlayerHp > 0)
            {
                rbody.velocity = Vector2.zero;
                Invoke("isHitReset", 0.5f);
                hitBoxCollider.SetActive(false);
                anim.SetTrigger("isHurt");
                rbody.constraints = RigidbodyConstraints2D.FreezeRotation;

                if (transform.position.x > collision.transform.position.x)
                {
                    DamLeftFlag = false;
                    StartCoroutine(DamageOn(0.05f));
                }
                else
                {
                    DamLeftFlag = true;
                    StartCoroutine(DamageOn(0.05f));
                }
                AttackBox2();
            }
            else if (PlayerHp <= 0)
            {
                if (death == 0)
                {
                    death++;
                    rbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                    anim.SetTrigger("isDeath");
                    Invoke("Death", 1.2f);
                }
            }
        }
    }

    IEnumerator DamageOn(float waitTime)
    {
        if (DamLeftFlag)
        {
            transform.Translate(-0.5f, 0, 0);
            yield return new WaitForSeconds(waitTime);
            transform.Translate(-0.5f, 0, 0);
            yield return new WaitForSeconds(waitTime);
            transform.Translate(-0.5f, 0, 0);
            yield return new WaitForSeconds(waitTime);
            transform.Translate(-0.5f, 0, 0);
        }
        else if (!DamLeftFlag)
        {
            transform.Translate(0.5f, 0, 0);
            yield return new WaitForSeconds(waitTime);
            transform.Translate(0.5f, 0, 0);
            yield return new WaitForSeconds(waitTime);
            transform.Translate(0.5f, 0, 0);
            yield return new WaitForSeconds(waitTime);
            transform.Translate(0.5f, 0, 0);
        }
    }

    void isHitReset()
    {
        IsHit = false;
    }

    void GAtkCheck()
    {
        GAtkcheck = false;
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
