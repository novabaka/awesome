using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueText : TextTyping
{
    public string sceneName;
    void Update()
    {
        if (text_exit == true)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
