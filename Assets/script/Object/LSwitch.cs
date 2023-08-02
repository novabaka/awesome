using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LSwitch : MonoBehaviour
{
    bool OnOff = false;

    SpriteRenderer sr;

    public GameObject LeverSwitch;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        LeverSwitch.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("SwitchCheck"))
        {
            if (OnOff)
            {
                LeverSwitch.SetActive(false);
                OnOff = false;
                sr.material.color = Color.white;
            }
            else if (!OnOff)
            {
                LeverSwitch.SetActive(true);
                OnOff = true;
                sr.material.color = Color.gray;
            }
        }
    }
}
