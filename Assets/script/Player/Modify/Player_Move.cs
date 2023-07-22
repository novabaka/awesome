using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float speed;
    [SerializeField] float jumppower;

    int death = 0;

    public float vx = 0;
    public bool leftFlag = false;
    public bool pushFlag = false;
    public bool jumpFlag = false;
    bool groundFlag = false;

    public bool Attacking = false;
    public bool Attackmotion = false;

    public Rigidbody2D rbody; // 리지드바디 가져오는 코드
    public Animator anim; // 애니메이터 가져오는 코드

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        death = 0;
    }

    
    void Update()
    {
        if (Input.GetKey("right"))
        {
            MoveRight();
        }
        if (Input.GetKey("left"))
        {
            MoveLeft();
        }
        if (Input.GetKeyUp("right"))
        {
            vx = 0;
            Invoke("AnimOff", 0);
        }
        if (Input.GetKeyUp("left"))
        {
            vx = 0;
            Invoke("AnimOff", 0);
        }
        if (Input.GetKey("space"))
        {
            Jump();
        }

        if (Input.GetKeyDown("a"))
        {
            Attack();
        }
    }

    //버튼 함수
    public void PointerDownLeft()
    {
        MoveLeft();
    }
    public void PointerUpLeft()
    {
        Invoke("AnimOff", 0);
        vx = 0;
    }
    public void PointerDownRight()
    {
        MoveRight();
    }
    public void PointerUpRight()
    {
        Invoke("AnimOff", 0);
        vx = 0;
    }
    public void PointerDownJump()
    {
        Jump();
    }
    public void PointerDownAttack()
    {
        Attack();
    }

    //플레이어 움직임 함수
    private void MoveRight()
    {
        vx = speed;
        leftFlag = false;
        Invoke("AnimOn", 0);
    }
    private void MoveLeft()
    {
        vx = -speed;
        leftFlag = true;
        Invoke("AnimOn", 0);
    }
    private void Jump()
    {
        if (groundFlag)
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
    }
    private void Attack()
    {
        if (!Attacking && groundFlag && !Attackmotion)
        {
            Attacking = true;
            anim.SetTrigger("isAttack");
            Invoke("Attack_ing", 0.73f);
            Invoke("Attack_motion1", 0.325f);
        }
    }


    public void Attack_motion1()
    {
        Attackmotion = true;
        Invoke("Attack_motion2", 0.375f);
    }

    private void Attack_motion2()
    {
        Attackmotion = false;
    }

    public void Attack_ing()
    {
        Attacking = false;
    }

    public void AnimOn()
    {
        anim.SetBool("isRun", true);
    }

    public void AnimOff()
    {
        anim.SetBool("isRun", false);
    }

    void FixedUpdate()
    {
        if (!Attacking && (death == 0))
        {
            rbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            rbody.velocity = new Vector2(vx, rbody.velocity.y);
            this.GetComponent<SpriteRenderer>().flipX = leftFlag;

            if (jumpFlag)
            {
                jumpFlag = false;
                rbody.AddForce(new Vector2(0, jumppower), ForceMode2D.Impulse);
            }
        }
        else if (Attacking || (death >= 0))
        {
            rbody.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }
    private void OnTriggerStay2D(UnityEngine.Collider2D collision)
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

    public void Player_Dead()
    {
        if (death == 0)
        {
            death++;
            anim.SetTrigger("isDeath");
            this.GetComponent<BoxCollider2D>().enabled = false;
            Invoke("Player_Death", 1.35f);
        }
    }
    public void Player_Death()
    {
        Destroy(gameObject);
    }
}
