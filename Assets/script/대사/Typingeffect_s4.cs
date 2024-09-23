using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Typingeffect_s4 : MonoBehaviour
{
    public Text tx;
    private string m_text1 = "�䳢 ���� : ����� Ȥ�� �ٶ��� ������ �� ���̴µ� ��� ������?\n";
    private string m_text2 = "�� ���� : �ٶ��� ������ �����η� �̻縦 �����.\n";
    private string m_text3 = "�䳢 ���� : ��..��..  (���� �̻��ѵ�.. �ٶ������� �� �� �����߰ھ�)";


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_typing());
    }

    IEnumerator _typing()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i <= m_text1.Length; i++)
        {
            tx.text = m_text1.Substring(0, i);

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i <= m_text2.Length; i++)
        {
            tx.text = m_text1 + m_text2.Substring(0, i);

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i <= m_text3.Length; i++)
        {
            tx.text = m_text1 + m_text2 + m_text3.Substring(0, i);

            yield return new WaitForSeconds(0.1f);
        }

        
    }
}
