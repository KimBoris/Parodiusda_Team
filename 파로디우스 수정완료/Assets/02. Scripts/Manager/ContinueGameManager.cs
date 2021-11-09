using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueGameManager : MonoBehaviour
{
    public Text textCreditCount;
    public Text textCreditText;
    public int creditCount;
    public Text textContinueMent;

    public Image countContinue;

    float countChange = 10f;   //시간을 담을 변수

    private void OnEnable()
    {
        TextObjectSetting();      //텍스트 오브젝트 초기화
    }

    void TextObjectSetting()
    {
        textCreditCount = GameObject.Find("CreditCount").GetComponent<Text>();
        textCreditText = GameObject.Find("CreditText").GetComponent<Text>();
        textContinueMent = GameObject.Find("ContinueMent").GetComponent<Text>();
        textCreditText.text = "CREDIT";
        textContinueMent.text = "게임을 다시 시작하려면 코인을 넣어주세요\n(C 키를 누르면 CREDIT 이 올라갑니다)";
    }

    private void Update()
    {
        CurCredit();    //현재 코인수 확인
        ContinueGame();  //컨티뉴 실행 함수
    }

    void CurCredit()  //현재 코인 확인 및 코인 텍스트 표시
    {
        creditCount = GameManager.instance.creditCount;
        textCreditCount.text = creditCount.ToString();
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameManager.instance.creditCount++;
        }
    }

    void ContinueGame()
    {

        ChangeCountImage();   //시간의 흐름에 따라 컨티뉴카운트 숫자가 줄어듬
        if (creditCount > 0)
        {
            textContinueMent.text = "U 키를 누르면 캐릭터 선택 화면으로 돌아갑니다";
            if (Input.GetKeyDown(KeyCode.U))  //크레딧이 있을 경우 U 키를 누르면 스테이지씬(Stage01_SHJ) 호출
            {
                GameManager.instance.ContinueGamePlay();
            }
        }
    }

    void ChangeCountImage()
    {
        string fileName = "Continue/ContinueNum";
        countChange -= Time.deltaTime;
        switch ((int)countChange)
        {
            case 9:
                countContinue.sprite = Resources.Load<Sprite>(fileName + 9);
                break;
            case 8:
                countContinue.sprite = Resources.Load<Sprite>(fileName + 8);
                break;
            case 7:
                countContinue.sprite = Resources.Load<Sprite>(fileName + 7);
                break;
            case 6:
                countContinue.sprite = Resources.Load<Sprite>(fileName + 6);
                break;
            case 5:
                countContinue.sprite = Resources.Load<Sprite>(fileName + 5);
                break;
            case 4:
                countContinue.sprite = Resources.Load<Sprite>(fileName + 4);
                break;
            case 3:
                countContinue.sprite = Resources.Load<Sprite>(fileName + 3);
                break;
            case 2:
                countContinue.sprite = Resources.Load<Sprite>(fileName + 2);
                break;
            case 1:
                countContinue.sprite = Resources.Load<Sprite>(fileName + 1);
                break;
            case 0:
                countContinue.sprite = Resources.Load<Sprite>(fileName + 0);
                GameManager.instance.MoveToGameClearScene();
                break;
            
        }
    }
}
