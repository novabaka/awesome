using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualButton : MonoBehaviour
{
    [SerializeField] bool on = true; 
    [SerializeField] Image img;
    [SerializeField] RawImage rawImage;
    [SerializeField] GameObject obj1;
    [SerializeField] GameObject obj2;
    [SerializeField] GameObject obj3;
    [SerializeField] GameObject obj5;
    public void onClickButton() { 
        if (on)
        {
            img.color = Color.white;
            rawImage.color = new Color(0, 0.6627451f, 1);
            obj1.active = false;
            obj2.active = false;
            obj3.active = false;
            obj5.active = true;
        }
        else
        {
            img.color = Color.clear;
            rawImage.color = Color.clear;
            obj1.active = true;
            obj2.active = true;
            obj3.active = true;
            obj5.active = false;
        }
        
    }
}
