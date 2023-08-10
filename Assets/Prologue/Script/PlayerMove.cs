using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public SpriteRenderer bubble;
    public SpriteRenderer NoHeadKing;
    public SpriteRenderer smile;

    public string idle;
    public string run;

    public string Anim;

    public bool playerMove = false;

    private void Start()
    {
        Anim = idle;
    }
    public void OnBubble()
    {
        bubble.color = Color.white;
    }
    public void OffBubble()
    {
        bubble.color = Color.clear;
    }
    public void OnHeadKing ()
    {
        NoHeadKing.color = Color.white;
    }
    public void OffHeadKing()
    {
        NoHeadKing.color = Color.clear;
    }
    public void OnSmile()
    {
        smile.color = Color.white;
    }
    public void OffSmile()
    {
        smile.color = Color.clear;
    }
    public void Flip()
    {
        this.GetComponent<SpriteRenderer>().flipX = true;
    }
    public void nonFlip()
    {
        this.GetComponent<SpriteRenderer>().flipX = false;
        playerMove = true;
    }
    public void offPlayer()
    {
        this.GetComponent<SpriteRenderer>().color = Color.clear;
    }
    void Update()
    {
        if (playerMove)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * 3));
            Anim = run;
        }
    }

    private void FixedUpdate()
    {
        this.GetComponent<Animator>().Play(Anim);
    }
}
