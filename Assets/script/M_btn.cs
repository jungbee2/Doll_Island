using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class M_btn : MonoBehaviour
{
    InitManager init; // 불러오기

    //효과음
    //버튼 클릭시 효과음
    public AudioSource BtnBgm; // 버튼 클릭 시 효과음

    void Start()
    {
        init = GameObject.Find("InitManager").GetComponent<InitManager>();
    }

    public void ButtonClick()  // 스타트 버튼
    {
        BtnBgm.Play(); // 버튼 클릭 시
        if (init.data == null) SceneManager.LoadScene("story1"); //첫 실행이면 스토리 씬으로 넘어간다.
        else // 첫 실행이 아니면 맵화면으로간다.
        {
            init.slider.enabled = true; // true값
            init.daynight.enabled = true;// true값

            init.GetComponent<InitManager>().SendMessage("DayNightBgm");
            init.SendMessage("ShowMsg", "주어진 시간은 5일입니다!");

            if (init.data.isDay) GameObject.Find("PanelMain").GetComponent<Image>().color = init.day;
            else GameObject.Find("PanelMain").GetComponent<Image>().color = init.night;
            SceneManager.LoadScene("Map");
        }
    }

    /*
    public void CheatButtonClick() // 개발 중에 치트버튼 생성
    {
        if (init.data == null) SceneManager.LoadScene("story10"); // 첫 실행이면 스토리씬으로 넘어간다.
        else // 첫 실행이 아니면 맵화면으로 간다.
        {
            init.slider.enabled = true;
            init.daynight.enabled = true;

            if (init.data.isDay) GameObject.Find("PanelMain").GetComponent<Image>().color = init.day;
            else GameObject.Find("PanelMain").GetComponent<Image>().color = init.night;
            SceneManager.LoadScene("Map");
        }
        
    }
    */
    void Update()
    {

    }
}