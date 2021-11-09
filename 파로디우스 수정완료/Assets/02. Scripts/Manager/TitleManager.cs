using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public int creditCount;
    public Text textInformation;
    public Text textCreditCount;
    public SpriteRenderer pleaseInsertCoin;
    public SpriteRenderer pleaseGameStart;

    private void Awake()
    {
        textInformation = GameObject.Find("ControlInformation").GetComponent<Text>();
        textCreditCount = GameObject.Find("CreditCount").GetComponent<Text>();
        pleaseInsertCoin = GameObject.Find("ImageTitle1").GetComponent<SpriteRenderer>();
        pleaseGameStart = GameObject.Find("ImageTitle2").GetComponent<SpriteRenderer>();

    }
    private void OnEnable()
    {
        textInformation.text = "C 키를 누르면 코인이 삽입됩니다.";        
    }
    private void Update()
    {
        creditCount = GameManager.instance.creditCount;  //게임매니저에서 크레딧 갯수 호출
        textCreditCount.text = creditCount.ToString();   //호출한 크레딧 갯수를 텍스트로 표시
        InsertCoin();   //코인이 삽입되지 않았을 때 실행될 함수
        BeginGame();    //코인이 삽입된 후 게임시작을 위해 실행될 함수
        TitleViewChange();   //코인 삽입 여부에 따라 화면의 이미지 변경 (동전넣어라 / 게임스타트해라... 의 원작 게임화면)
    }

    void InsertCoin()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameManager.instance.creditCount++;
            creditCount = GameManager.instance.creditCount;
        }
    }

    void BeginGame()
    {
        if(creditCount > 0)
        {
            textInformation.text = "U 키를 누르면 게임이 시작됩니다.";
            if (Input.GetKeyDown(KeyCode.U))
            {
                GameManager.instance.creditCount--;
                SceneManager.LoadScene("PlayerSelectScene");
            }
        }
        else if(creditCount <= 0)
        {
            textInformation.text = "C 키를 누르면 코인이 삽입됩니다.";
            if (Input.GetKeyDown(KeyCode.U))
            {
                textInformation.text = "C키를 눌러 코인을 넣어주세요.";
            }
        }
    }

    void TitleViewChange()
    {
        if (creditCount <= 0)
        {
            pleaseInsertCoin.enabled = true;
            pleaseGameStart.enabled = false;
        }
        else if (creditCount > 0)
        {
            pleaseInsertCoin.enabled = false;
            pleaseGameStart.enabled = true;
        }
    }
}
