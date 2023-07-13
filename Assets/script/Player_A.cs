using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_A : MonoBehaviour
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
        if (Player.Attackmotion == true)
        {
            if (Player.leftFlag == true)
            {
                RightObject.SetActive(false);
                LeftObject.SetActive(true);
            }
            if (Player.leftFlag == false)
            {
                LeftObject.SetActive(false);
                RightObject.SetActive(true);
            }
        }
        else if (Player.Attackmotion == false)
        {
            LeftObject.SetActive(false);
            RightObject.SetActive(false);
        }
    }
}
