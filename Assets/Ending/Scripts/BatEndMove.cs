using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEndMove : MonoBehaviour
{

    public bool upMove = false;
    public bool rightMove = false;
    public bool downMove = false;
    // Start is called before the first frame update
    public void onUp()
    {
        upMove = true;
    }
    public void offUp()
    {
        upMove = false;
    }
    public void onRight()
    {
        rightMove = true;
    }
    public void offRight()
    {
        rightMove = false;
    }
    public void onDown()
    {
        downMove = true;
    }

    void Update()
    {
        if (rightMove)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * 4));
        }
        if (upMove)
        {
            transform.Translate(Vector3.up * (Time.deltaTime * 6));
        }
        if (downMove)
        {
            transform.Translate(Vector3.down * (Time.deltaTime * 8));
        }
    }

}
