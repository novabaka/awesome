using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 6f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("PlayerHitBox") || collision.transform.CompareTag("block"))
        {
            Destroy(gameObject, 0.02f);
        }
    }
}
