using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class topBad : MonoBehaviour
{
    public float changeTime;
    public string SceneName;
    [SerializeField] EndTextTyping text = new EndTextTyping();


    void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 16 && changeTime >= 15.9)
        {
            text.startType();
        }
        else if (changeTime <= 0)
        {
            SceneManager.LoadScene(SceneName);
        }

    }
}
