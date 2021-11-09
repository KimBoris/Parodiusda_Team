using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingCreditManager : MonoBehaviour
{
    public GameObject playerTaco;    //타코 플레이어 게임오브젝트
    public GameObject playerPantarou;//펭타로 플레이어 게임오브젝트
    public Vector3 startPos;
    public string playerName1; //선택한 플레이어의 이름
    public int score;
    public float timer;
    public float resultTimer;
    int minute=0;
    int second = 0;
    public Text textScore;
    public Text textTimer;
    public Text textResult;
    public GameObject spriteCredit29;
    public GameObject spriteCredit30;

    private void Awake()
    {
        playerName1 = "Pantarou";
    }
    private void OnEnable()
    {
        if (GameManager.instance.playerName == null)
        {

            MakePlayerCharacter();
        }
        else if (GameManager.instance.playerName != null)
        {
            playerName1 = GameManager.instance.playerName;
            MakePlayerCharacter();
        }

        textResult = GameObject.Find("TextResult").GetComponent<Text>();
        textScore = GameObject.Find("TextEndingCreditScore").GetComponent<Text>();
        //spriteCredit29 = GameObject.Find("EndingCredit (29)");
        //spriteCredit30 = GameObject.Find("EndingCredit (30)");

        score = 0;
        timer = 0;
        resultTimer = 0;

        //spriteCredit29.SetActive(false);
        //spriteCredit30.SetActive(false);
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

    private void Update()
    {
        
        textScore.text = score.ToString();
        //TimeCount();
        resultTimer += Time.deltaTime;
        ResultComment();
        if(resultTimer > 100)
        {
            GameManager.instance.MoveToTitle();
        }
        ChangeTimeScale();
    }

    public void ScoreUp(int scorePoint)
    {
        score += scorePoint;
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

    void ResultComment()
    {
        if (resultTimer> 85 && score >= 1500)
        {
            textResult.text = "저기... 누가 뭘 만들었는지 보긴 보셨죠???\n우리 이름을 너무 많이 파괴 시키셔서\n자막은 제대로 보셨는지 궁금하군요 ㅡㅡ\n아무튼 우리 서다봉의 게임은 여기까지 입니다\n플레이해주셔서 감사합니다\n\nPeace";
        }
        else if (resultTimer > 85 && score < 1500)
        {
            textResult.text = "열심히 만든 작품을 즐겨주셔서 대단히 감사합니다\n만든 사람의 이름도 제대로 보셨겠죠??\n\n당신의 앞날에 행운이 함께하길...\n\nPeace";
        }
    }

    void ChangeTimeScale()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Time.timeScale++;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Time.timeScale = 1;
        }
    }
}
