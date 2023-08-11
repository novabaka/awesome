using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bottomHappy : MonoBehaviour
{
    public float changeTime;
    public string SceneName;
    [SerializeField] EndTextTyping text = new EndTextTyping();


    void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 9 && changeTime >= 8.9)
        {
            text.startType();
        }
        else if (changeTime <= 0)
        {
            SceneManager.LoadScene(SceneName);
        }

    }
}
