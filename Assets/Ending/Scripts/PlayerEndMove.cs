using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerEndMove : MonoBehaviour
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
    public void onRun()
    {
        playerMove = true;
        Anim = run;
    }
    public void offRun()
    {
        playerMove = false;
        Anim = idle;
    }
    public void standUp()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.position = new Vector3 (948.12f, 105.87f, 0);
    }
    void Update()
    {
        if (playerMove)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * 4));
            Anim = run;
        }
    }

    private void FixedUpdate()
    {
        this.GetComponent<Animator>().Play(Anim);
    }
}
