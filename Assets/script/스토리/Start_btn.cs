using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_btn : MonoBehaviour
{
    InitManager init;

    //효과음

    //버튼 클릭시 효과음
    public AudioSource BtnBgm; // 버튼 클릭 시 효과음

    void Start()
    {
        init = GameObject.Find("InitManager").GetComponent<InitManager>();
    }

    public void ButtonClick()
    {
        BtnBgm.Play(); // 스토리 버튼 클릭 시 효과음
                     
        //빈 데이터를 생성후 저장, 이후 실행시 다시 스토리 씬을 보지 않아도 된다.
        init.data = new SaveData();
        InitManager.SaveIngameData(init.data);

        init.pos = 51;
        SceneManager.LoadScene("GameScene");
        init.GetComponent<InitManager>().SendMessage("DayNightBgm");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
