using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance = null;
    public string sceneName;
    public string playerName;
    public string controlMode;
    public int playerHp;
    public int score;
    public int bestScore;
    public int creditCount; //현재 크레딧 수
    public float bgMovePosition;

    private void Awake()
    {
        //게임매니저 인스턴스화
        if (instance == null)//싱글턴이 존재하지 않을 경우
        {
            instance = this;//해당 오브젝트를 싱글톤 오브젝트로 설정
        }
        else if (instance != this)//instance에 할당된 클래스의 인스턴스가 다를 경우
        {
            Destroy(this.gameObject);//중복 방지를 위해 이 오브젝트를 삭제
        }

        DontDestroyOnLoad(gameObject);//씬이 변경되더라도 삭제하지 않고 유지


        playerName = null;
        playerHp = 2;
        score = 0;
        creditCount = 0;
        bgMovePosition = 0;
    }

    private void Update()
    {
        sceneName = SceneManager.GetActiveScene().name;  //현재의 씬 네임 확인
        PlayableControl();   //타이틀화면에서 u 키를 누르면 캐릭터 선택 화면으로...
        PlayerStatusReset(); //플레이어 정보 초기화
        //Debug.Log("배경좌표" + bgMovePosition);
        MoveToEndingCredit();

        GamePause();
    }

    void GamePause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }
    }
    void PlayableControl()  //씬 이동 함수
    {

        if (Input.GetKeyDown(KeyCode.T))   //타이틀 씬으로 이동
        {
            SceneManager.LoadScene("TitleScene");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))  //플레이어 체력 다운
        {
            playerHp = 0;  
        }
        else if (Input.GetKeyDown(KeyCode.X))   //게임 클리어 씬으로 이동
        {
            SceneManager.LoadScene("GameClearScene");
        }
    }

    void PlayerStatusReset()   //게임 중 플레이어 선택 화면으로 씬을 이동할 경우, 플레이어의 이름과 HP, 점수를 초기화
    {
        if(sceneName == "PlayerSelectScene")
        {
            playerName = null;
            playerHp = 2;
            score = 0;
        }
    }

    void MoveToEndingCredit()
    {
        if (sceneName == "GameClearScene")
        {
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                SceneManager.LoadScene("EndingCreditScene");
            }
        }
        
    }

    public void ScoreAdd(int addScore)
    {
        score += addScore;
    }

    public void MoveToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void SelectPlayerName(string pName)  
        //캐릭터 선택시 해당 캐릭터의 이름을 저장하고 조작방법 설정 씬으로 이동하는 함수
        //캐릭터 선택 씬에서 캐릭터 선택을 하면 실행됨
    {
        playerName = pName;
        SceneManager.LoadScene("ControlSelectScene");
    }


    public void SelectModeName(string mode)  
        //캐릭터 조작 설정을 완료하면 게임 스타트 씬으로 이동
        //캐릭터 조작 방법을 선택하면 실행됨
    {
        controlMode = mode;
        SceneManager.LoadScene("GameStartScene");

    }

    public void ContinueGamePlay()  //컨티뉴 시 캐릭터 선택 씬으로 이동
    {
        creditCount--;
        SceneManager.LoadScene("PlayerSelectScene");
    }

    public void MoveToContinueScene()  //게임오버가 되면 실행되는 함수, 컨티뉴게임 씬으로 이동
    {
        SceneManager.LoadScene("ContinueGameScene");
        Time.timeScale = 1;
    }

    public void MoveToGameClearScene()
    {
        SceneManager.LoadScene("GameClearScene");
    }
}
