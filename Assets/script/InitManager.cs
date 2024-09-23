using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Mathematics;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class SaveData // ������ ����
{
    public float gameTime; // ����� ���� �ð�,  ���ǿ��� 5�ʸ��� ����Ǵ� ������
    public bool isDay; //���� ���̸� true, ���̸� false
    public List<int> getItem; //���� = 0, ����(2) = 1, �⸧ = 2, ���� = 3, �ؾ����� = 4 
    public bool oilFull; // �⸧ á���� ���� ���ؼ�
    public SaveData() // ������ ����
    {
        gameTime = 0f;
        isDay = true;
        getItem = new List<int>();
        oilFull = false; 
    }
}
public class InitManager : MonoBehaviour
{
    public bool gameOver = false; // false��

    public string[] items = new string[5] { "����", "����(2)", "�⸧", "����", "�ؾ�����" }; //������ ���ҽ� �̸��� �����ϴ� �迭

    float dt = 0f;
    float LimitTIme = 7200f; //���ѽð�-> 8���� 5�� ����ϸ� = 7200
    public float nowTime; // ����� ���� �ð� (��)

    //�ؽ�Ʈ
    public Text GameTimeText; // ���� �ð� ȭ�鿡 ǥ��

    public Color day; //���� �� ����
    public Color night; // ���� �� ����
    public SaveData data;  // ���� ���� �Ǿ �����־�� �ϴ� ������ �ʿ��� ������
    public int pos; // ���� ������ġ

    // �̹��� �� ��ư
    public Image slider; // �����̴� �� �̹���
    public Image daynight; // �� �� ���� ȸ�� �̹���
    public Button msgpop; // ���� �޽��� �˾�
    public Image pause; // �Ͻ����� �̹���
    public Image back; // ���ư��� �̹���

    bool opentrigger = false; 
    bool closetrigger = false;

    public Button timepop;// �����ð� �˾�â


    // �����
    public AudioSource DayBgm; // �� �����
    public AudioSource NightBgm; // �� �����

    //ȿ����
    //��ư Ŭ���� ȿ����
    public AudioSource BtnBgm; // ��ư Ŭ�� �� ȿ����


    // ���� �� ���̱�
    public AudioSource firebgm;// ���� �� ����Ҷ�

    //�⸧�ֱ�
    public AudioSource oilbgm; // ���� ������ ȿ����

    void Start() // �����Ҷ� 
    {
        ApplicationChrome.statusBarState = ApplicationChrome.States.Hidden;

        back = GameObject.Find("Back_img").GetComponent<Image>();  // ���ư��� ������
        back.enabled = false;  // �Ⱥ��̰�

        pause = GameObject.Find("Pause_img").GetComponent<Image>();  // �Ͻ����� ������
        pause.enabled = false;  // �Ⱥ��̰�

        slider = GameObject.Find("Slider_img").GetComponent<Image>();   // �����̴� ������
        slider.enabled = false;  // �Ⱥ��̰�

        daynight = GameObject.Find("DayNight_img").GetComponent<Image>(); // ���� ȸ�� ������
        daynight.enabled = false; // �Ⱥ��̰�

        data = LoadIngameData(); // ������ �ε�, ���� ���� ù �����̸� ���⿡ null �� ����.

        DontDestroyOnLoad(GameObject.Find("InitManager"));
        SceneManager.LoadScene("Main"); // ����ȭ�� �ҷ�����

      // Debug.Log(data.gameTime + "/" + LimitTIme); // Ȯ�ο�
    }

    public void ShowMsg(string str) // �޽��� �˾� ���� �Լ�
    {
        msgpop.transform.Find("Text").GetComponent<Text>().text = str;
        msgpop.gameObject.SetActive(true);
    }

    public void TimeMsg(string str) // �޽��� �˾� ���� �Լ�
    {
        timepop.transform.Find("Text").GetComponent<Text>().text = str;
        timepop.gameObject.SetActive(true);
    }

    public void DayNightBgm() // �� �� ��ȯ �����
    {
        if (data.isDay) // ��
        {
            DayBgm.Play(); //������ ���
            NightBgm.Stop(); //������ ����
        }
        else // ��
        { 
            DayBgm.Stop(); //������ ���
            NightBgm.Play(); //������ ����
        }
    }

