using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class CameraEndMove : MonoBehaviour
{
     public SpriteRenderer fade;
    public void offView()
    {
        fade.color = Color.black;
    }
}
