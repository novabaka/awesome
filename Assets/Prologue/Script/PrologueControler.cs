using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PrologueControler : MonoBehaviour
{
    public float changeTime;
    public string SceneName;
    public PlayableDirector Timeline;
    [SerializeField] PrologueTextTyping text = new PrologueTextTyping();

    private void Awake()
    {
        Timeline.Play();
    }
    void Update()
    {
        changeTime -= Time.deltaTime;
        if(changeTime <= 9 && changeTime >= 8.9)
        {
            text.startType();
        }
        else if(changeTime <= 0)
        {
            SceneManager.LoadScene(SceneName);
        }
       
    }
   }
