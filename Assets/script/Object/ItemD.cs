using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemD : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
