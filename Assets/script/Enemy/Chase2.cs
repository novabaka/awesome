using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase2 : MonoBehaviour
{
    public Player PlayerScript;

    public string targetObjectName; // 대상 이름 string 변수
    public float speed = 1; // 적 속도

    public static int death = 0;

    float vx; // x 지정

    public float AttackCoolTime;
    float AttackCoolDown;
    bool Attacking = false;
    public static bool Attackmotion = false;
    bool AttackCool = false;

    public static bool isLeft = true;

    bool IsGuard = false;

    int RandomAttack;

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

        death = 0;
    }

    void Update()
    {
        if (range && (death == 0)) // 거리안에 들어오면 애니메이션 활성화
        {
            anim.SetBool("isRun", true);
        }
        if (!range) // 나가면 애니메이션 비활성화
        {
            anim.SetBool("isRun", false);
        }

        RandomAttack = Random.Range(1, 4);
    }

    void FixedUpdate()
    {

        if (range && !Attacking && !IsGuard && (death == 0)) // 만약 범위 안에 들어오면 쫒아가게 만드는 코드 ( 공격중이나 방어중이 아닐때, 죽은상태가 아닐때)
        {
            rbody.constraints = RigidbodyConstraints2D.FreezeRotation; // z방향 회전 고정, 위치 고정 해제

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
        else // 범위 안에 플레이어가 없거나 공격중이거나 방어중일때
        {
            rbody.constraints = RigidbodyConstraints2D.FreezePosition; // 위치 고정
            this.transform.rotation = Quaternion.Euler(0, 0, 0); // 회전 고정
        }

        if (Enemy_AR2.AttackRange) // 플레이어가 공격범위에 들어왔을때
        {
            if (!Attacking && !IsGuard && !AttackCool && (death == 0) && ((RandomAttack == 1) || (RandomAttack == 3))) // 공격중이 아니며 쿨타임이 아니고 죽은상태가 아니고 랜덤 변수 1 or 3을 받았을 때
            {
                anim.SetTrigger("isAttack"); // 공격 모션 활성화
                Attacking = true; // 공격중 변수 활성화
                Invoke("Attack_ing1", 0.3f); // 0.15초 후 지정 함수 실행
            }
            else if (!Attacking && !IsGuard && !AttackCool && (death == 0) && (RandomAttack == 2)) // 공격중이 아니며 쿨타임이 아니고 랜덤 변수 1을 받았을 때
            {
                anim.SetTrigger("isGuard"); // 방어 모션 활성화
                IsGuard = true; // 방어중 변수 활성화
                Invoke("Guard_ing", 1.6f); // 1.6초 후 지정 함수 실행
            }
        }

        if (AttackCool) // 공격 쿨타임 변수
        {
            AttackCoolDown--; // 공격 쿨다운 초당 50번 줄이기(FixedUpdate)
        }

        if (AttackCoolDown == 0) // 공격 쿨다운이 0이 되면
        {
            AttackCoolDown = AttackCoolTime * 50; // 설정해준 쿨타임에 50을 곱한 값을 쿨다운 변수에 입력
            AttackCool = false; // 공격 쿨타임 비활성화
        }

        if (Enemy_HB2.Hit == true)
        {
            if (!IsGuard)
            {
                if (death == 0)
                {
                    death++;
                    anim.SetTrigger("isDeath");
                    this.GetComponent<BoxCollider2D>().enabled = false;
                    Invoke("Enemy_Death", 3.5f);
                }
            }
            else if (IsGuard)
            {
                PlayerScript.Player_Dead();
            }
        }
    }

    private void Enemy_Death()
    {
        Destroy(gameObject);
    }

    private void Attack_ing1() 
    {
        Attackmotion = true;
        Invoke("Attack_ing2", 0.3f);
        Invoke("Attack_ing3", 0.15f);
    }

    private void Attack_ing2()
    {
        Attacking = false;
        AttackCool = true; // 공격 쿨타임 변수 활성화
    }

    private void Attack_ing3()
    {
        Attackmotion = false;
    }

    private void Guard_ing()
    {
        IsGuard = false; // 방어중 변수 비활성화
        AttackCool = true; // 공격 쿨타임 변수 활성화
        AttackCoolDown = AttackCoolDown + 50; // 방어는 1초 추가 쿨
    }

    void OnTriggerStay2D(UnityEngine.Collider2D collision) // 트리거 범위안에 콜라이더가 들어온 경우
    {
        if (collision.gameObject.tag == ("Player")) // 닿은 콜라이더가 Player태그를 가지고 있다면 range 활성화
        {
            range = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) // 트리거 범위 밖으로 콜라이더가 나간 경우
    {
        if (collision.gameObject.tag == ("Player")) // 닿은 콜라이더가 Player태그를 가지고 있지 않다면 range 비활성화
        {
            range = false;
        }
    }
}
