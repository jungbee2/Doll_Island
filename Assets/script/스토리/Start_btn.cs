using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_btn : MonoBehaviour
{
    InitManager init;

    //ȿ����

    //��ư Ŭ���� ȿ����
    public AudioSource BtnBgm; // ��ư Ŭ�� �� ȿ����

    void Start()
    {
        init = GameObject.Find("InitManager").GetComponent<InitManager>();
    }

    public void ButtonClick()
    {
        BtnBgm.Play(); // ���丮 ��ư Ŭ�� �� ȿ����
                     
        //�� �����͸� ������ ����, ���� ����� �ٽ� ���丮 ���� ���� �ʾƵ� �ȴ�.
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
