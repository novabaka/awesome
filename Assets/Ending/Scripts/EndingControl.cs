using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingControl : MonoBehaviour
{
    public GameObject badendone;
    public GameObject happyendone;
    public GameObject badendtwo;
    public GameObject happyendtwo;

    public static int ending = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(ending == 1)
        {
            badendone.SetActive(true);
        }
        else if (ending == 2)
        {
            happyendone.SetActive(true);
        }
        else if (ending == 3)
        {
            badendtwo.SetActive(true);
        }
        else if(ending == 4)
        {
            happyendtwo.SetActive(true);
        }
    }
}