    public void MapButtonClick() // �� ������ ��ư Ŭ����
    {
        BtnBgm.Play(); // ��ư Ŭ�� ��
        SceneManager.LoadScene("Map"); // ���� �ҷ�����
    }

    public void MainButtonClick() // �������� ���� ��ư Ŭ����
    {
        BtnBgm.Play(); // ��ư Ŭ�� ��

        back = GameObject.Find("Back_img").GetComponent<Image>();  // ���ư��� ������
        back.enabled = false;  // �Ⱥ��̰�

        pause = GameObject.Find("Pause_img").GetComponent<Image>();  // �Ͻ����� ������
        pause.enabled = false;  // �Ⱥ��̰�

        slider = GameObject.Find("Slider_img").GetComponent<Image>();   // �����̴� ������
        slider.enabled = false;  // �Ⱥ��̰�

        daynight = GameObject.Find("DayNight_img").GetComponent<Image>(); // ���� ȸ�� ������
        daynight.enabled = false; // �Ⱥ��̰�

        data = LoadIngameData(); // ������ �ε�, ���� ���� ù �����̸� ���⿡ null �� ����.

        DontDestroyOnLoad(GameObject.Find("InitManager")); 
        SceneManager.LoadScene("Main"); // ����ȭ�� �ҷ�����
        Debug.Log("��������"); 
    }

    public void ReturnClick() //   �ٽ��ϱ� ��ư Ŭ����
    {
        BtnBgm.Play(); // ��ư Ŭ�� ��
        gameOver = false;

        back = GameObject.Find("Back_img").GetComponent<Image>();  // ���ư��� ������
        back.enabled = false;  // �Ⱥ��̰�

        pause = GameObject.Find("Pause_img").GetComponent<Image>();  // �Ͻ����� ������
        pause.enabled = false;  // �Ⱥ��̰�

        slider = GameObject.Find("Slider_img").GetComponent<Image>();   // �����̴� ������
        slider.enabled = false;  // �Ⱥ��̰�

        daynight = GameObject.Find("DayNight_img").GetComponent<Image>(); // ���� ȸ�� ������
        daynight.enabled = false; // �Ⱥ��̰�

        data = new SaveData();
        InitManager.SaveIngameData(data);

        DontDestroyOnLoad(GameObject.Find("InitManager"));
        SceneManager.LoadScene("Main"); // ����ȭ�� �ҷ�����
        Debug.Log("��������");
    }

    public void OpenBtnClick() // �����̴� ���� ��ư Ŭ����
    {
        BtnBgm.Play(); // ��ư Ŭ�� ��
        if (closetrigger) // ����������
        {
            closetrigger = false;  // �Ⱥ��̰�
            opentrigger = true; //���̰�
        }
        else if (opentrigger) // ����������������
        {
            opentrigger = false; // �Ⱥ��̰�
            closetrigger = true; //���̰�
        }
        else if (GameObject.Find("Slider_img").transform.localPosition.x > 1000) opentrigger = true; //�����̴� �̹��� �� true���϶� ������
        else closetrigger = true;
    }

