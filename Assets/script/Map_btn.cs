using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map_btn : MonoBehaviour
{
    public void ButtonClick() // 버튼 클릭시
    {
        SceneManager.LoadScene("Map"); //맵 불러오기
    }
    void Start()
    {

    }
    void Update()
    {

    }
}
