using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PrologueTextTyping : MonoBehaviour
{
    public float delay; //타이핑 속도
    public float Skip_delay; //타이핑 후 다음내용으로 넘길수있는 딜레이
    public int cnt;

    //타이핑효과
    public string[] fulltext;
    public int dialog_cnt;
    string currentText;

    public bool text_exit;
    bool text_full;
    bool text_cut;

    public void startType()
    {
        getTyping(dialog_cnt, fulltext);
    }


    //텍스트 출력 완료 탈출
    void Update()
    {
        if (text_exit == true)
        {
            gameObject.SetActive(false);
        }

    }

    //타이핑 후 다음내용, 종료
    public void endTyping()
    {
        if (text_full == true)
        {
            cnt++;
            text_full = false;
            text_cut = false;
            StartCoroutine(ShowText(fulltext));
        }
        else
        {
            text_cut = true;
        }
    }

    //텍스트 시작호출
    public void getTyping(int _dialog_cnt, string[] _fullText)
    {
        text_exit = false;
        text_full = false;
        text_cut = false;
        cnt = 0;

        dialog_cnt = _dialog_cnt;
        fulltext = new string[dialog_cnt];
        fulltext = _fullText;

        StartCoroutine(ShowText(fulltext));
    }

    IEnumerator ShowText(string[] _fullText)
    {
        if (cnt >= dialog_cnt)
        {
            text_exit = true;
            StopCoroutine("showText");
        }
        else
        {
            currentText = "";
            for (int i = 0; i < _fullText[cnt].Length; i++)
            {
                if (text_cut == true)
                {
                    break;
                }
                currentText = _fullText[cnt].Substring(0, i + 1);
                this.GetComponent<Text>().text = currentText;
                yield return new WaitForSeconds(delay);
            }
            this.GetComponent<Text>().text = _fullText[cnt];
            yield return new WaitForSeconds(Skip_delay);

            text_full = true;
        }
    }
}
