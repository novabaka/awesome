using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public string idleAnim;
    public string walkingAnim;

    public string Anim;

    public bool enemyMove = false;

    private void Start()
    {
        Anim = idleAnim;
    }

    void Update()
    {
        if (enemyMove)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * 3));
            Anim = walkingAnim;
        }
    }
    public void onEnemy()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
        enemyMove = true;
    }
    private void FixedUpdate()
    {
        this.GetComponent<Animator>().Play(Anim);
    }
}

