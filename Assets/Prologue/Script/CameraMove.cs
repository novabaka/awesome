using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class CameraMove : MonoBehaviour
{
    public PrologueControler controler;
     public SpriteRenderer fade;
    public Image img;

    void Awake()
    {
        transform.position = new Vector3(-21, 0, -10);
    }
    void Update()
    {
        if (controler.cameraMove)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * 4));
        }
        if (transform.position.x >= 3)
        {
            controler.cameraMove = false;
        }

    }
    public void offView()
    {
        fade.color = Color.black;
    }
    public void onImg()
    {
        img.color = Color.white;
    }
}
