using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool EndCheck_ing = false;

    public bool GuardAttackD = false;
    public bool nowGuarding = false;

    public bool DeathOn = false;
    public bool DamOn = false;
    public bool DeathUse;

    public bool Hit_ing = false;
    public bool Attack_ing = false;

    public int enemyHp = 1;
    public float moveSpeed = 1f;
    public float jumpPower = 4.5f;
    public float atkCoolTime = 1;
    public float atkCoolTimeCalc = 1;

    public bool isHit = false;
    public bool isGround = true;
    public bool canAtk = true;
    public bool EnemyDirRight;

    protected Rigidbody2D rb;
    protected BoxCollider2D boxCollider;
    public GameObject hitBoxCollider;
    public Animator Anim;
    public LayerMask layerMask;
    public LayerMask layerMask2;
    public LayerMask layerMask3;

    public float ABTime;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        Anim = GetComponent<Animator>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        StartCoroutine(CalcCoolTime());
        StartCoroutine(ResetCollider());
        StartCoroutine(CalcABTime());
    }

    IEnumerator ResetCollider()
    {
        while (true)
        {
            yield return null;
            if (!hitBoxCollider.activeInHierarchy)
            {
                yield return new WaitForSeconds(0.4f);
                hitBoxCollider.SetActive(true);
                isHit = false;
                DamOn = false;
            }
        }
    }

    IEnumerator CalcCoolTime()
    {
        while (true)
        {
            yield return null;
            if (!canAtk)
            {
                atkCoolTimeCalc -= Time.deltaTime;
                if (atkCoolTimeCalc <= 0)
                {
                    atkCoolTimeCalc = atkCoolTime;
                    canAtk = true;
                }
            }
        }
    }

    IEnumerator CalcABTime()
    {
        while (true)
        {
            yield return null;
            if (ABTime <= 3 && ABTime > 0)
            {
                ABTime -= Time.deltaTime;
                if (ABTime <= 0)
                {
                    ABTime = 0;
                }
            }
        }
    }

    public bool IsPlayingAnim(string AnimName)
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).IsName(AnimName))
        {
            return true;
        }
        return false;
    }
    public void MyAnimSetTrigger(string AnimName)
    {
        if (!IsPlayingAnim(AnimName))
        {
            Anim.SetTrigger(AnimName);
        }
    }

    protected void EnemyFlip()
    {
        EnemyDirRight = !EnemyDirRight;

        Vector3 thisScale = transform.localScale;
        if (EnemyDirRight)
        {
            thisScale.x = -Mathf.Abs(thisScale.x);
        }
        else
        {
            thisScale.x = Mathf.Abs(thisScale.x);
        }
        transform.localScale = thisScale;
        rb.velocity = Vector2.zero;
    }

    protected bool IsPlayerDir()
    {
        if (transform.position.x < PlayerData.Instance.Player.transform.position.x ? EnemyDirRight : !EnemyDirRight)
        {
            return true;
        }
        return false;
    }

    protected void GroundCheck()
    {
        if (Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, Vector2.down, 0.05f, layerMask))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    IEnumerator WaitRoutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }

    public void TakeDamage()
    {
        enemyHp--;
        isHit = true;
        hitBoxCollider.SetActive(false);
        Attack_ing = false;

        if (enemyHp <= 0)
        {
            if (DeathUse)
            {
                MyAnimSetTrigger("Death");
                DeathOn = true;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                StartCoroutine(WaitRoutine(0.4f));
            }
            else if (!DeathUse)
            {
                DeathOn = true;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                StartCoroutine(WaitRoutine(0.1f));
            }
        }
        else
        {
            Hit_ing = true;
            MyAnimSetTrigger("Hit");
            rb.velocity = Vector2.zero;
            if (transform.position.x > PlayerData.Instance.Player.transform.position.x)
            {
                rb.velocity = new Vector2(2f, 0);
            }
            else
            {
                rb.velocity = new Vector2(-2f, 0);
            }
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!nowGuarding)
        {
            if ((collision.transform.CompareTag("PlayerWeapon")) || (collision.transform.CompareTag("PlayerWeapon2")))
            {
                if (!DamOn)
                {
                    TakeDamage();
                    DamOn = true;
                }
            }
        }
        else if (nowGuarding)
        {
            if (collision.transform.CompareTag("PlayerWeapon2"))
            {
                if (!DamOn)
                {
                    TakeDamage();
                    DamOn = true;
                }
            }
            else if (collision.transform.CompareTag("PlayerWeapon"))
            {
                GuardAttackD = true;
            }
        }
    }
}
