using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class M_btn : MonoBehaviour
{
    InitManager init; // �ҷ�����

    //ȿ����
    //��ư Ŭ���� ȿ����
    public AudioSource BtnBgm; // ��ư Ŭ�� �� ȿ����

    void Start()
    {
        init = GameObject.Find("InitManager").GetComponent<InitManager>();
    }

    public void ButtonClick()  // ��ŸƮ ��ư
    {
        BtnBgm.Play(); // ��ư Ŭ�� ��
        if (init.data == null) SceneManager.LoadScene("story1"); //ù �����̸� ���丮 ������ �Ѿ��.
        else // ù ������ �ƴϸ� ��ȭ�����ΰ���.
        {
            init.slider.enabled = true; // true��
            init.daynight.enabled = true;// true��

            init.GetComponent<InitManager>().SendMessage("DayNightBgm");
            init.SendMessage("ShowMsg", "�־��� �ð��� 5���Դϴ�!");

            if (init.data.isDay) GameObject.Find("PanelMain").GetComponent<Image>().color = init.day;
            else GameObject.Find("PanelMain").GetComponent<Image>().color = init.night;
            SceneManager.LoadScene("Map");
        }
    }

    /*
    public void CheatButtonClick() // ���� �߿� ġƮ��ư ����
    {
        if (init.data == null) SceneManager.LoadScene("story10"); // ù �����̸� ���丮������ �Ѿ��.
        else // ù ������ �ƴϸ� ��ȭ������ ����.
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