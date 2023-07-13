using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public string targetObjectName; // 대상 이름 string 변수
    public float speed = 1; // 적 속도

    float vx; // x 지정

    public float AttackCoolTime;
    float AttackCoolDown;
    bool Attacking = false;
    public static bool Attackmotion = false;
    bool AttackCool = false;

    public static bool isLeft = true;

    bool range = false; // 범위 들어왔는지

    GameObject targetObject; // 타겟 지정
    Rigidbody2D rbody; // 리지드바디 가져오는 코드
    Animator anim; // 애니메이터 가져오는 코드

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start() 
    {
        targetObject = GameObject.Find(targetObjectName); // 타겟을 찾는 코드

        rbody = GetComponent<Rigidbody2D>();
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        AttackCoolDown = AttackCoolTime * 50;
    }
    
    void Update()
    {
        if (range)
        {
            anim.SetBool("isRun", true);
        }
        if (!range)
        {
            anim.SetBool("isRun", false);
        }
    }

    void FixedUpdate()
    {

        if (range && !Attacking) // 만약 범위 안에 들어오면 쫒아가게 만드는 코드 ( 
        {
            rbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            Vector3 dir = (targetObject.transform.position - this.transform.position).normalized; 

            vx = dir.x * speed;
            rbody.velocity = new Vector2(vx, rbody.velocity.y);

            this.GetComponent<SpriteRenderer>().flipX = (vx > 0); // 좌우 플립

            if (vx > 0)
            {
                if (isLeft)
                {
                    isLeft = false;
                }
                else if (!isLeft)
                {
                    isLeft = true;
                }
            }
        }
        else
        {
            rbody.constraints = RigidbodyConstraints2D.FreezePosition;
        }

        if (Enemy_AR.AttackRange)
        {
            if (!Attacking && !AttackCool)
            {
                anim.SetTrigger("isAttack");
                Attacking = true;
                AttackCool = true;
                Invoke("Attack_ing1", 0.15f);
            }
        }

        if (AttackCool)
        {
            AttackCoolDown--;
        }

        if (AttackCoolDown == 0)
        {
            AttackCoolDown = AttackCoolTime * 50;
            AttackCool = false;
        }

        if (Enemy_HB.Hit == true)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void Attack_ing1()
    {
        Attackmotion = true;
        Invoke("Attack_ing2", 0.45f);
        Invoke("Attack_ing3", 0.3f);
    }

    private void Attack_ing2()
    {
        Attacking = false;
    }

    private void Attack_ing3()
    {
        Attackmotion = false;
    }


    void OnTriggerStay2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player")) // 플레이어가 범위 안에 들어오면 range 활성화
        {
            range = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) // 나가면 range 비활성화
    {
        if (collision.gameObject.tag == ("Player"))
        {
            range = false;
        }
    }
}
