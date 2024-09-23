using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Typingeffect_s4 : MonoBehaviour
{
    public Text tx;
    private string m_text1 = "토끼 인형 : 이장님 혹시 다람쥐 인형이 안 보이는데 어디 갔나요?\n";
    private string m_text2 = "곰 이장 : 다람쥐 인형은 어제부로 이사를 갔어요.\n";
    private string m_text3 = "토끼 인형 : 아..네..  (뭔가 이상한데.. 다람쥐집을 한 번 가봐야겠어)";


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
