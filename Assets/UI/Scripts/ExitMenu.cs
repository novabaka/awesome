using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenu : MonoBehaviour
{
    
    public string SceneName;
    public void loadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneName);
    }

}
