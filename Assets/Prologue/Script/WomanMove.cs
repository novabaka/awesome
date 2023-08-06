using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanMove : MonoBehaviour
{
    public SpriteRenderer bubble;
    public SpriteRenderer NoHeadKing;
    public SpriteRenderer smile;
    public void OnBubble()
    {
        bubble.color = Color.white;
    }
    public void OffBubble()
    {
        bubble.color = Color.clear;
    }
    public void OnHeadKing()
    {
        NoHeadKing.color = Color.white;
    }
    public void OffHeadKing()
    {
        NoHeadKing.color = Color.clear;
    }
    public void OnSmile()
    {
        smile.color = Color.white;
    }
    public void OffSmile()
    {
        smile.color = Color.clear;
    }
    public void offWoman()
    {
        this.GetComponent<SpriteRenderer>().color = Color.clear;
    }
}
