using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase2 : MonoBehaviour
{
    public Player PlayerScript;

    public string targetObjectName; // ��� �̸� string ����
    public float speed = 1; // �� �ӵ�

    public static int death = 0;

    float vx; // x ����

    public float AttackCoolTime;
    float AttackCoolDown;
    bool Attacking = false;
    public static bool Attackmotion = false;
    bool AttackCool = false;

    public static bool isLeft = true;

    bool IsGuard = false;

    int RandomAttack;

    bool range = false; // ���� ���Դ���

    GameObject targetObject; // Ÿ�� ����
    Rigidbody2D rbody; // ������ٵ� �������� �ڵ�
    Animator anim; // �ִϸ����� �������� �ڵ�

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        targetObject = GameObject.Find(targetObjectName); // Ÿ���� ã�� �ڵ�

        rbody = GetComponent<Rigidbody2D>();
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        AttackCoolDown = AttackCoolTime * 50;

        death = 0;
    }

    void Update()
    {
        if (range && (death == 0)) // �Ÿ��ȿ� ������ �ִϸ��̼� Ȱ��ȭ
        {
            anim.SetBool("isRun", true);
        }
        if (!range) // ������ �ִϸ��̼� ��Ȱ��ȭ
        {
            anim.SetBool("isRun", false);
        }

        RandomAttack = Random.Range(1, 4);
    }

    void FixedUpdate()
    {

        if (range && !Attacking && !IsGuard && (death == 0)) // ���� ���� �ȿ� ������ �i�ư��� ����� �ڵ� ( �������̳� ������� �ƴҶ�, �������°� �ƴҶ�)
        {
            rbody.constraints = RigidbodyConstraints2D.FreezeRotation; // z���� ȸ�� ����, ��ġ ���� ����

            Vector3 dir = (targetObject.transform.position - this.transform.position).normalized;

            vx = dir.x * speed;
            rbody.velocity = new Vector2(vx, rbody.velocity.y);

            this.GetComponent<SpriteRenderer>().flipX = (vx > 0); // �¿� �ø�

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
        else // ���� �ȿ� �÷��̾ ���ų� �������̰ų� ������϶�
        {
            rbody.constraints = RigidbodyConstraints2D.FreezePosition; // ��ġ ����
            this.transform.rotation = Quaternion.Euler(0, 0, 0); // ȸ�� ����
        }

        if (Enemy_AR2.AttackRange) // �÷��̾ ���ݹ����� ��������
        {
            if (!Attacking && !IsGuard && !AttackCool && (death == 0) && ((RandomAttack == 1) || (RandomAttack == 3))) // �������� �ƴϸ� ��Ÿ���� �ƴϰ� �������°� �ƴϰ� ���� ���� 1 or 3�� �޾��� ��
            {
                anim.SetTrigger("isAttack"); // ���� ��� Ȱ��ȭ
                Attacking = true; // ������ ���� Ȱ��ȭ
                Invoke("Attack_ing1", 0.3f); // 0.15�� �� ���� �Լ� ����
            }
            else if (!Attacking && !IsGuard && !AttackCool && (death == 0) && (RandomAttack == 2)) // �������� �ƴϸ� ��Ÿ���� �ƴϰ� ���� ���� 1�� �޾��� ��
            {
                anim.SetTrigger("isGuard"); // ��� ��� Ȱ��ȭ
                IsGuard = true; // ����� ���� Ȱ��ȭ
                Invoke("Guard_ing", 1.6f); // 1.6�� �� ���� �Լ� ����
            }
        }

        if (AttackCool) // ���� ��Ÿ�� ����
        {
            AttackCoolDown--; // ���� ��ٿ� �ʴ� 50�� ���̱�(FixedUpdate)
        }

        if (AttackCoolDown == 0) // ���� ��ٿ��� 0�� �Ǹ�
        {
            AttackCoolDown = AttackCoolTime * 50; // �������� ��Ÿ�ӿ� 50�� ���� ���� ��ٿ� ������ �Է�
            AttackCool = false; // ���� ��Ÿ�� ��Ȱ��ȭ
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
        AttackCool = true; // ���� ��Ÿ�� ���� Ȱ��ȭ
    }

    private void Attack_ing3()
    {
        Attackmotion = false;
    }

    private void Guard_ing()
    {
        IsGuard = false; // ����� ���� ��Ȱ��ȭ
        AttackCool = true; // ���� ��Ÿ�� ���� Ȱ��ȭ
        AttackCoolDown = AttackCoolDown + 50; // ���� 1�� �߰� ��
    }

    void OnTriggerStay2D(UnityEngine.Collider2D collision) // Ʈ���� �����ȿ� �ݶ��̴��� ���� ���
    {
        if (collision.gameObject.tag == ("Player")) // ���� �ݶ��̴��� Player�±׸� ������ �ִٸ� range Ȱ��ȭ
        {
            range = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) // Ʈ���� ���� ������ �ݶ��̴��� ���� ���
    {
        if (collision.gameObject.tag == ("Player")) // ���� �ݶ��̴��� Player�±׸� ������ ���� �ʴٸ� range ��Ȱ��ȭ
        {
            range = false;
        }
    }
}
