using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueControler : MonoBehaviour
{
    public CameraMove camera = new CameraMove();
    public PlayerMove player = new PlayerMove();
    public WomanMove woman = new WomanMove();
    public KingMove king = new KingMove();
    public EnemyMove enemy1 = new EnemyMove();
    public EnemyMove enemy2 = new EnemyMove();

    public bool cameraMove = true;
    int time = 0;
    void Update()
    {
        if (time == 50)
        {
            player.OnBubble();
        }
        if (time == 100)
        {
            player.OffBubble();
        }
        if(time == 150)
        {
            woman.OnBubble();
        }
        if(time == 200)
        {
            woman.OffBubble();
        }
        if (time == 250)
        {
            player.OnBubble();
            player.OnHeadKing();
        }
        if( time == 300)
        {
            player.OffBubble();
            player.OffHeadKing();
        }
        if (time == 350)
        {
            woman.OnBubble();
            woman.OnHeadKing();
            king.onKing();
            enemy1.onEnemy();
            enemy2.onEnemy();
        }
        if ( time == 400)
        {
            woman.OffBubble();
            woman.OffHeadKing();
            
        }
        if (time == 450)
        {
            king.kingMove = false;
            enemy1.enemyMove = false;
            enemy2.enemyMove = false;
            enemy1.Anim = enemy1.idleAnim;
            enemy2.Anim = enemy2.idleAnim;
            king.Anim = king.idleAnim;
            woman .OnBubble();
            woman.OnSmile();
            player.OnBubble();
            player.OnSmile();
            king.onBubble();
            king.onAngry();
        }
        if(time == 500)
        {
            woman.OffBubble();
            woman.OffSmile();
            player.OffBubble();
            player.OffSmile();
            king.offBubble();
            king.offAngry();
            player.Flip();      
        }
        if(time == 550)
        {
            king.Anim = king.goAnim;
            enemy1.onEnemy();
            enemy2.onEnemy();
            player.nonFlip();
        }
        if(time == 660)
        {
            camera.offView();
            player.offPlayer();
            woman.offWoman();
        }
        if (time == 800)
        {
            camera.onImg();
        }
        if(time == 1100)
        {
            SceneManager.LoadScene("main_game");
        }
    }
    void FixedUpdate()
    {
        if (!cameraMove)
        {
            time += 1;
        }
        
    }
}
