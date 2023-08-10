using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameManual : MonoBehaviour
{
    [SerializeField] bool on = true; 
    [SerializeField] GameObject img;
    [SerializeField] GameObject rawImage;
    [SerializeField] GameObject obj1;
    public void onClickButton() { 
        if (on)
        { 
            img.SetActive(true);
            rawImage.SetActive(true);
            obj1.SetActive(true);
        }
        else
        {
            img.SetActive(false);
            rawImage.SetActive(false);
            obj1.SetActive(false);
        }
        
    }
}
