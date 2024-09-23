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
public class SaveData // 데이터 저장
{
    public float gameTime; // 경과한 게임 시간,  현실에서 5초마다 저장되는 데이터
    public bool isDay; //지금 낮이면 true, 밤이면 false
    public List<int> getItem; //열쇠 = 0, 열쇠(2) = 1, 기름 = 2, 성냥 = 3, 해양지도 = 4 
    public bool oilFull; // 기름 찼는지 보기 위해서
    public SaveData() // 데이터 저장
    {
        gameTime = 0f;
        isDay = true;
        getItem = new List<int>();
        oilFull = false; 
    }
}
public class InitManager : MonoBehaviour
{
    public bool gameOver = false; // false값

    public string[] items = new string[5] { "열쇠", "열쇠(2)", "기름", "성냥", "해양지도" }; //아이템 리소스 이름을 연결하는 배열

    float dt = 0f;
    float LimitTIme = 7200f; //제한시간-> 8분을 5일 계산하면 = 7200
    public float nowTime; // 경과된 게임 시간 (분)

    //텍스트
    public Text GameTimeText; // 게임 시간 화면에 표현

    public Color day; //낮일 때 색상
    public Color night; // 밤일 때 색상
    public SaveData data;  // 게임 종료 되어도 남아있어야 하는 저장이 필요한 데이터
    public int pos; // 나의 현재위치

    // 이미지 및 버튼
    public Image slider; // 슬라이더 바 이미지
    public Image daynight; // 낮 밤 구현 회전 이미지
    public Button msgpop; // 공통 메시지 팝업
    public Image pause; // 일시정지 이미지
    public Image back; // 돌아가기 이미지

    bool opentrigger = false; 
    bool closetrigger = false;

    public Button timepop;// 성공시간 팝업창


    // 배경음
    public AudioSource DayBgm; // 낮 배경음
    public AudioSource NightBgm; // 밤 배경음

    //효과음
    //버튼 클릭시 효과음
    public AudioSource BtnBgm; // 버튼 클릭 시 효과음


    // 성냥 불 붙이기
    public AudioSource firebgm;// 성냥 불 사용할때

    //기름넣기
    public AudioSource oilbgm; // 오일 열릴때 효과음

    void Start() // 시작할때 
    {
        ApplicationChrome.statusBarState = ApplicationChrome.States.Hidden;

        back = GameObject.Find("Back_img").GetComponent<Image>();  // 돌아가기 아이콘
        back.enabled = false;  // 안보이게

        pause = GameObject.Find("Pause_img").GetComponent<Image>();  // 일시정지 아이콘
        pause.enabled = false;  // 안보이게

        slider = GameObject.Find("Slider_img").GetComponent<Image>();   // 슬라이더 아이콘
        slider.enabled = false;  // 안보이게

        daynight = GameObject.Find("DayNight_img").GetComponent<Image>(); // 낮밤 회전 아이콘
        daynight.enabled = false; // 안보이게

        data = LoadIngameData(); // 데이터 로딩, 만약 게임 첫 실행이면 여기에 null 이 들어간다.

        DontDestroyOnLoad(GameObject.Find("InitManager"));
        SceneManager.LoadScene("Main"); // 메인화면 불러오기

      // Debug.Log(data.gameTime + "/" + LimitTIme); // 확인용
    }

    public void ShowMsg(string str) // 메시지 팝업 띄우는 함수
    {
        msgpop.transform.Find("Text").GetComponent<Text>().text = str;
        msgpop.gameObject.SetActive(true);
    }

    public void TimeMsg(string str) // 메시지 팝업 띄우는 함수
    {
        timepop.transform.Find("Text").GetComponent<Text>().text = str;
        timepop.gameObject.SetActive(true);
    }

    public void DayNightBgm() // 낮 밤 전환 배경음
    {
        if (data.isDay) // 낮
        {
            DayBgm.Play(); //비지엠 재생
            NightBgm.Stop(); //비지엠 멈춤
        }
        else // 밤
        { 
            DayBgm.Stop(); //비지엠 재생
            NightBgm.Play(); //비지엠 멈춤
        }
    }

