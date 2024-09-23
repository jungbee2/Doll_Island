using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    InitManager init;
    public Image pause; // 일시정지 이미지
    public Image back; // 돌아가기 이미지

    //효과음
    //버튼 클릭시 효과음
    public AudioSource BtnBgm; // 버튼 클릭 시 효과음

    void Start()
    {
        init = GameObject.Find("InitManager").GetComponent<InitManager>();

        back = GameObject.Find("Back_img").GetComponent<Image>(); // 돌아가기 이미지
        back.enabled = false; // 안보이게

        pause = GameObject.Find("Pause_img").GetComponent<Image>(); // 일시정지 이미지
        pause.enabled = false; // 안보이게
    }

    void Update()
    {
        
    }

    public void HouseClick(Button btn)  // 집 아이콘 누를시
    {
        BtnBgm.Play(); // 버튼 클릭 시
        init.pos = int.Parse(btn.name) * 10;  
        SceneManager.LoadScene("GameScene"); // 게임씬을 불러온다
    } 

    public void CancelClick() 
    {
        BtnBgm.Play(); // 버튼 클릭 시
        SceneManager.LoadScene("GameScene"); 
    }
}
