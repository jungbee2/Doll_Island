using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSt_btn : MonoBehaviour
{
    //효과음

    //버튼 클릭시 효과음
    public AudioSource BtnBgm; // 버튼 클릭 시 효과음

    public void ButtonClick()
    {
        BtnBgm.Play(); // 스토리 버튼 클릭 시 효과음
        SceneManager.LoadScene("story10");
    }
    void Start()
    {

    }
    void Update()
    {

    }
}