    public void MapButtonClick() // 맵 아이콘 버튼 클릭시
    {
        BtnBgm.Play(); // 버튼 클릭 시
        SceneManager.LoadScene("Map"); // 지도 불러오기
    }

    public void MainButtonClick() // 메인으로 가기 버튼 클릭시
    {
        BtnBgm.Play(); // 버튼 클릭 시

        back = GameObject.Find("Back_img").GetComponent<Image>();  // 돌아가기 아이콘
        back.enabled = false;  // 안보이게

        pause = GameObject.Find("Pause_img").GetComponent<Image>();  // 일시정지 아이콘
        pause.enabled = false;  // 안보이게

        slider = GameObject.Find("Slider_img").GetComponent<Image>();   // 슬라이더 아이콘
        slider.enabled = false;  // 안보이게

        daynight = GameObject.Find("DayNight_img").GetComponent<Image>(); // 낮밤 회전 아이콘
        daynight.enabled = false; // 안보이게

        data = LoadIngameData(); // 데이터 로딩, 만약 게임 첫 실행이면 여기에 null 이 들어간다.

        DontDestroyOnLoad(GameObject.Find("InitManager")); 
        SceneManager.LoadScene("Main"); // 메인화면 불러오기
        Debug.Log("메인으로"); 
    }

    public void ReturnClick() //   다시하기 버튼 클릭시
    {
        BtnBgm.Play(); // 버튼 클릭 시
        gameOver = false;

        back = GameObject.Find("Back_img").GetComponent<Image>();  // 돌아가기 아이콘
        back.enabled = false;  // 안보이게

        pause = GameObject.Find("Pause_img").GetComponent<Image>();  // 일시정지 아이콘
        pause.enabled = false;  // 안보이게

        slider = GameObject.Find("Slider_img").GetComponent<Image>();   // 슬라이더 아이콘
        slider.enabled = false;  // 안보이게

        daynight = GameObject.Find("DayNight_img").GetComponent<Image>(); // 낮밤 회전 아이콘
        daynight.enabled = false; // 안보이게

        data = new SaveData();
        InitManager.SaveIngameData(data);

        DontDestroyOnLoad(GameObject.Find("InitManager"));
        SceneManager.LoadScene("Main"); // 메인화면 불러오기
        Debug.Log("메인으로");
    }

    public void OpenBtnClick() // 슬라이더 열기 버튼 클릭시
    {
        BtnBgm.Play(); // 버튼 클릭 시
        if (closetrigger) // 닫혀있을때
        {
            closetrigger = false;  // 안보이게
            opentrigger = true; //보이게
        }
        else if (opentrigger) // 열려있을때있을때
        {
            opentrigger = false; // 안보이게
            closetrigger = true; //보이게
        }
        else if (GameObject.Find("Slider_img").transform.localPosition.x > 1000) opentrigger = true; //슬라이더 이미지 및 true값일때 움직임
        else closetrigger = true;
    }

