using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AC : MonoBehaviour
{
    public GameObject LeftObject;
    public GameObject RightObject;

    void start()
    {
        LeftObject.SetActive(false);
        RightObject.SetActive(false);
    }
    void FixedUpdate()
    {
        if (Chase.isLeft == true)
        {
            RightObject.SetActive(false);
            LeftObject.SetActive(true);
        }
        if (Chase.isLeft == false)
        {
            LeftObject.SetActive(false);
            RightObject.SetActive(true);
        }

        if (Chase.Attackmotion == false)
        {
            LeftObject.SetActive(false);
            RightObject.SetActive(false);
        }
    }
}
