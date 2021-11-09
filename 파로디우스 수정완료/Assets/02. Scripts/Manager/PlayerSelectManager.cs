using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelectManager : MonoBehaviour
{
    public string sceneName;         //씬 이름을 담을 string 변수
    public GameObject selectTaco;    //플레이어 셀렉트 씬에서 문어 찾기
    public GameObject selectPantarou;//플레이어 셀렉트 씬에서 펭귄 찾기

    public GameObject autoTaco;      //타코의 조작 오토
    public GameObject manualTaco;    //타코의 조작 메뉴얼
    public GameObject autoPantarou;  //펭타로의 조작 오토
    public GameObject manualPantarou;//펭타로의 조작 메뉴얼

    public string selectPlayer;      //각 씬에서 담을 플레이어캐릭터 이름
    public string selectMode;        //플레이어 조작 모드를 담을 이름

    //private void Awake()
    //{
    //    sceneName = SceneManager.GetActiveScene().name;

    //    if (sceneName == "PlayerSelectScene")
    //    {
    //        selectTaco = GameObject.Find("SelectTaco");
    //        selectPantarou = GameObject.Find("SelectPantarou");
    //    }
    //    else if (sceneName == "ControlSelectScene")
    //    {
    //        selectPlayer = GameManager.instance.playerName;
    //        autoTaco = GameObject.Find("AutoTaco");
    //        manualTaco = GameObject.Find("ManualTaco");
    //        autoPantarou = GameObject.Find("AutoPantarou");
    //        manualPantarou = GameObject.Find("manualPantarou");
    //    }
    //}
    private void OnEnable()
    {
        sceneName = SceneManager.GetActiveScene().name;


        if (sceneName == "PlayerSelectScene")    //플레이어캐릭터 선택 화면일 경우 선택된 캐릭터 게임오브젝트 대입
        {
            selectTaco = GameObject.Find("SelectTaco");
            selectPantarou = GameObject.Find("SelectPantarou");
            GameManager.instance.score = 0;
        }
        else if (sceneName == "ControlSelectScene") { 
            //컨트롤선택 화면일 경우 게임매니저에 선택한 캐릭터의 이름 전달
            //조작방법에 따른 캐릭터 게임오브젝트 대입
        
            selectPlayer = GameManager.instance.playerName;
            autoTaco = GameObject.Find("AutoTaco");
            manualTaco = GameObject.Find("ManualTaco");
            autoPantarou = GameObject.Find("AutoPantarou");
            manualPantarou = GameObject.Find("ManualPantarou");
        }

        if (sceneName == "PlayerSelectScene")
        {
            PlayerSelect();
        }
        else if(sceneName == "ControlSelectScene")
        {
            ModeSelect();
        }
    }

    void PlayerSelect() //선택한 플레이어에 따라 스프라이트 변환
    {
        selectPlayer = "Taco";
        SelectPlayerTaco();
    }

    void ModeSelect()  //선택한 캐릭터에 따라 해당 캐릭터에 맞는 조작설정 화면 활성/비활성화 처리
    {
        if (GameManager.instance.playerName == "Taco")
        {
            TacoCtrlAuto();  //타코 선택 시 실행
        }
        else if (GameManager.instance.playerName == "Pantarou")
        {
            PantarouCtrlAuto();  //펭타로 선택 시 실행
        }
    }

    void SelectPlayerTaco()   //타코를 선택한 경우 타코의 스프라이트 활성화, 펭타로 스프라이트 비활성화
    {
        selectTaco.GetComponent<SpriteRenderer>().enabled = true;
        selectPantarou.GetComponent<SpriteRenderer>().enabled = false;
    }

    void SelectPlayerPantarou() //펭타로가 선택된 경우 펭타로 스프라이트 활성화, 타코 스프라이트 비활성화
    {
        selectTaco.GetComponent<SpriteRenderer>().enabled = false;
        selectPantarou.GetComponent<SpriteRenderer>().enabled = true;
    }
    private void Update()
    {
        if (sceneName == "PlayerSelectScene")  //현재 씬이 플레이어 선택 화면일 경우
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))   //W, S 키로 캐릭터 선택
            {
                if (selectPlayer == "Taco")
                {
                    selectPlayer = "Pantarou";
                    SelectPlayerPantarou();
                }
                else if (selectPlayer == "Pantarou")
                {
                    selectPlayer = "Taco";
                    SelectPlayerTaco();
                }
            }

            if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
            {
                //switch (selectPlayer)
                //{
                //    case "Taco":
                //        GameManager.instance.SelectPlayerName(selectPlayer);
                //        break;

                //    case "Pantarou":
                GameManager.instance.SelectPlayerName(selectPlayer);

                //        break;
                //}
            }
        }
        else if (sceneName == "ControlSelectScene")
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                if (selectMode == "AutoTaco")
                {
                    //selectMode = "ManualTaco";
                    TacoCtrlManual();
                }
                else if (selectMode == "ManualTaco")
                {
                    //selectMode = "AutoTaco";
                    TacoCtrlAuto();
                }
                else if (selectMode == "AutoPantarou")
                {
                    //selectMode = "ManualPantarou";
                    PantarouCtrlManual();
                }
                else if (selectMode == "ManualPantarou")
                {
                    //selectMode = "AutoPantarou";
                    PantarouCtrlAuto();
                }
            }

        }

        //조작방법 설정 (어느 것을 선택해도 해당 캐릭터의 메뉴얼 조작으로 실행됨)
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
        {
            if (selectMode == "AutoTaco" || selectMode == "ManualTaco")
            {
                GameManager.instance.SelectModeName("manualTaco");
            }
            else if (selectMode == "AutoPantarou" || selectMode == "ManualPantarou")
            {
                GameManager.instance.SelectModeName("manualPantarou");
            }

        }

    }
    void TacoCtrlAuto()   //조작설정에 따른 스프라이트 활성/비활성화 처리
    {
        selectMode = "AutoTaco";
        autoTaco.GetComponent<SpriteRenderer>().enabled = true;
        manualTaco.GetComponent<SpriteRenderer>().enabled = false;
        autoPantarou.GetComponent<SpriteRenderer>().enabled = false;
        manualPantarou.GetComponent<SpriteRenderer>().enabled = false;
    }

    void PantarouCtrlAuto()   //조작설정에 따른 스프라이트 활성/비활성화 처리
    {
        selectMode = "AutoPantarou";
        autoTaco.GetComponent<SpriteRenderer>().enabled = false;
        manualTaco.GetComponent<SpriteRenderer>().enabled = false;
        autoPantarou.GetComponent<SpriteRenderer>().enabled = true;
        manualPantarou.GetComponent<SpriteRenderer>().enabled = false;
    }

    void TacoCtrlManual()   //조작설정에 따른 스프라이트 활성/비활성화 처리
    {
        selectMode = "ManualTaco";
        autoTaco.GetComponent<SpriteRenderer>().enabled = false;
        manualTaco.GetComponent<SpriteRenderer>().enabled = true;
        autoPantarou.GetComponent<SpriteRenderer>().enabled = false;
        manualPantarou.GetComponent<SpriteRenderer>().enabled = false;
    }

    void PantarouCtrlManual()   //조작설정에 따른 스프라이트 활성/비활성화 처리
    {
        selectMode = "ManualPantarou";
        autoTaco.GetComponent<SpriteRenderer>().enabled = false;
        manualTaco.GetComponent<SpriteRenderer>().enabled = false;
        autoPantarou.GetComponent<SpriteRenderer>().enabled = false;
        manualPantarou.GetComponent<SpriteRenderer>().enabled = true;
    }
}
