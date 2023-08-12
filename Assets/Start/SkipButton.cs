using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : SceneControl
{
    public string SceneName;
    public void OnClickButton()
    {
        loadScene();
    }
}
