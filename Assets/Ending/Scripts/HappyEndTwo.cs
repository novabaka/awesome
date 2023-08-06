using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HappyEndTwo : MonoBehaviour
{
    public PlayerEndMove player = new PlayerEndMove();
    public KingEndMove king = new KingEndMove();
    public BatEndMove bat = new BatEndMove();
    public CameraEndMove camera = new CameraEndMove();
    public EndTextTyping text = new EndTextTyping();

    int time = 0;

    public void startProcess()
    {
        StartCoroutine(happyendProcess());
        camera.transform.position = new Vector3(939.46f, 113.67f, -10);
    }

    IEnumerator happyendProcess()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.02f);
            time += 1;
        }
    }
    void Update()
    {
        if (time == 50)
        {
            player.onRun();
        }
        if (time == 190)
        {
            player.offRun();
        }
        if (time == 240)
        {
            player.Flip();
            king.onRun();

        }
        if (time == 300)
        {
            king.offRun();
        }
        if (time == 330)
        {
            king.onBubble();
            king.onAngry();
        }
        if (time == 380)
        {
            king.offBubble();
            king.offAngry();
            king.Anim = king.goAnim;
        }
        if (time == 400)
        {
            player.nonFlip();

        }
        if (time == 500)
        {
            player.offRun();
        }
        if (time == 535)
        {
            player.standUp();
        }
        if (time == 540)
        {
            player.standUp();
            bat.onUp();
        }
        if (time == 570)
        {
            player.standUp();
        }
        if (time == 640)
        {
            bat.offUp();
        }
        if (time == 660)
        {
            player.Flip();
        }
        if (time == 750)
        {
            bat.onRight();
        }
        if (time == 900)
        {
            camera.offView();
        }
        if (time == 925)
        {
            text.startType();
        }
        if (time == 1700)
        {
            SceneManager.LoadScene("Start");
        }

    }
}
