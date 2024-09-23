using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GSManager : MonoBehaviour
{
    InitManager init;// 불러오기

    int duckClick = 0; //오리액자 클릭

    int step = 0;   // 비밀번호 클릭
    int cnt = 0; // 비밀번호 자릿 수 제한

    //아이템 
    public GameObject key;
    public GameObject key2;
    public GameObject oil;
    public GameObject fire;
    public GameObject map;

    //효과음

    //버튼 클릭시 효과음
    public AudioSource BtnBgm; // 버튼 클릭 시 효과음

    //아이템 얻는 효과음
    public AudioSource ItemGet; // 아이템 얻을때 효과음

    //비번, 금고
    public AudioSource PassBtn; // 비번 누를 때 효과음
    public AudioSource OkPass;  // 금고 비번 맞을 때 효과음
    public AudioSource NotPass; // 비번 틀릴 때 효과음
    public AudioSource DoorlockPass; // 도어락 비번 맞을 때 효과음

    // 문 열리는, 잠겨있는
    public AudioSource keyBgm; //열쇠 돌리는 효과음
    public AudioSource keyDrop; //열쇠 떨어지는 효과음 
    public AudioSource OpenDoor; // 문 열리는 효과음
    public AudioSource LockDoor; // 문 잠겨있는 효과음

    // 액자 터치시
    public AudioSource ducktouch; // 오리액자 터치시 효과음

    //텍스트
    public Text stime; // 텍스트

    //이미지
    public GameObject pwdImg; //  부엉이방 -9382  비밀번호 이미지
    public GameObject mapImg; //  선착장 -지도아이콘 이미지
    public GameObject oilImg; // 선착장 - 오일 아이콘 이미지

    void Start()
    {
        ApplicationChrome.statusBarState = ApplicationChrome.States.Hidden;
   
        init = GameObject.Find("InitManager").GetComponent<InitManager>();  

        init.slider.enabled = true; //슬라이더
        init.daynight.enabled = true; // 낮 밤 구현 시계
         
        init.pause.enabled = true; //  일시정지
        init.back.enabled = true; //  돌아가기

        if (init.gameOver)
        {
            init.back = GameObject.Find("Back_img").GetComponent<Image>();  // 돌아가기 아이콘
            init.back.enabled = false;  // 안보이게

            init.pause = GameObject.Find("Pause_img").GetComponent<Image>();  // 일시정지 아이콘
            init.pause.enabled = false;  // 안보이게

            init.slider = GameObject.Find("Slider_img").GetComponent<Image>();   // 슬라이더 아이콘
            init.slider.enabled = false;  // 안보이게

            init.daynight = GameObject.Find("DayNight_img").GetComponent<Image>(); // 낮밤 회전 아이콘
            init.daynight.enabled = false; // 안보이게

            // 밤낮 및 가방 유아이 끄는 코드
        }

        // 아이템 안보이게 설정
        if (init.data.getItem.Contains(1)) key2.SetActive(false);
        if (init.data.getItem.Contains(2)) oil.SetActive(false);
        if (init.data.getItem.Contains(3)) fire.SetActive(false);
        if (init.data.getItem.Contains(4)) map.SetActive(false);

        // 밤낮 배경 초기화 코드 
        if (init.data.isDay) GameObject.Find("PanelMain").GetComponent<Image>().color = init.day;
        else GameObject.Find("PanelMain").GetComponent<Image>().color = init.night;
        DisplayRoom();
    }

    void Update()
    {
    }

    // 창고 비번 : 2952 , 순차적 이여야 함
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

            case 10: //별
            case 11: //샵
                cnt--;
                if (step == 4 & cnt == 4)
                {
                    DoorlockPass.Play(); //도어락 맞을때 효과음, 팝업
                    init.pos =121; //내부이동
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

    // 곰방 금고 비번 : 3469  , 순서 상관 없음
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

            case 10: //확인
                cnt--;
                if (step == 4 & cnt==4) 
                {
                    OkPass.Play();
                    init.pos = 23; //금고안
                    StartCoroutine(DelayedLoadScene());
                    Debug.Log("okay"); //팝업
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

    // 부엉이방 비번 : 9342 , 순차적 이여야 함
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

            case 10: //별
            case 11: //샵
                cnt--;
                if (step == 4 & cnt == 4)
                {
                    DoorlockPass.Play(); //도어락 맞을때 효과음, 팝업
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
        keyBgm.Play(); // 열쇠돌리는 소리
        if (init.data.getItem.Contains(0) & init.pos == 21) // 0번 아이템, 즉 열쇠를 이미 가지고 있는 경우 그리고 21번 방인 경우
        {
            GameObject.Find("PanelRoom").transform.Find(init.pos.ToString()).gameObject.SetActive(false);
            init.pos++;
            DisplayRoom();
            OpenDoor.Play(); // 방문여는소리

            Debug.Log("곰방 문을 열었습니다!"); //추후 대화창 처리
            init.SendMessage("ShowMsg", "곰방 문을 열었습니다!");
        }
        else // 0번 아이템, 즉 열쇠를 아직 획득 못한 경우
        {
            LockDoor.Play(); // 닫혀 있는소리
            Debug.Log("열쇠를 획득해 오세요!"); //추후 대화창 처리 
            init.SendMessage("ShowMsg", "열쇠를 획득해 오세요!");
        }
    }
    public void TouchDuck() // 오리액자 3번 누를시 열쇠가 떨어진다
    {
        ducktouch.Play(); // 오리액자 터치 효과음
        StartCoroutine(ShakeDuck());
        duckClick++;
        if (duckClick >= 3 & !init.data.getItem.Contains(0))
        {
            keyDrop.Play(); // 열쇠 떨어지는 소리
            Debug.Log("액자 뒤에서 열쇠가 떨어졌다!!");
            init.SendMessage("ShowMsg", "액자 뒤에서 열쇠가 떨어졌다!!");
            key.SetActive(true);
        }
    }
    IEnumerator ShakeDuck() // 오리 액자 누를시, 흔들린다.
    {
        GameObject.Find("Duck_img").transform.eulerAngles = new Vector3(0, 0, 5);
        yield return new WaitForSeconds(0.05f);
        GameObject.Find("Duck_img").transform.eulerAngles = new Vector3(0, 0, -5);
        yield return new WaitForSeconds(0.05f);
        GameObject.Find("Duck_img").transform.eulerAngles = new Vector3(0, 0, 5);
        yield return new WaitForSeconds(0.05f);
        GameObject.Find("Duck_img").transform.eulerAngles = new Vector3(0, 0, 0);
    }
    public void ShowPwd() // 비밀번호
    {
        pwdImg.SetActive(true);
    }

    public void ShowOil() // 기름
    {
        oilImg.SetActive(true);
        init.data.oilFull = true;
        InitManager.SaveIngameData(init.data);
    }

    public void DisplayRoom() 
    {
        GameObject.Find("PanelRoom").transform.Find(init.pos.ToString()).gameObject.SetActive(true);

        if (init.pos == 81)   // 81번방 
        {
            pwdImg.SetActive(false); //  비밀번호 이미지 안보이게 설정
        }
        else if (init.pos == 111) // 111번방 
        {
            if (init.data.oilFull) // 기름이 가득 찰 경우
            {
                oilImg.SetActive(true); // 보이게
            }
            else // 아닐경우
            {
                oilImg.SetActive(false); // 안보이게
            }

            if (init.data.getItem.Contains(4)) // 해양지도를 갖고 있을 경우
            {
                mapImg.SetActive(true); // 보이게
            }
            else // 아닐경우
            {
                mapImg.SetActive(false); // 안보이게
            }
        }
        else if (init.pos == 140)
        {
            int d = ((int)init.nowTime / 180) + 1;
            int h = (int)init.nowTime % 180 * 24 / 180;

            Debug.Log(d + "일" + h + "시간 경과 "); // 콘솔창 출력
            stime.text = (d + "일" + " " + h + "시간"); // 화면에 출력(성공시)
        }
    }
 
    public void owlPwd() // 80 부엉이 외부 
    {
        if (init.data.isDay || init.pos == 80) // 저녁에 부엉이방
        {
        }
        else
        {
            LockDoor.Play(); // 닫혀 있는소리
            Debug.Log("낮에 들어 갈 수 있습니다!");
            init.SendMessage("ShowMsg", "낮에 들어 갈 수 있습니다!");
        }
        key.SetActive(false);
    }

    public void MainButtonClick() //  게임 오버 후 메인으로 가기 버튼 클릭시
    {
        BtnBgm.Play(); // 버튼 클릭 시
        init.gameOver = false;

        init.back = GameObject.Find("Back_img").GetComponent<Image>();  // 돌아가기 아이콘
        init.back.enabled = false;  // 안보이게

        init.pause = GameObject.Find("Pause_img").GetComponent<Image>();  // 일시정지 아이콘
        init.pause.enabled = false;  // 안보이게

        init.slider = GameObject.Find("Slider_img").GetComponent<Image>();   // 슬라이더 아이콘
        init.slider.enabled = false;  // 안보이게

        init.daynight = GameObject.Find("DayNight_img").GetComponent<Image>(); // 낮밤 회전 아이콘
        init.daynight.enabled = false; // 안보이게
        
        init.data = new SaveData();
        InitManager.SaveIngameData(init.data);

        // init.data =  init.LoadIngameData(); // 데이터 로딩, 만약 게임 첫 실행이면 여기에 null 이 들어간다.

        DontDestroyOnLoad(GameObject.Find("InitManager"));
        SceneManager.LoadScene("Main"); // 메인화면
        Debug.Log("메인으로");
    }

    public void GetKey() //열쇠 아이템을 얻게 되는 함수, 아이템 획득
    {
        if(init.data.getItem.Contains(0)) // 0번 아이템, 즉 열쇠를 이미 가지고 있는 경우
        {
            Debug.Log("이미 획득한 물건입니다!"); //추후 대화창 처리
            init.SendMessage("ShowMsg", "이미 획득한 물건입니다!");
        }
        else // 0번 아이템, 즉 열쇠를 아직 획득 못한 경우
        {
            ItemGet.Play(); // 아이템 얻는 효과음
            Debug.Log("열쇠1을 획득하였습니다! 가방을 확인해 보세요!"); //추후 대화창 처리
            init.SendMessage("ShowMsg", "열쇠1을 획득하였습니다!\n가방을 확인해 보세요!");

            init.data.getItem.Add(0); //0번 아이템. 즉 열쇠 인벤토리에 추가
            InitManager.SaveIngameData(init.data); //데이터 저장
        }
        key.SetActive(false);
    }

    public void GetKey2() //열쇠 아이템을 얻게 되는 함수
    {
        if (init.data.getItem.Contains(1)) // 1번 아이템, 즉 열쇠2를 이미 가지고 있는 경우
        {
            Debug.Log("이미 획득한 물건입니다!"); //추후 대화창 처리
            init.SendMessage("ShowMsg", "이미 획득한 물건입니다!");
        }
        else // 1번 아이템, 즉 열쇠2를 아직 획득 못한 경우
        {
            ItemGet.Play(); // 아이템 얻는 효과음
            Debug.Log("열쇠2를 획득하였습니다! 가방을 확인해 보세요!"); //추후 대화창 처리
            init.SendMessage("ShowMsg", "열쇠2를 획득하였습니다!\n가방을 확인해 보세요!");

            init.data.getItem.Add(1); //1번 아이템. 즉 열쇠2 인벤토리에 추가
            InitManager.SaveIngameData(init.data); //데이터 저장
        }
        key2.SetActive(false);
    }

    public void GetOil() //기름 아이템을 얻게 되는 함수
    {
        if (init.data.getItem.Contains(2)) // 2번 아이템, 즉 기름을 이미 가지고 있는 경우
        {
            Debug.Log("이미 획득한 물건입니다!"); //추후 대화창 처리
            init.SendMessage("ShowMsg", "이미 획득한 물건입니다!");
        }
        else // 2번 아이템, 즉 기름를 아직 획득 못한 경우
        {
            ItemGet.Play(); // 아이템 얻는 효과음
            Debug.Log("기름을 획득하였습니다! 가방을 확인해 보세요!"); //추후 대화창 처리
            init.SendMessage("ShowMsg", "기름을 획득하였습니다!\n가방을 확인해 보세요!");

            init.data.getItem.Add(2); //2번 아이템. 즉 기름 인벤토리에 추가
            InitManager.SaveIngameData(init.data); //데이터 저장
        }
        oil.SetActive(false);
    }

    public void GetFire() //성냥 아이템을 얻게 되는 함수
    {
        if (init.data.getItem.Contains(3)) // 3번 아이템, 즉 성냥을 이미 가지고 있는 경우
        {
            fire.SetActive(false);
            InitManager.SaveIngameData(init.data); //  지도 얻고 난 뒤에도 데이터 저장
            Debug.Log("이미 획득한 물건입니다!"); //추후 대화창 처리
            init.SendMessage("ShowMsg", "이미 획득한 물건입니다!");
        }
        else // 3번 아이템, 즉 성냥을 아직 획득 못한 경우
        {
            ItemGet.Play(); // 아이템 얻는 효과음
            Debug.Log("성냥을 획득하였습니다! 가방을 확인해 보세요!"); //추후 대화창 처리
            init.SendMessage("ShowMsg", "성냥을 획득하였습니다!\n가방을 확인해 보세요!");

            init.data.getItem.Add(3); //3번 아이템. 즉 성냥 인벤토리에 추가
            InitManager.SaveIngameData(init.data); //데이터 저장
        }
        fire.SetActive(false);
        InitManager.SaveIngameData(init.data); //  지도 얻고 난 뒤에도 데이터 저장
    }

    public void GetMap() //해양 지도를 얻게 되는 함수
    {
        fire.SetActive(false);
        InitManager.SaveIngameData(init.data); //  지도 얻고 난 뒤에도 데이터 저장

        if (init.data.getItem.Contains(4)) // 4번 아이템, 즉 지도를 이미 가지고 있는 경우
        {
          
            Debug.Log("이미 획득한 물건입니다!"); //추후 대화창 처리
            init.SendMessage("ShowMsg", "이미 획득한 물건입니다!");
        }
        else if(init.data.getItem.Contains(3))//4번 아이템, 즉 해양지도를 아직 획득 못한 경우 , 성냥 가지고 있는 경우
        {
            ItemGet.Play(); // 아이템 얻는 효과음
            Debug.Log("해양지도를 획득하였습니다! 가방을 확인해 보세요!"); //추후 대화창 처리
            init.SendMessage("ShowMsg", "해양지도를 획득하였습니다!\n가방을 확인해 보세요!");

            init.data.getItem.Add(4); //3번 아이템. 즉 해양지도 인벤토리에 추가
            init.data.getItem.Remove(3); //성냥 삭제

            InitManager.SaveIngameData(init.data); //데이터 저장
        }
        else
        {
            Debug.Log("성냥이 필요 할 것 같네요.."); //추후 대화창 처리
            init.SendMessage("ShowMsg", "성냥이 필요 할 것 같네요..");
        }
        fire.SetActive(false);
        map.SetActive(false);
    }

    public void DoorClickButton() // 다음 방으로 넘어가기
    {
        BtnBgm.Play(); // 버튼 클릭 시
        if (init.data.isDay || init.pos == 80 || init.pos == 100 || init.pos == 110 || init.pos == 120) // 저녁에 부엉이방, 공원, 선착장, 창고
        {
            GameObject.Find("PanelRoom").transform.Find(init.pos.ToString()).gameObject.SetActive(false);
            init.pos++;
            DisplayRoom();
        }
        else
        {
            Debug.Log("낮에 들어 갈 수 있습니다..");
            init.SendMessage("ShowMsg", "낮에 들어 갈 수 있습니다..");
        }
    }

    public void RoomBackButton() // 돌아가기 버튼
    {
        BtnBgm.Play(); // 버튼 클릭 시
        if (init.pos % 10 >= 1) // 방 안에 있는 경우 방 바깥으로 나간다
        {
            GameObject.Find("PanelRoom").transform.Find(init.pos.ToString()).gameObject.SetActive(false);
            init.pos--;
            GameObject.Find("GSManager").GetComponent<GSManager>().SendMessage("DisplayRoom");
        }
    }
}
