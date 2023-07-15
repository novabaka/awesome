using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public string sceneName;
    public void OnclickButton()
    {
        SceneManager.LoadScene(sceneName);
    }
}
