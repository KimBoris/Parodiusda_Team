using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public string sceneName;  //씬 이름을 담을 string 변수
    public string playerName;  //이전 씬에서 선택된 플레이어명을 저장할 변수
    public int playerHp;      //플레이어HP 이미지 표시를 위한 변수

    public Image imagePantarouHp1;   //펭타로 HP 이미지 1
    public Image imagePantarouHp2;   //펭타로 HP 이미지 2
    public Image imageTacoHp1;   //타코 HP 이미지 1
    public Image imageTacoHp2;   //타코 HP 이미지 2

    public int score;     //현재 점수를 담을 변수
    public int bestScore; //베스트 점수를 담을 변수
    public Text textScore;   //현재 점수를 표시할 텍스트
    public Text TextBestScore;//베스트 점수를 표시할 텍스트

    public GameObject pantarouItemMenu;
    public GameObject tacoItemMenu;




    private void OnEnable()
    {
        PlayerStatusSetting();  //플레이어 정보 변수 할당
        ItemMenuSetting();  //선택한 캐릭터에 따라 화면 하단의 아이템 이미지 변경
        ScoreLoad();
    }

    void ScoreLoad()
    {
        if (PlayerPrefs.HasKey("HiScore"))
        {
            TextBestScore.text = PlayerPrefs.GetInt("HiScore").ToString();
        }
    }

    void PlayerStatusSetting()
    {
        sceneName = SceneManager.GetActiveScene().name;  //씬 이름 할당 (이게 쓰는 곳이 있나???)
        playerHp = GameManager.instance.playerHp;        //플레이어HP 할당 (화면에 남은 HP 표시할 값)
        playerName = GameManager.instance.playerName;    //플레이어캐릭터 이름을 담을 변수
        score = GameManager.instance.score;              //현재 점수 (게임 시작 시는 0, 플레이 중에는 현재의 점수값)
        bestScore = GameManager.instance.bestScore;      //베스트 점수
        imagePantarouHp1 = GameObject.Find("PantarouHp1").GetComponent<Image>();
        imagePantarouHp2 = GameObject.Find("PantarouHp2").GetComponent<Image>();
        imageTacoHp1 = GameObject.Find("TacoHp1").GetComponent<Image>();
        imageTacoHp2 = GameObject.Find("TacoHp2").GetComponent<Image>();

    }

    void ItemMenuSetting()
    {
        if(playerName == "Pantarou")
        {
            Instantiate(pantarouItemMenu, new Vector3(0, -4.64f, 0), Quaternion.identity);
        }
        else if (playerName == "Taco")
        {
            Instantiate(tacoItemMenu, new Vector3(0, -4.64f, 0), Quaternion.identity);
        }
    }


    private void Update()
    {
        ChangeToGamePlayScene();  //게임실행 씬 이동 함수
        ScoreStateUpdate();       //현재 스코어 갱신 함수
        MakePlayerHpImage();  //현재 플레이어의 정보에 따라 HP 이미지 정보를 적용할 함수
    }

    void ChangeToGamePlayScene()  // Y키를 누르면 게임실행 씬으로 이동
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene("Stage01_SHJ");
        }
    }

    void ScoreStateUpdate()
    {
        score = GameManager.instance.score;
        textScore.text = score.ToString();
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

}
