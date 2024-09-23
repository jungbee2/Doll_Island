using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GSManager : MonoBehaviour
{
    InitManager init;// �ҷ�����

    int duckClick = 0; //�������� Ŭ��

    int step = 0;   // ��й�ȣ Ŭ��
    int cnt = 0; // ��й�ȣ �ڸ� �� ����

    //������ 
    public GameObject key;
    public GameObject key2;
    public GameObject oil;
    public GameObject fire;
    public GameObject map;

    //ȿ����

    //��ư Ŭ���� ȿ����
    public AudioSource BtnBgm; // ��ư Ŭ�� �� ȿ����

    //������ ��� ȿ����
    public AudioSource ItemGet; // ������ ������ ȿ����

    //���, �ݰ�
    public AudioSource PassBtn; // ��� ���� �� ȿ����
    public AudioSource OkPass;  // �ݰ� ��� ���� �� ȿ����
    public AudioSource NotPass; // ��� Ʋ�� �� ȿ����
    public AudioSource DoorlockPass; // ����� ��� ���� �� ȿ����

    // �� ������, ����ִ�
    public AudioSource keyBgm; //���� ������ ȿ����
    public AudioSource keyDrop; //���� �������� ȿ���� 
    public AudioSource OpenDoor; // �� ������ ȿ����
    public AudioSource LockDoor; // �� ����ִ� ȿ����

    // ���� ��ġ��
    public AudioSource ducktouch; // �������� ��ġ�� ȿ����

    //�ؽ�Ʈ
    public Text stime; // �ؽ�Ʈ

    //�̹���
    public GameObject pwdImg; //  �ξ��̹� -9382  ��й�ȣ �̹���
    public GameObject mapImg; //  ������ -���������� �̹���
    public GameObject oilImg; // ������ - ���� ������ �̹���

    void Start()
    {
        ApplicationChrome.statusBarState = ApplicationChrome.States.Hidden;
   
        init = GameObject.Find("InitManager").GetComponent<InitManager>();  

        init.slider.enabled = true; //�����̴�
        init.daynight.enabled = true; // �� �� ���� �ð�
         
        init.pause.enabled = true; //  �Ͻ�����
        init.back.enabled = true; //  ���ư���

        if (init.gameOver)
        {
            init.back = GameObject.Find("Back_img").GetComponent<Image>();  // ���ư��� ������
            init.back.enabled = false;  // �Ⱥ��̰�

            init.pause = GameObject.Find("Pause_img").GetComponent<Image>();  // �Ͻ����� ������
            init.pause.enabled = false;  // �Ⱥ��̰�

            init.slider = GameObject.Find("Slider_img").GetComponent<Image>();   // �����̴� ������
            init.slider.enabled = false;  // �Ⱥ��̰�

            init.daynight = GameObject.Find("DayNight_img").GetComponent<Image>(); // ���� ȸ�� ������
            init.daynight.enabled = false; // �Ⱥ��̰�

            // �㳷 �� ���� ������ ���� �ڵ�
        }

        // ������ �Ⱥ��̰� ����
        if (init.data.getItem.Contains(1)) key2.SetActive(false);
        if (init.data.getItem.Contains(2)) oil.SetActive(false);
        if (init.data.getItem.Contains(3)) fire.SetActive(false);
        if (init.data.getItem.Contains(4)) map.SetActive(false);

        // �㳷 ��� �ʱ�ȭ �ڵ� 
        if (init.data.isDay) GameObject.Find("PanelMain").GetComponent<Image>().color = init.day;
        else GameObject.Find("PanelMain").GetComponent<Image>().color = init.night;
        DisplayRoom();
    }

    void Update()
    {
    }

    // â�� ��� : 2952 , ������ �̿��� ��
    public void StoragePwd(int pass)
    {
        PassBtn.Play();
        cnt++;
        switch (pass)
        {
            case 0:
            case 1:
                step = 0;
                break;

            case 2://
                if (step == 0)
                {
                    step++;
                }
                else if (step == 3)
                {
                    step++;
                }
                else step = 0;
                { 
                }
                break;

            case 3:
            case 4:
                step = 0;
                break;

            case 5://
                if (step == 2) step++;
                else step = 0;
                break;

            case 6:
            case 7:
            case 8:
                step = 0;
                break;

            case 9://
                if (step == 1) step++;
                else step = 0;
                break;

            case 10: //��
            case 11: //��
                cnt--;
                if (step == 4 & cnt == 4)
                {
                    DoorlockPass.Play(); //����� ������ ȿ����, �˾�
                    init.pos =121; //�����̵�
                    StartCoroutine(DelayedLoadScene());
                    Debug.Log("okay");
                    init.SendMessage("ShowMsg", "OKAY!");
                    break;
                }
                else
                    NotPass.Play();
                Debug.Log("NOPE");
                init.SendMessage("ShowMsg", "NOPE");
                step = 0;
                cnt = 0;
                break;
        }
    }

    // ���� �ݰ� ��� : 3469  , ���� ��� ����
    public void BearPwd(int pass)
    {
        PassBtn.Play();
        cnt++;
        switch (pass)
        {
            case 1:
            case 2:
                break;
         
            case 3://
                step++;
                break;

            case 4://
                step++;
                break;

            case 5:
                break;

            case 6://
                step++;
                break;

            case 7:
            case 8:
                break;

            case 9://
               step++;
                break;

            case 10: //Ȯ��
                cnt--;
                if (step == 4 & cnt==4) 
                {
                    OkPass.Play();
                    init.pos = 23; //�ݰ��
                    StartCoroutine(DelayedLoadScene());
                    Debug.Log("okay"); //�˾�
                    init.SendMessage("ShowMsg", "OKAY!");
                    break;
                }
                else
                NotPass.Play();
                Debug.Log("NOPE");
                init.SendMessage("ShowMsg", "NOPE");
                step = 0;
                cnt = 0;
                break;
        }
    }

    // �ξ��̹� ��� : 9342 , ������ �̿��� ��
    public void Pwd(int pass)
    {
        PassBtn.Play();
        cnt++;
        switch (pass)
        {
            case 0:
               
            case 1:
                step = 0;
                break;

            case 2:
                if (step == 3)  step++;
                else step = 0;
                break;

            case 3:
                if (step == 1) step++;
                else step = 0;
                break;

            case 4:
                step = 0;
                break;

            case 5:
            case 6:
            case 7:
                step = 0;
                break;
            case 8:
                if (step == 2) step++;
                else step = 0;
                break;
                
            case 9:
                if (step == 0) step++;
                else  step = 0;
                break;

            case 10: //��
            case 11: //��
                cnt--;
                if (step == 4 & cnt == 4)
                {
                    DoorlockPass.Play(); //����� ������ ȿ����, �˾�
                    init.pos = 82;
                    StartCoroutine(DelayedLoadScene());
                    Debug.Log("okay");
                    init.SendMessage("ShowMsg", "OKAY!");
                    break;
                }
                else
                NotPass.Play();
                Debug.Log("NOPE");
                init.SendMessage("ShowMsg", "NOPE");
                step = 0;
                cnt = 0;
                break;
        }       
    }
    IEnumerator DelayedLoadScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameScene");
    }
    public void bearOpendoorclick()
    {
        keyBgm.Play(); // ���赹���� �Ҹ�
        if (init.data.getItem.Contains(0) & init.pos == 21) // 0�� ������, �� ���踦 �̹� ������ �ִ� ��� �׸��� 21�� ���� ���
        {
            GameObject.Find("PanelRoom").transform.Find(init.pos.ToString()).gameObject.SetActive(false);
            init.pos++;
            DisplayRoom();
            OpenDoor.Play(); // �湮���¼Ҹ�

            Debug.Log("���� ���� �������ϴ�!"); //���� ��ȭâ ó��
            init.SendMessage("ShowMsg", "���� ���� �������ϴ�!");
        }
        else // 0�� ������, �� ���踦 ���� ȹ�� ���� ���
        {
            LockDoor.Play(); // ���� �ִ¼Ҹ�
            Debug.Log("���踦 ȹ���� ������!"); //���� ��ȭâ ó�� 
            init.SendMessage("ShowMsg", "���踦 ȹ���� ������!");
        }
    }
    public void TouchDuck() // �������� 3�� ������ ���谡 ��������
    {
        ducktouch.Play(); // �������� ��ġ ȿ����
        StartCoroutine(ShakeDuck());
        duckClick++;
        if (duckClick >= 3 & !init.data.getItem.Contains(0))
        {
            keyDrop.Play(); // ���� �������� �Ҹ�
            Debug.Log("���� �ڿ��� ���谡 ��������!!");
            init.SendMessage("ShowMsg", "���� �ڿ��� ���谡 ��������!!");
            key.SetActive(true);
        }
    }
    IEnumerator ShakeDuck() // ���� ���� ������, ��鸰��.
    {
        GameObject.Find("Duck_img").transform.eulerAngles = new Vector3(0, 0, 5);
        yield return new WaitForSeconds(0.05f);
        GameObject.Find("Duck_img").transform.eulerAngles = new Vector3(0, 0, -5);
        yield return new WaitForSeconds(0.05f);
        GameObject.Find("Duck_img").transform.eulerAngles = new Vector3(0, 0, 5);
        yield return new WaitForSeconds(0.05f);
        GameObject.Find("Duck_img").transform.eulerAngles = new Vector3(0, 0, 0);
    }
    public void ShowPwd() // ��й�ȣ
    {
        pwdImg.SetActive(true);
    }

    public void ShowOil() // �⸧
    {
        oilImg.SetActive(true);
        init.data.oilFull = true;
        InitManager.SaveIngameData(init.data);
    }

    public void DisplayRoom() 
    {
        GameObject.Find("PanelRoom").transform.Find(init.pos.ToString()).gameObject.SetActive(true);

        if (init.pos == 81)   // 81���� 
        {
            pwdImg.SetActive(false); //  ��й�ȣ �̹��� �Ⱥ��̰� ����
        }
        else if (init.pos == 111) // 111���� 
        {
            if (init.data.oilFull) // �⸧�� ���� �� ���
            {
                oilImg.SetActive(true); // ���̰�
            }
            else // �ƴҰ��
            {
                oilImg.SetActive(false); // �Ⱥ��̰�
            }

            if (init.data.getItem.Contains(4)) // �ؾ������� ���� ���� ���
            {
                mapImg.SetActive(true); // ���̰�
            }
            else // �ƴҰ��
            {
                mapImg.SetActive(false); // �Ⱥ��̰�
            }
        }
        else if (init.pos == 140)
        {
            int d = ((int)init.nowTime / 180) + 1;
            int h = (int)init.nowTime % 180 * 24 / 180;

            Debug.Log(d + "��" + h + "�ð� ��� "); // �ܼ�â ���
            stime.text = (d + "��" + " " + h + "�ð�"); // ȭ�鿡 ���(������)
        }
    }
 
    public void owlPwd() // 80 �ξ��� �ܺ� 
    {
        if (init.data.isDay || init.pos == 80) // ���ῡ �ξ��̹�
        {
        }
        else
        {
            LockDoor.Play(); // ���� �ִ¼Ҹ�
            Debug.Log("���� ��� �� �� �ֽ��ϴ�!");
            init.SendMessage("ShowMsg", "���� ��� �� �� �ֽ��ϴ�!");
        }
        key.SetActive(false);
    }

    public void MainButtonClick() //  ���� ���� �� �������� ���� ��ư Ŭ����
    {
        BtnBgm.Play(); // ��ư Ŭ�� ��
        init.gameOver = false;

        init.back = GameObject.Find("Back_img").GetComponent<Image>();  // ���ư��� ������
        init.back.enabled = false;  // �Ⱥ��̰�

        init.pause = GameObject.Find("Pause_img").GetComponent<Image>();  // �Ͻ����� ������
        init.pause.enabled = false;  // �Ⱥ��̰�

        init.slider = GameObject.Find("Slider_img").GetComponent<Image>();   // �����̴� ������
        init.slider.enabled = false;  // �Ⱥ��̰�

        init.daynight = GameObject.Find("DayNight_img").GetComponent<Image>(); // ���� ȸ�� ������
        init.daynight.enabled = false; // �Ⱥ��̰�
        
        init.data = new SaveData();
        InitManager.SaveIngameData(init.data);

        // init.data =  init.LoadIngameData(); // ������ �ε�, ���� ���� ù �����̸� ���⿡ null �� ����.

        DontDestroyOnLoad(GameObject.Find("InitManager"));
        SceneManager.LoadScene("Main"); // ����ȭ��
        Debug.Log("��������");
    }

    public void GetKey() //���� �������� ��� �Ǵ� �Լ�, ������ ȹ��
    {
        if(init.data.getItem.Contains(0)) // 0�� ������, �� ���踦 �̹� ������ �ִ� ���
        {
            Debug.Log("�̹� ȹ���� �����Դϴ�!"); //���� ��ȭâ ó��
            init.SendMessage("ShowMsg", "�̹� ȹ���� �����Դϴ�!");
        }
        else // 0�� ������, �� ���踦 ���� ȹ�� ���� ���
        {
            ItemGet.Play(); // ������ ��� ȿ����
            Debug.Log("����1�� ȹ���Ͽ����ϴ�! ������ Ȯ���� ������!"); //���� ��ȭâ ó��
            init.SendMessage("ShowMsg", "����1�� ȹ���Ͽ����ϴ�!\n������ Ȯ���� ������!");

            init.data.getItem.Add(0); //0�� ������. �� ���� �κ��丮�� �߰�
            InitManager.SaveIngameData(init.data); //������ ����
        }
        key.SetActive(false);
    }

    public void GetKey2() //���� �������� ��� �Ǵ� �Լ�
    {
        if (init.data.getItem.Contains(1)) // 1�� ������, �� ����2�� �̹� ������ �ִ� ���
        {
            Debug.Log("�̹� ȹ���� �����Դϴ�!"); //���� ��ȭâ ó��
            init.SendMessage("ShowMsg", "�̹� ȹ���� �����Դϴ�!");
        }
        else // 1�� ������, �� ����2�� ���� ȹ�� ���� ���
        {
            ItemGet.Play(); // ������ ��� ȿ����
            Debug.Log("����2�� ȹ���Ͽ����ϴ�! ������ Ȯ���� ������!"); //���� ��ȭâ ó��
            init.SendMessage("ShowMsg", "����2�� ȹ���Ͽ����ϴ�!\n������ Ȯ���� ������!");

            init.data.getItem.Add(1); //1�� ������. �� ����2 �κ��丮�� �߰�
            InitManager.SaveIngameData(init.data); //������ ����
        }
        key2.SetActive(false);
    }

    public void GetOil() //�⸧ �������� ��� �Ǵ� �Լ�
    {
        if (init.data.getItem.Contains(2)) // 2�� ������, �� �⸧�� �̹� ������ �ִ� ���
        {
            Debug.Log("�̹� ȹ���� �����Դϴ�!"); //���� ��ȭâ ó��
            init.SendMessage("ShowMsg", "�̹� ȹ���� �����Դϴ�!");
        }
        else // 2�� ������, �� �⸧�� ���� ȹ�� ���� ���
        {
            ItemGet.Play(); // ������ ��� ȿ����
            Debug.Log("�⸧�� ȹ���Ͽ����ϴ�! ������ Ȯ���� ������!"); //���� ��ȭâ ó��
            init.SendMessage("ShowMsg", "�⸧�� ȹ���Ͽ����ϴ�!\n������ Ȯ���� ������!");

            init.data.getItem.Add(2); //2�� ������. �� �⸧ �κ��丮�� �߰�
            InitManager.SaveIngameData(init.data); //������ ����
        }
        oil.SetActive(false);
    }

    public void GetFire() //���� �������� ��� �Ǵ� �Լ�
    {
        if (init.data.getItem.Contains(3)) // 3�� ������, �� ������ �̹� ������ �ִ� ���
        {
            fire.SetActive(false);
            InitManager.SaveIngameData(init.data); //  ���� ��� �� �ڿ��� ������ ����
            Debug.Log("�̹� ȹ���� �����Դϴ�!"); //���� ��ȭâ ó��
            init.SendMessage("ShowMsg", "�̹� ȹ���� �����Դϴ�!");
        }
        else // 3�� ������, �� ������ ���� ȹ�� ���� ���
        {
            ItemGet.Play(); // ������ ��� ȿ����
            Debug.Log("������ ȹ���Ͽ����ϴ�! ������ Ȯ���� ������!"); //���� ��ȭâ ó��
            init.SendMessage("ShowMsg", "������ ȹ���Ͽ����ϴ�!\n������ Ȯ���� ������!");

            init.data.getItem.Add(3); //3�� ������. �� ���� �κ��丮�� �߰�
            InitManager.SaveIngameData(init.data); //������ ����
        }
        fire.SetActive(false);
        InitManager.SaveIngameData(init.data); //  ���� ��� �� �ڿ��� ������ ����
    }

    public void GetMap() //�ؾ� ������ ��� �Ǵ� �Լ�
    {
        fire.SetActive(false);
        InitManager.SaveIngameData(init.data); //  ���� ��� �� �ڿ��� ������ ����

        if (init.data.getItem.Contains(4)) // 4�� ������, �� ������ �̹� ������ �ִ� ���
        {
          
            Debug.Log("�̹� ȹ���� �����Դϴ�!"); //���� ��ȭâ ó��
            init.SendMessage("ShowMsg", "�̹� ȹ���� �����Դϴ�!");
        }
        else if(init.data.getItem.Contains(3))//4�� ������, �� �ؾ������� ���� ȹ�� ���� ��� , ���� ������ �ִ� ���
        {
            ItemGet.Play(); // ������ ��� ȿ����
            Debug.Log("�ؾ������� ȹ���Ͽ����ϴ�! ������ Ȯ���� ������!"); //���� ��ȭâ ó��
            init.SendMessage("ShowMsg", "�ؾ������� ȹ���Ͽ����ϴ�!\n������ Ȯ���� ������!");

            init.data.getItem.Add(4); //3�� ������. �� �ؾ����� �κ��丮�� �߰�
            init.data.getItem.Remove(3); //���� ����

            InitManager.SaveIngameData(init.data); //������ ����
        }
        else
        {
            Debug.Log("������ �ʿ� �� �� ���׿�.."); //���� ��ȭâ ó��
            init.SendMessage("ShowMsg", "������ �ʿ� �� �� ���׿�..");
        }
        fire.SetActive(false);
        map.SetActive(false);
    }

    public void DoorClickButton() // ���� ������ �Ѿ��
    {
        BtnBgm.Play(); // ��ư Ŭ�� ��
        if (init.data.isDay || init.pos == 80 || init.pos == 100 || init.pos == 110 || init.pos == 120) // ���ῡ �ξ��̹�, ����, ������, â��
        {
            GameObject.Find("PanelRoom").transform.Find(init.pos.ToString()).gameObject.SetActive(false);
            init.pos++;
            DisplayRoom();
        }
        else
        {
            Debug.Log("���� ��� �� �� �ֽ��ϴ�..");
            init.SendMessage("ShowMsg", "���� ��� �� �� �ֽ��ϴ�..");
        }
    }

    public void RoomBackButton() // ���ư��� ��ư
    {
        BtnBgm.Play(); // ��ư Ŭ�� ��
        if (init.pos % 10 >= 1) // �� �ȿ� �ִ� ��� �� �ٱ����� ������
        {
            GameObject.Find("PanelRoom").transform.Find(init.pos.ToString()).gameObject.SetActive(false);
            init.pos--;
            GameObject.Find("GSManager").GetComponent<GSManager>().SendMessage("DisplayRoom");
        }
    }
}
