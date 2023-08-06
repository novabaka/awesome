using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KingMove : MonoBehaviour
{
    public SpriteRenderer bubble;
    public SpriteRenderer angry;

    public string idleAnim;
    public string walkingAnim;
    public string goAnim;

    public string Anim;

    public bool kingMove = false;

    private void Start()
    {
        Anim = idleAnim;
    }

    
    public void onKing()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
        kingMove = true;
    }
    public void offKing()
    {
        this.GetComponent<SpriteRenderer>().color = Color.clear;
    }
    public void onBubble()
    {
        bubble.color = Color.white;
    }
    public void offBubble()
    {
        bubble.color = Color.clear;
    }
    public void onAngry()
    {
        angry.color = Color.white;
    }
    public void offAngry()
    {
        angry.color = Color.clear;
        Anim = goAnim;
    }

    void Update()
    {
        if (kingMove)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * 3));
            Anim = walkingAnim;
        }
    }

    private void FixedUpdate()
    {
        this.GetComponent<Animator>().Play(Anim);
    }
}
