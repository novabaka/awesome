using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingControl : MonoBehaviour
{

    public BadEndTwo badendtwo = new BadEndTwo();
    public HappyEndTwo happyendtwo = new HappyEndTwo();

    public static int ending = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(ending == 1)
        {
            Debug.Log("BadEnd");
        }
        else if (ending == 2)
        {
            Debug.Log("HappyEnd");
        }
        else if (ending == 3)
        {
            badendtwo.startProcess();
        }
        else if(ending == 4)
        {
            happyendtwo.startProcess();
        }
    }



    void Update()
    {
        
    }
}