    public void BackButton() // 돌아가기 버튼
    {
        BtnBgm.Play(); // 버튼 클릭 시
        if (pos % 10 >= 1) // 방 안에 있는 경우 방 바깥으로 나간다
        {
            GameObject.Find("PanelRoom").transform.Find(pos.ToString()).gameObject.SetActive(false); 
            pos--;
            GameObject.Find("GSManager").GetComponent<GSManager>().SendMessage("DisplayRoom");
        }
        else if (pos % 10 == 0) // 지도로  이동한다
        {
            SceneManager.LoadScene("Map");
        }
    }
    IEnumerator SwapColor(Color start, Color end)  // 색상
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
        //시간 경과 처리
        if (!gameOver & data != null & GameObject.Find("DayNight_img").GetComponent<Image>().enabled )// 스토리 씬이 넘어가 본격적 게임이 시작하기 전에 시간은 상관 없다.
        {
            float oneDay = 8f; //게임시간으로 하루의 길이 (분 단위)
            float t1 = 5f; //gameTIme을 리얼 타임 5초마다 원하는 시간만큼 증가시킴(이숫자가 작아지면 자주 저장 되니 주의)
            float t2 = 120f / oneDay; //리얼타임 5초마다 증가되는 게임시간
           
            dt += Time.deltaTime;
            if (dt >= t1) //dt = 0초부터 시작
            {
                data.gameTime += t2;
                SaveIngameData(data); //경과된 시간 저장
                dt = 0f;
            }
            if (data.gameTime > LimitTIme) // LimitTime =2400f니깐, data.gameTIme이 2400f를 초과하면 되는것.
            {
                pos = 130;  //130방이 켜지고 (gameover)
                gameOver = true; //(gameover값 이 트루가 된다.)
                Time.timeScale = 0; // 시간 흐름 비율 0

                DayBgm.Stop(); // 비지엠 멈춤
                NightBgm.Stop(); // 비지엠 멈춤 
                SceneManager.LoadScene("GameScene");
            }
          
            //밤낮 회전판 표시 및 배경색 반영
            nowTime = (data.gameTime + (dt * t2 / t1)) * 180 / 1440f; // 이값이 180f 면 하루가 지난거 
            GameObject.Find("DayNight_img").transform.eulerAngles = new Vector3(0, 0, -nowTime + 45);

            if (((int)nowTime) % 180 > 112.5f & data.isDay) // 시간이 밤 되었는데 낮인 경우
            {
                Debug.Log("밤이 되었습니다!");
                GameTimeText.text = ("밤이 되었습니다!");
                
                data.isDay = false;
                DayNightBgm();
               
                SaveIngameData(data); // 데이터 저장
                StartCoroutine(SwapColor(GameObject.Find("PanelMain").GetComponent<Image>().color, night));
            }
            else if (((int)nowTime) % 180 <= 112.5f & !data.isDay) //시간이 낮 되었는데 밤인 경우
            {
                Debug.Log("낮이 되었습니다!");
                GameTimeText.text = ("낮이 되었습니다!");
               
                data.isDay = true;
                DayNightBgm();

                SaveIngameData(data); //데이터 저장
                StartCoroutine(SwapColor(GameObject.Find("PanelMain").GetComponent<Image>().color, day));
            }
        }
        
        if (opentrigger) //슬라이더 열기
        {
            GameObject go = GameObject.Find("Slider_img"); // 슬라이더 이미지
            go.transform.localPosition = new Vector3(go.transform.localPosition.x - 10f, go.transform.localPosition.y);

            if (go.transform.localPosition.x < 713) 
            {
                go.transform.localPosition = new Vector3(713f, go.transform.localPosition.y);
                opentrigger = false;
            }
        }

