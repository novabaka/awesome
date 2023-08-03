using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class BSwitch : MonoBehaviour
{
    public int Number;
    int Number2;
    public GameObject[] ButtonSwitch;

    SpriteRenderer sr;
    
    public int SwitchType;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Number2 = Number;
        if (SwitchType == 1)
        {
            while(Number2 >= 0)
            {
                ButtonSwitch[Number2].SetActive(false);
                Number2--;
            }
        }
        else if (SwitchType == 2)
        {
            ButtonSwitch[Number].SetActive(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("SwitchCheck"))
        {
            if (SwitchType == 1)
            {
                sr.material.color = Color.clear;
                ButtonSwitch[Number].SetActive(true);
            }
            else if (SwitchType == 2)
            {
                sr.material.color = Color.clear;
                ButtonSwitch[Number].SetActive(false);
            }
        }
    }
}
