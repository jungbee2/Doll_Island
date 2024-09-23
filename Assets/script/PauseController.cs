using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    InitManager init;// �ҷ�����

    private bool pauseOn = false;

    public Image slider; // �����̴� �� �̹���
    public Image daynight; // �� �� ���� ȸ�� �̹���
    public Image pause; // �Ͻ����� �̹���
    public Image back; // ���ư��� �̹���

    public SaveData data;  // ���� ���� �Ǿ �����־�� �ϴ� ������ �ʿ��� ������

    public void ActivePauseBt() // �Ͻ����� ��ư ������
    {
        // �Ͻ����� ��ư ������ ó��
        if(!pauseOn)
        {
            Time.timeScale = 0; // �ð� �帧 ���� 0
         
            init.DayBgm.Stop(); // ������ ����
            init.NightBgm.Stop(); // ������ ���� 
        }
        else //��ư �ٽ� ������ 
        {
            Time.timeScale = 1; // �ð� �帧 ���� 1
            init.GetComponent<InitManager>().SendMessage("DayNightBgm"); // ������ ���
        }
        pauseOn = !pauseOn; // �� �� ����
}
    public void QuitBt() // �������� ��ư ������
    {
        Application.Quit(); //�������� - ����
        Debug.Log("��������");
    }
   
    void Start()
    {
        init = GameObject.Find("InitManager").GetComponent<InitManager>(); // init 
    }

    void Update()
    {
        
    }
}
