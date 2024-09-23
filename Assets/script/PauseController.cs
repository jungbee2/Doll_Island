using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    InitManager init;// 불러오기

    private bool pauseOn = false;

    public Image slider; // 슬라이더 바 이미지
    public Image daynight; // 낮 밤 구현 회전 이미지
    public Image pause; // 일시정지 이미지
    public Image back; // 돌아가기 이미지

    public SaveData data;  // 게임 종료 되어도 남아있어야 하는 저장이 필요한 데이터

    public void ActivePauseBt() // 일시정지 버튼 누를시
    {
        // 일시정지 버튼 누를시 처리
        if(!pauseOn)
        {
            Time.timeScale = 0; // 시간 흐름 비율 0
         
            init.DayBgm.Stop(); // 비지엠 멈춤
            init.NightBgm.Stop(); // 비지엠 멈춤 
        }
        else //버튼 다시 누를시 
        {
            Time.timeScale = 1; // 시간 흐름 비율 1
            init.GetComponent<InitManager>().SendMessage("DayNightBgm"); // 비지엠 재생
        }
        pauseOn = !pauseOn; // 불 값 반전
}
    public void QuitBt() // 게임종료 버튼 누를시
    {
        Application.Quit(); //게임종료 - 어플
        Debug.Log("게임종료");
    }
   
    void Start()
    {
        init = GameObject.Find("InitManager").GetComponent<InitManager>(); // init 
    }

    void Update()
    {
        
    }
}
