using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PrologueTextTyping : MonoBehaviour
{
    public float delay; //Ÿ���� �ӵ�
    public float Skip_delay; //Ÿ���� �� ������������ �ѱ���ִ� ������
    public int cnt;

    //Ÿ����ȿ��
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


    //�ؽ�Ʈ ��� �Ϸ� Ż��
    void Update()
    {
        if (text_exit == true)
        {
            gameObject.SetActive(false);
        }

    }

    //Ÿ���� �� ��������, ����
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

    //�ؽ�Ʈ ����ȣ��
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
