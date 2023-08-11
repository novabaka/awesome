using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class topHappy : MonoBehaviour
{
    public float changeTime;
    public string SceneName;
    [SerializeField] EndTextTyping text = new EndTextTyping();


    void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 13 && changeTime >= 12.9)
        {
            text.startType();
        }
        else if (changeTime <= 0)
        {
            SceneManager.LoadScene(SceneName);
        }

    }
}
