using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueSkip : MonoBehaviour
{
    [SerializeField] string sceneName;
    public void onClickButton()
    {
        SceneManager.LoadScene(sceneName);
    }
}
