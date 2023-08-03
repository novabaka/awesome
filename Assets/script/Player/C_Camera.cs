using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Camera : MonoBehaviour
{
    public bool Check = false;
    public static int number = 0;
    public GameObject[] Player;

    public float offsetY = 0f;
    public float offsetZ = -10f;
    public float smooth = 5f;

    Vector3 target;
    private void LateUpdate()
    {
        if (number == 0)
        {
            target = new Vector3(Player[number].transform.position.x, Player[number].transform.position.y + offsetY, Player[number].transform.position.z + offsetZ);
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * smooth);
        }
        else 
        {
            transform.position = new Vector3(Player[number].transform.position.x, Player[number].transform.position.y + offsetY, Player[number].transform.position.z + offsetZ);
        }
    }

    void FixedUpdate()
    {
        if (EndCheck.EndChecking)
        {
            if(!Check)
            {
                Check = true;
                StartCoroutine(EndCamera());
            }
        }
    }

    IEnumerator EndCamera()
    {
        number = 1;
        yield return new WaitForSeconds(0.1f);
    }
}
