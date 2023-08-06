using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowMove : MonoBehaviour
{
    public bool shot = false;
    // Update is called once per frame
    void Update()
    {
        if (shot)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * 7));
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }
        
    }
}
