using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s4 : MonoBehaviour
{
    //효과음

    //스토리 버튼 클릭시 효과음
    public AudioSource StoryBtn; // 스토리 버튼 클릭 시 효과음

    public void ButtonClick()
    {
        StoryBtn.Play(); // 스토리 버튼 클릭 시 효과음
        SceneManager.LoadScene("story5");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}