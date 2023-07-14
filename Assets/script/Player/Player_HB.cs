using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_HB : MonoBehaviour
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
        if (Player.leftFlag == true)
        {
            RightObject.SetActive(true);
            LeftObject.SetActive(false);
        }
        if (Player.leftFlag == false)
        {
            LeftObject.SetActive(true);
            RightObject.SetActive(false);
        }
    }
}
