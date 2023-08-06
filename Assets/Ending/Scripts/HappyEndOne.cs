using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HappyEndOne : MonoBehaviour
{

    public CameraEndMove camera = new CameraEndMove();
    public EndTextTyping text = new EndTextTyping();
    int time = 0;
    public void startProcess()
    {
        camera.transform.position = new Vector3(973.6f, -0.4f, -10);
        camera.offView();
        StartCoroutine(happyEndProcess());
    }

    IEnumerator happyEndProcess()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.02f);
            time += 1;
        }
    }

    private void Update()
    {
        if (time == 50)
        {
            text.startType();
        }
        if (time == 850)
        {
            SceneManager.LoadScene("Start");
        }
    }
}
