using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public string SceneName;
    public void loadScene()
    {
        SceneManager.LoadScene(SceneName);
    }
}
