using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Camera : MonoBehaviour
{
    public GameObject Player;

    public float offsetY = 0f;
    public float offsetZ = -10f;
    public float smooth = 5f;

    Vector3 target;
    private void LateUpdate()
    {
        target = new Vector3(Player.transform.position.x, offsetY, Player.transform.position.z + offsetZ);
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * smooth);
    }
}
