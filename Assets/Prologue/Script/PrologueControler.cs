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
    [SerializeField] GameObject GP;
    [SerializeField] GameObject MP;

    private void Awake()
    {
        Timeline.Play();
    }
    void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 9.7 && changeTime >= 9.6)
        {
            MP.SetActive(false);
            GP.SetActive(false);
        }
        else if(changeTime <= 9 && changeTime >= 8.9)
        {
            text.startType();
        }
        else if(changeTime <= 0)
        {
            SceneManager.LoadScene(SceneName);
        }
       
    }
   }