    public void BackButton() // ���ư��� ��ư
    {
        BtnBgm.Play(); // ��ư Ŭ�� ��
        if (pos % 10 >= 1) // �� �ȿ� �ִ� ��� �� �ٱ����� ������
        {
            GameObject.Find("PanelRoom").transform.Find(pos.ToString()).gameObject.SetActive(false); 
            pos--;
            GameObject.Find("GSManager").GetComponent<GSManager>().SendMessage("DisplayRoom");
        }
        else if (pos % 10 == 0) // ������  �̵��Ѵ�
        {
            SceneManager.LoadScene("Map");
        }
    }
    IEnumerator SwapColor(Color start, Color end)  // ����
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 0.5f;
            GameObject.Find("PanelMain").GetComponent<Image>().color = Color.Lerp(start, end, t);
            yield return null;
        }
    }

    void Update()
    {
        //�ð� ��� ó��
        if (!gameOver & data != null & GameObject.Find("DayNight_img").GetComponent<Image>().enabled )// ���丮 ���� �Ѿ ������ ������ �����ϱ� ���� �ð��� ��� ����.
        {
            float oneDay = 8f; //���ӽð����� �Ϸ��� ���� (�� ����)
            float t1 = 5f; //gameTIme�� ���� Ÿ�� 5�ʸ��� ���ϴ� �ð���ŭ ������Ŵ(�̼��ڰ� �۾����� ���� ���� �Ǵ� ����)
            float t2 = 120f / oneDay; //����Ÿ�� 5�ʸ��� �����Ǵ� ���ӽð�
           
            dt += Time.deltaTime;
            if (dt >= t1) //dt = 0�ʺ��� ����
            {
                data.gameTime += t2;
                SaveIngameData(data); //����� �ð� ����
                dt = 0f;
            }
            if (data.gameTime > LimitTIme) // LimitTime =2400f�ϱ�, data.gameTIme�� 2400f�� �ʰ��ϸ� �Ǵ°�.
            {
                pos = 130;  //130���� ������ (gameover)
                gameOver = true; //(gameover�� �� Ʈ�簡 �ȴ�.)
                Time.timeScale = 0; // �ð� �帧 ���� 0

                DayBgm.Stop(); // ������ ����
                NightBgm.Stop(); // ������ ���� 
                SceneManager.LoadScene("GameScene");
            }
          
            //�㳷 ȸ���� ǥ�� �� ���� �ݿ�
            nowTime = (data.gameTime + (dt * t2 / t1)) * 180 / 1440f; // �̰��� 180f �� �Ϸ簡 ������ 
            GameObject.Find("DayNight_img").transform.eulerAngles = new Vector3(0, 0, -nowTime + 45);

            if (((int)nowTime) % 180 > 112.5f & data.isDay) // �ð��� �� �Ǿ��µ� ���� ���
            {
                Debug.Log("���� �Ǿ����ϴ�!");
                GameTimeText.text = ("���� �Ǿ����ϴ�!");
                
                data.isDay = false;
                DayNightBgm();
               
                SaveIngameData(data); // ������ ����
                StartCoroutine(SwapColor(GameObject.Find("PanelMain").GetComponent<Image>().color, night));
            }
            else if (((int)nowTime) % 180 <= 112.5f & !data.isDay) //�ð��� �� �Ǿ��µ� ���� ���
            {
                Debug.Log("���� �Ǿ����ϴ�!");
                GameTimeText.text = ("���� �Ǿ����ϴ�!");
               
                data.isDay = true;
                DayNightBgm();

                SaveIngameData(data); //������ ����
                StartCoroutine(SwapColor(GameObject.Find("PanelMain").GetComponent<Image>().color, day));
            }
        }
        
        if (opentrigger) //�����̴� ����
        {
            GameObject go = GameObject.Find("Slider_img"); // �����̴� �̹���
            go.transform.localPosition = new Vector3(go.transform.localPosition.x - 10f, go.transform.localPosition.y);

            if (go.transform.localPosition.x < 713) 
            {
                go.transform.localPosition = new Vector3(713f, go.transform.localPosition.y);
                opentrigger = false;
            }
        }

        if (closetrigger) //�����̴� �ݱ�
        {
            GameObject go = GameObject.Find("Slider_img"); // �����̴� �̹���
            go.transform.localPosition = new Vector3(go.transform.localPosition.x + 10f, go.transform.localPosition.y);

            if (go.transform.localPosition.x > 1122) 
            {
                go.transform.localPosition = new Vector3(1122f, go.transform.localPosition.y);
                closetrigger = false;
            }
        }
    }

    public void ShowTime() //���� �ð� �˷��ִ� �Լ�
    {
        int d = ((int)nowTime / 180) +1;
        int h = (int)nowTime % 180 * 24 / 180;

        Debug.Log(d + "��" + h + "�ð� ��� " + (data.isDay ? "��" : "��") + "�Դϴ�."); // �ܼ�â ���
        GameTimeText.text = (d + "��" + h + "�ð�"+ "\n" + (data.isDay ? "��" : "��") ); // ȭ�鿡 ���
    }

    public void ShowItems() // ������ ������ ǥ���ϴ� �Լ�
    {
        BtnBgm.Play(); // ��ư Ŭ�� ��
        int n = 1;
        foreach (int i in data.getItem) // �������� ������ �� ���� ���� ���� ǥ��
        {
            GameObject slot = GameObject.Find("BagPopup/Image/Slot_" + n++);
            slot.GetComponent<Image>().sprite = Resources.Load("itemIcon/" + items[i], typeof(Sprite)) as Sprite;
            //������ ���������̹Ƿ� Ŭ���� �����ϰ� ��ư ����� Ȱ��ȭ ���ش�.

            if (i == 3 || i == 2 || i == 1) slot.GetComponent<Button>().enabled = true;
            else slot.GetComponent<Button>().enabled = false;
        }
        while (n <=4) //�ܿ� �� ���� ó��
        {
            GameObject slot = GameObject.Find("BagPopup/Image/Slot_" + n++);
            slot.GetComponent<Image>().sprite = Resources.Load("itemIcon/noitem", typeof(Sprite)) as Sprite;
            slot.GetComponent<Button>().enabled = false;
        }
    }
   
    public void itemClick(Button btn) // ���������� Ŭ������ �� �ൿ�� �����ϴ� �Լ�
    {
        BtnBgm.Play(); // ��ư Ŭ�� ��
        if (data.getItem[int.Parse(btn.name.Substring(5,1)) -1]==3)//������ ���
        {
            if (pos == 81 /* �ξ��� �濡 �ش��ϴ� ��ȣ*/ & !data.isDay    /*true ��� ������ üũ�ϴ� ���ǽ��� �ִ´�*/)
            {
                firebgm.Play(); // ���� ��� �� ȿ����
                Debug.Log("������ �Ѽ� ���� ���� ��й�ȣ�� �ִ�!!");// �˾� �޽����� ��ü
                SendMessage("ShowMsg", "������ �Ѽ� ���� ����\n��й�ȣ�� �ִ�!!");

                GameObject.Find("GSManager").GetComponent<GSManager>().SendMessage("ShowPwd");
            }
            else Debug.Log("�㿡 �ξ��� ������ ����ؾ� �� �� ����!"); //�˾� �޽����� ��ü
            SendMessage("ShowMsg", "�㿡 �ξ��� ������\n����ؾ� �� �� ����!");
        }
        else if (data.getItem[int.Parse(btn.name.Substring(5, 1)) - 1] == 2)//������ ���
        {
            if (pos == 111 /* �����忡 �ش��ϴ� ��ȣ*/)
            {
                oilbgm.Play(); //�⸧ ������ ȿ����
                Debug.Log("�⸧�� ä������!");// �˾� �޽����� ��ü
                SendMessage("ShowMsg", "�⸧�� ä������!");

                GameObject.Find("GSManager").GetComponent<GSManager>().SendMessage("ShowOil");
            }
            else 
                Debug.Log("�⸧�� ä���ߵ� �� ����.."); //�˾� �޽����� ��ü
        }
        else if (data.getItem[int.Parse(btn.name.Substring(5, 1)) - 1] == 1)// ���������� ����2 �� ���
        {
            if (pos == 111 /* �����忡 �ش��ϴ� ��ȣ*/& data.getItem.Contains(4)/*  �ؾ������� ������ �ִٴ� ��ȣ*/ & data.getItem.Contains(2)/*  ������ ������ �ִٴ� ��ȣ*/ & (data.oilFull)/*  ������ ä���ٴ� ���*/)
            {
                int d = ((int)nowTime / 180) + 1;
                int h = (int)nowTime % 180 * 24 / 180;

                pos = 140;  //140���� ������ (gameclear)
                Time.timeScale = 0; // �ð� �帧 ���� 0

                DayBgm.Stop(); // ������ ����
                NightBgm.Stop(); // ������ ���� 

                Debug.Log("������ �����ߴ�!!");// �˾� �޽����� ��ü
                Debug.Log(d + "��" + h + "�ð� ��� " + (data.isDay ? "��" : "��") + "�Դϴ�."); // �ܼ�â ���
                SceneManager.LoadScene("GameScene");
            }
            else Debug.Log("�ٸ� �����۵鵵 ����!"); //�˾� �޽����� ��ü
        }
           // 111 ���濡�� �������, �⸧ ä���, ���濡�� ����2 ������ ����
    }

    SaveData LoadIngameData() // ������ �ε� �Լ�
    {
        string path = Application.persistentDataPath + "/IngameData.dat";

        if (!File.Exists(path)) return null; //ù �����ؼ� �����Ͱ� ���� ��� null�� �����Ѵ�

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);

        SaveData data = (SaveData)formatter.Deserialize(file);
        file.Close();

        return data;
    }

    public static void SaveIngameData(SaveData data) // ������ �����Լ�
    {
        String path = Application.persistentDataPath + "/IngameData.dat";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Create);

        formatter.Serialize(file, data);
        file.Close();
    }
}
