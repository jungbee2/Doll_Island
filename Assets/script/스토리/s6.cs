using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s6 : MonoBehaviour
{
    ///ȿ����

    //���丮 ��ư Ŭ���� ȿ����
    public AudioSource StoryBtn; // ���丮 ��ư Ŭ�� �� ȿ����

    public void ButtonClick()
    {
        StoryBtn.Play(); // ���丮 ��ư Ŭ�� �� ȿ����
        SceneManager.LoadScene("story7");
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
