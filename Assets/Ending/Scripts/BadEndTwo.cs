using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BadEndTwo : MonoBehaviour
{

    public PlayerEndMove player = new PlayerEndMove();
    public KingEndMove king = new KingEndMove();
    public BatEndMove bat = new BatEndMove();
    public BowmanMove bowman1 = new BowmanMove();
    public BowmanMove bowman2 = new BowmanMove();
    public BowmanMove bowman3 = new BowmanMove();
    public CameraEndMove camera = new CameraEndMove();
    public EndTextTyping text = new EndTextTyping();

    int time = 0;
    public void startProcess()
    {
        camera.transform.position = new Vector3(939.46f, 113.67f, -10);
        StartCoroutine(badendProcess());
    }

    IEnumerator badendProcess()
    {
        while (true) {
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
            bowman1.onMove();
            bowman2.onMove();
            bowman3.onMove();

        }
        if (time == 300)
        {
            king.offRun();
            bowman1.offMove();
            bowman2.offMove();
            bowman3.offMove();
        }
        if( time == 330)
        {
            king.onBubble();
            king.onAngry();
        }
        if(time == 380)
        {
            king.offBubble();
            king.offAngry();
            king.Anim = king.goAnim;
        }
        if(time == 400)
        {
            player.nonFlip();
            bowman1.onMove();
            bowman2.onMove();
            bowman3.onMove();

        }
        if(time == 500)
        {
            player.offRun();
            bowman1.offMove();
            bowman2.offMove();
            bowman3.offMove();
        }
        if(time == 535)
        {
            player.standUp();
        }
        if(time == 540)
        {
            player.standUp();
            bat.onUp();
        }
        if(time == 570)
        {
            player.standUp();
        }
        if(time == 640)
        {
            bat.offUp();
        }
        if(time== 660)
        {
            player.Flip();
            bowman1.onShot();
            bowman2.onShot();
            bowman3.onShot();
        }
        if(time == 675)
        {
            bowman1.offShot();
            bowman2.offShot();
            bowman3.offShot();
        }
        if(time == 750)
        {
            bat.onDown();
        }
        if (time == 900)
        {
            camera.offView();
        }
        if(time == 925)
        {
            text.startType();
        }
        if(time == 1700)
        {
            SceneManager.LoadScene("Start");
        }
        
    }
}
