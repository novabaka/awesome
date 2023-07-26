using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftButton : MonoBehaviour
{
    private Player_Move pm = new Player_Move();

    public void OnClickButton()
    {
        pm.leftFlag = true;
        pm.vx = -pm.speed;
        pm.AnimOn();
    }
}
