using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    InitManager init;
    public Image pause; // �Ͻ����� �̹���
    public Image back; // ���ư��� �̹���

    //ȿ����
    //��ư Ŭ���� ȿ����
    public AudioSource BtnBgm; // ��ư Ŭ�� �� ȿ����

    void Start()
    {
        init = GameObject.Find("InitManager").GetComponent<InitManager>();

        back = GameObject.Find("Back_img").GetComponent<Image>(); // ���ư��� �̹���
        back.enabled = false; // �Ⱥ��̰�

        pause = GameObject.Find("Pause_img").GetComponent<Image>(); // �Ͻ����� �̹���
        pause.enabled = false; // �Ⱥ��̰�
    }

    void Update()
    {
        
    }

    public void HouseClick(Button btn)  // �� ������ ������
    {
        BtnBgm.Play(); // ��ư Ŭ�� ��
        init.pos = int.Parse(btn.name) * 10;  
        SceneManager.LoadScene("GameScene"); // ���Ӿ��� �ҷ��´�
    } 

    public void CancelClick() 
    {
        BtnBgm.Play(); // ��ư Ŭ�� ��
        SceneManager.LoadScene("GameScene"); 
    }
}
