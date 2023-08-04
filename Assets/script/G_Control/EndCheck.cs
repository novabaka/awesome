using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndCheck : MonoBehaviour
{
    public int Enumber;

    public static int BKnightCount = 0;
    public static int LKnightCount = 0;

    public static int RangerCount = 0;

    public GameObject[] SummonPoint;

    public GameObject[] EndPlayer;
    public GameObject BKnight;
    public GameObject LKnight;
    public GameObject Ranger;

    public static bool EndChecking = false;

    void Start()
    {
        if (Enumber == 1)
        {
            EndPlayer[0].SetActive(false);
        }
        else if (Enumber == 2)
        {
            EndPlayer[0].SetActive(false);
            EndPlayer[1].SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            EndChecking = true;
            Destroy(collision.gameObject);
            StartCoroutine(camera());
            StartCoroutine(End());
            StartCoroutine(EndSummon());
        }
    }

    IEnumerator camera()
    {
        yield return new WaitForSeconds(0.1f);
        C_Camera.number = Enumber + 1;
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(0.05f);
        if (Enumber == 1)
        {
            EndPlayer[0].SetActive(true);
        }
        else if (Enumber == 2)
        {
            EndPlayer[0].SetActive(true);
            EndPlayer[1].SetActive(true);
        }
    }

    IEnumerator EndSummon()
    {
        if (Enumber == 1)
        {
            yield return new WaitForSeconds(0.1f);
            for (int l = 1; l <= LKnightCount; l++)
            {
                Instantiate(LKnight, SummonPoint[l].transform.position, transform.rotation);
                yield return new WaitForSeconds(0.02f);
            }

            for (int b = 1; b <= BKnightCount; b++)
            {
                Instantiate(BKnight, SummonPoint[LKnightCount + b].transform.position, transform.rotation);
                yield return new WaitForSeconds(0.02f);
            }

            yield return new WaitForSeconds(0.3f);

            if ((LKnightCount + BKnightCount) > 6)
            {
                Bad_End();
            }
            else if ((LKnightCount + BKnightCount) <= 6)
            {
                Happy_End();
            }
        }
        else if (Enumber == 2)
        {
            yield return new WaitForSeconds(0.1f);
            for (int r = 1; r <= RangerCount; r++)
            {
                Instantiate(Ranger, SummonPoint[r].transform.position, transform.rotation);
                yield return new WaitForSeconds(0.02f);
            }

            yield return new WaitForSeconds(0.3f);

            if (RangerCount > 4)
            {
                Bad_End2();
            }
            else if (RangerCount <= 4)
            {
                Happy_End2();
            }
        }
    }

    void Bad_End()
    {

    }

    void Happy_End()
    {

    }

    void Bad_End2()
    {

    }

    void Happy_End2()
    {

    }
}