        if (closetrigger) //슬라이더 닫기
        {
            GameObject go = GameObject.Find("Slider_img"); // 슬라이더 이미지
            go.transform.localPosition = new Vector3(go.transform.localPosition.x + 10f, go.transform.localPosition.y);

            if (go.transform.localPosition.x > 1122) 
            {
                go.transform.localPosition = new Vector3(1122f, go.transform.localPosition.y);
                closetrigger = false;
            }
        }
    }

    public void ShowTime() //현재 시간 알려주는 함수
    {
        int d = ((int)nowTime / 180) +1;
        int h = (int)nowTime % 180 * 24 / 180;

        Debug.Log(d + "일" + h + "시간 경과 " + (data.isDay ? "낮" : "밤") + "입니다."); // 콘솔창 출력
        GameTimeText.text = (d + "일" + h + "시간"+ "\n" + (data.isDay ? "낮" : "밤") ); // 화면에 출력
    }

    public void ShowItems() // 아이템 가방을 표현하는 함수
    {
        BtnBgm.Play(); // 버튼 클릭 시
        int n = 1;
        foreach (int i in data.getItem) // 아이템이 있으면 앞 슬롯 에서 부터 표시
        {
            GameObject slot = GameObject.Find("BagPopup/Image/Slot_" + n++);
            slot.GetComponent<Image>().sprite = Resources.Load("itemIcon/" + items[i], typeof(Sprite)) as Sprite;
            //성냥은 사용아이템이므로 클릭이 가능하게 버튼 기능을 활성화 해준다.

            if (i == 3 || i == 2 || i == 1) slot.GetComponent<Button>().enabled = true;
            else slot.GetComponent<Button>().enabled = false;
        }
        while (n <=4) //잔여 빈 슬롯 처리
        {
            GameObject slot = GameObject.Find("BagPopup/Image/Slot_" + n++);
            slot.GetComponent<Image>().sprite = Resources.Load("itemIcon/noitem", typeof(Sprite)) as Sprite;
            slot.GetComponent<Button>().enabled = false;
        }
    }
   
    public void itemClick(Button btn) // 사용아이템을 클릭했을 때 행동을 지정하는 함수
    {
        BtnBgm.Play(); // 버튼 클릭 시
        if (data.getItem[int.Parse(btn.name.Substring(5,1)) -1]==3)//성냥인 경우
        {
            if (pos == 81 /* 부엉이 방에 해당하는 번호*/ & !data.isDay    /*true 대신 밤인지 체크하는 조건식을 넣는다*/)
            {
                firebgm.Play(); // 성냥 사용 시 효과음
                Debug.Log("성냥을 켜서 벽을 보니 비밀번호가 있다!!");// 팝업 메시지로 대체
                SendMessage("ShowMsg", "성냥을 켜서 벽을 보니\n비밀번호가 있다!!");

                GameObject.Find("GSManager").GetComponent<GSManager>().SendMessage("ShowPwd");
            }
            else Debug.Log("밤에 부엉이 집에서 사용해야 될 것 같아!"); //팝업 메시지로 대체
            SendMessage("ShowMsg", "밤에 부엉이 집에서\n사용해야 될 것 같아!");
        }
        else if (data.getItem[int.Parse(btn.name.Substring(5, 1)) - 1] == 2)//오일인 경우
        {
            if (pos == 111 /* 선착장에 해당하는 번호*/)
            {
                oilbgm.Play(); //기름 따르는 효과음
                Debug.Log("기름이 채워졌다!");// 팝업 메시지로 대체
                SendMessage("ShowMsg", "기름이 채워졌다!");

                GameObject.Find("GSManager").GetComponent<GSManager>().SendMessage("ShowOil");
            }
            else 
                Debug.Log("기름을 채워야될 것 같아.."); //팝업 메시지로 대체
        }
        else if (data.getItem[int.Parse(btn.name.Substring(5, 1)) - 1] == 1)// 나가기위한 열쇠2 인 경우
        {
            if (pos == 111 /* 선착장에 해당하는 번호*/& data.getItem.Contains(4)/*  해양지도를 가지고 있다는 번호*/ & data.getItem.Contains(2)/*  오일을 가지고 있다는 번호*/ & (data.oilFull)/*  오일을 채웠다는 경우*/)
            {
                int d = ((int)nowTime / 180) + 1;
                int h = (int)nowTime % 180 * 24 / 180;

                pos = 140;  //140방이 켜지고 (gameclear)
                Time.timeScale = 0; // 시간 흐름 비율 0

                DayBgm.Stop(); // 비지엠 멈춤
                NightBgm.Stop(); // 비지엠 멈춤 

                Debug.Log("게임을 성공했다!!");// 팝업 메시지로 대체
                Debug.Log(d + "일" + h + "시간 경과 " + (data.isDay ? "낮" : "밤") + "입니다."); // 콘솔창 출력
                SceneManager.LoadScene("GameScene");
            }
            else Debug.Log("다른 아이템들도 얻어보자!"); //팝업 메시지로 대체
        }
           // 111 번방에서 지도얻고, 기름 채우고, 가방에서 열쇠2 누르면 성공
    }

    SaveData LoadIngameData() // 데이터 로딩 함수
    {
        string path = Application.persistentDataPath + "/IngameData.dat";

        if (!File.Exists(path)) return null; //첫 실행해서 데이터가 없는 경우 null을 리턴한다

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);

        SaveData data = (SaveData)formatter.Deserialize(file);
        file.Close();

        return data;
    }

    public static void SaveIngameData(SaveData data) // 데이터 저장함수
    {
        String path = Application.persistentDataPath + "/IngameData.dat";

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Create);

        formatter.Serialize(file, data);
        file.Close();
    }
}
