using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BowmanMove : MonoBehaviour
{
    public arrowMove arrow = new arrowMove();

    public string idle;
    public string run;
    public string shot;

    public string Anim;

    public bool manMove = false;

    public void onMove()
    {
        manMove = true;
        Anim = run;
    }
    public void offMove()
    {
        manMove = false;
        Anim = idle;
    }
    public void onShot()
    {
        Anim = shot;
        arrow.shot = true;
    }
    public void offShot()
    {
        Anim = idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (manMove)
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
