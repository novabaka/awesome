using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AC2 : MonoBehaviour
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
        if (Chase2.isLeft == true)
        {
            RightObject.SetActive(false);
            LeftObject.SetActive(true);
        }
        if (Chase2.isLeft == false)
        {
            LeftObject.SetActive(false);
            RightObject.SetActive(true);
        }

        if (Chase2.Attackmotion == false)
        {
            LeftObject.SetActive(false);
            RightObject.SetActive(false);
        }

        if (Chase2.death > 0)
        {
            LeftObject.SetActive(false);
            RightObject.SetActive(false);
        }
    }
}
