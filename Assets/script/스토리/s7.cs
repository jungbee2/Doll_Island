using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class s7 : MonoBehaviour
{
    //ȿ����

    //��ư Ŭ���� ȿ����
    public AudioSource BtnBgm; // ��ư Ŭ�� �� ȿ����

    public void ButtonClick()
    {
        BtnBgm.Play(); // ���丮 ��ư Ŭ�� �� ȿ����
        SceneManager.LoadScene("story8");
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
