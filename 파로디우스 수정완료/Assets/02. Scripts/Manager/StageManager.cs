using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public ObjPoolingMgr objManager;

    public GameObject playerTaco;    //타코 플레이어 게임오브젝트
    public GameObject playerPantarou;//펭타로 플레이어 게임오브젝트
    public Vector3 startPos;

    public GameObject pantarouItemMenu;
    public GameObject tacoItemMenu;

    public string playerName; //선택한 플레이어의 이름
    public float timer;     //시간을 담을 변수
    public Text textTimer;  //시간을 표시할 Text 오브젝트
    int second;  //초 단위 변수
    int minute;  //분 단위 변수

    public int playerHp;   //플레이어 체력

    public Image imagePantarouHp1;   //펭타로 HP 이미지 1
    public Image imagePantarouHp2;   //펭타로 HP 이미지 2
    public Image imageTacoHp1;   //타코 HP 이미지 1
    public Image imageTacoHp2;   //타코 HP 이미지 2
    public Image imageItemMenuPantarou;  //펭타로 아이템메뉴UI
    public Image imageItemMenuTaco;  //타코 아이템메뉴UI

    public int score;     //현재 점수를 담을 변수
    public int bestScore; //베스트 점수를 담을 변수
    public Text textScore;   //현재 점수를 표시할 텍스트
    public Text TextBestScore;//베스트 점수를 표시할 텍스트

    public Text textGameOver; //게임오버 시 띄울 게임오버 텍스트

    private void Awake()
    {
        if(GameManager.instance.playerName == null)
        {
            startPos = new Vector3(-4, 0, 0);
            Instantiate(playerPantarou, startPos, Quaternion.identity);
        }
        else
        { return; }

    }
    private void OnEnable()
    {
        Time.timeScale = 1;
        ScoreLoad();
        PlayerStateSetting();  //플레이어 현재 상태 확인
        TimerReset();   //화면에 표시된 타이머 텍스트 관련 변수 초기화
        MakePlayerCharacter();  //선택된 플레이어 생성 함수
        ItemMenuSetting();   //화면 하단에 넣을 아이템메뉴UI 초기화 함수
    }

    void ScoreLoad()
    {
        if (PlayerPrefs.HasKey("HiScore"))
        {
            TextBestScore.text = PlayerPrefs.GetInt("HiScore").ToString();
        }
    }

    void PlayerStateSetting()  //플레이어 현재 상태 확인
    {
        playerName = GameManager.instance.playerName;
        playerHp = GameManager.instance.playerHp;
        imagePantarouHp1 = GameObject.Find("PantarouHp1").GetComponent<Image>();
        imagePantarouHp2 = GameObject.Find("PantarouHp2").GetComponent<Image>();
        imageTacoHp1 = GameObject.Find("TacoHp1").GetComponent<Image>();
        imageTacoHp2 = GameObject.Find("TacoHp2").GetComponent<Image>();
        imageItemMenuPantarou = GameObject.Find("ItemMenuPantarou1").GetComponent<Image>();
        imageItemMenuTaco = GameObject.Find("ItemMenuTaco1").GetComponent<Image>();
        textGameOver = GameObject.Find("TextGameOver").GetComponent<Text>();
        textGameOver.enabled = false;
        
    }

    void TimerReset()  //화면에 표시되는 텍스트 관련 변수 초기화
    {
        timer = 0;   //시간 초기화
        second = 0;  //시간 초기화
        minute = 0;  //시간 초기화
        textTimer = GameObject.Find("TimerText").GetComponent<Text>();  //타이머텍스트 찾기
    }

    void MakePlayerCharacter()   //플레이어 캐릭터 설정 (이전 씬에서 선택한 플레이어 오브젝트를 불러옴)
    {
        if (GameManager.instance.playerName == "Pantarou")
        {
            startPos = new Vector3(-4, 0, 0);
            Instantiate(playerPantarou, startPos, Quaternion.identity);
        }
        else if (GameManager.instance.playerName == "Taco")
        {
            startPos = new Vector3(-4, 0, 0);
            Instantiate(playerTaco, startPos, Quaternion.identity);
        }
    }

    void ItemMenuSetting()
    {
        if (playerName == "Pantarou")
        {
            imageItemMenuPantarou.enabled = true;
            imageItemMenuTaco.enabled = false;

        }
        else if (playerName == "Taco")
        {
            imageItemMenuTaco.enabled = true;
            imageItemMenuPantarou.enabled = false;
        }
    }

    private void Update()
    {
        TimeCount();  //시간 카운트 함수
        TimeSkip();   //Time.timeScale 2로 변경해주는 함수 (테스트용 / 0 = 빠르게 / 9 = 정상속도)
        ScoreCount(); //현재 스코어 업데이트
        MakePlayerHpImage();    //선택된 플레이어의 Hp 이미지 적용 함수
    }

    void MakePlayerHpImage()  //화면에 표시될 플레이어의 Hp 이미지 표시
    {
        if (playerName == "Pantarou")
        {
            imageTacoHp1.enabled = false;
            imageTacoHp2.enabled = false;
            switch (playerHp)
            {
                case 2:
                    imagePantarouHp1.enabled = true;
                    imagePantarouHp2.enabled = true;
                    break;
                case 1:
                    imagePantarouHp1.enabled = true;
                    imagePantarouHp2.enabled = false;
                    break;
                case 0:
                    imagePantarouHp1.enabled = false;
                    imagePantarouHp2.enabled = false;
                    break;
            }
        }
        else if (playerName == "Taco")
        {
            imagePantarouHp1.enabled = false;
            imagePantarouHp2.enabled = false;
            switch (playerHp)
            {
                case 2:
                    imageTacoHp1.enabled = true;
                    imageTacoHp2.enabled = true;
                    break;
                case 1:
                    imageTacoHp1.enabled = true;
                    imageTacoHp2.enabled = false;
                    break;
                case 0:
                    imageTacoHp1.enabled = false;
                    imageTacoHp2.enabled = false;
                    break;
            }
        }
    }

    public void GameOverDirection()
    {
        StartCoroutine(MoveToGameOver());
    }

    IEnumerator MoveToGameOver()
    {
        yield return new WaitForSeconds(1f);
        textGameOver.enabled = true;
        Time.timeScale = 0.001f;
        yield return new WaitForSeconds(0.001f);
        textGameOver.enabled = false;
        yield return new WaitForSeconds(0.001f);
        textGameOver.enabled = true;
        yield return new WaitForSeconds(0.001f);
        textGameOver.enabled = false;
        yield return new WaitForSeconds(0.001f);
        textGameOver.enabled = true;
        yield return new WaitForSeconds(0.001f);
        textGameOver.enabled = false;
        yield return new WaitForSeconds(0.001f);
        textGameOver.enabled = true;
        yield return new WaitForSeconds(0.001f);
        textGameOver.enabled = false;
        yield return new WaitForSeconds(0.001f);
        textGameOver.enabled = true;
        yield return new WaitForSeconds(0.001f);
        PlayerPrefs.SetInt("HiScore", GameManager.instance.score);
        GameManager.instance.MoveToContinueScene();
    }

    void TimeCount()   //플레이시간 카운트 함수
    {
        timer += Time.deltaTime;
        second = (int)timer;
        if (second > 59)
        {
            minute++;
            timer = 0;
        }
        textTimer.text = "PlayTime: " + minute.ToString("00") + ":" + second.ToString("00");
        //Debug.Log(timer);
    }

    void TimeSkip()  //스테이지 진행 시 시간 변경
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Time.timeScale = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Time.timeScale = 1;
        }
    }

    void ScoreCount()
    {
        score = GameManager.instance.score;
        textScore.text = score.ToString();
    }

    public void SpreadGunBomb(Vector3 dir)  //펭타로의 SpreadBomb 폭발 오브젝트 불러오기
    {
        GameObject objBullet = objManager.MakeObj("PantarouBulletSpreadBomb");
        objBullet.transform.position = dir;
    }


}
