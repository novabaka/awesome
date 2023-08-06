using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingControl : MonoBehaviour
{
    public BadEndOne badendone = new BadEndOne();
    public HappyEndOne happyendone = new HappyEndOne();
    public BadEndTwo badendtwo = new BadEndTwo();
    public HappyEndTwo happyendtwo = new HappyEndTwo();

    public static int ending = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(ending == 1)
        {
            badendone.startProcess();
        }
        else if (ending == 2)
        {
            happyendone.startProcess();
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
