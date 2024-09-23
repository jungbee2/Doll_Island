using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffAnim : MonoBehaviour
{
    InitManager init; //불러오기

    float time = 0; 
    float blinktime = 0.5f;
    float xtime = 0;
    float waittime = 0f;

    void Start()
    {
        init = GameObject.Find("InitManager").GetComponent<InitManager>();
    }

    // 저녁에 문 깜빡이기
    void Update()
    {
        if (!init.data.isDay) // 부정 = 밤 , 긍정 = 낮
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
          
            if (xtime < blinktime) // 깜빡
            {
                GetComponent<Image>().color = new Color(1, 1, 1, 1 - xtime * 10); //꺼졌다가
            }
            else if (xtime < waittime + blinktime)
            {

            }
            else
            {
                GetComponent<Image>().color = new Color(1, 1, 1, (xtime - (waittime + blinktime)) * 10);
                //켜졌다가
                if(xtime> waittime+ blinktime * 2)
                {
                    xtime = 0;
                    waittime *= 0.8f; //깜빡이는 시간 줄어들기
                    if(waittime < 0.02f)
                    {
                        time = 0;
                        waittime = 0.2f;
               
                    }
                }
            }
            xtime += Time.deltaTime;
           
            time += Time.deltaTime;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
