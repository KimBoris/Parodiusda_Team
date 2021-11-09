using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour, IGetItem
{
    public Image pObjSpeedUp;   //펭타로 활성화 아이템 이미지
    public Image pObjsubWeapon;
    public Image pObjDoubleBullet;
    public Image pObjSpBullet;
    public Image pObjOption;
    public Image pBarrier;

    public Image tObjSpeedUp;  //타코 활성화 아이템 이미지
    public Image tObjsubWeapon;
    public Image tObjDoubleBullet;
    public Image tObjSpBullet;
    public Image tObjOption;
    public Image tBarrier;

    public Image emptySpeedUp;  //해당 아이템이 max 일 경우 활성화될 빈박스
    public Image emptySubWeapon;
    public Image emptyDoubleBullet;
    public Image emptySpBullet;
    public Image emptyOption;
    public Image emptyBarrier;

    public Image curEmptySpeedUp; //해당 아이템이 max 이면서 아이템 획득으로 아이템 순서가 해당 박스에 위치할 경우
    public Image curEmptySubWeapon;
    public Image curEmptyDoubleBullet;
    public Image curEmptySpBullet;
    public Image curEmptyOption;
    public Image curEmptyBarrier;

    public PlayerMove playerMove;
    public PantarouFireCtrl pantarouFireCtrl;
    public TacoFireCtrl tacoFireCtrl;
    public int curItemSelectNo;  //아이템 획득 후 현재 선택되어 있는 아이템의 위치
    //0: 아이템 획득 전, 1:스피드업, 2:서브웨폰, 3/4:총알, 5:옵션, 6:무작위?, 7:보호막

    public string playerName;

    private void OnEnable()
    {
        curItemSelectNo = 0; //게임 시작 시 아이템선택은 0 으로 초기화
        playerName = GameManager.instance.playerName;
        playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        pantarouFireCtrl = GameObject.FindWithTag("Player").GetComponent<PantarouFireCtrl>();
        tacoFireCtrl = GameObject.FindWithTag("Player").GetComponent<TacoFireCtrl>();
        //FireCtrlScr();  //캐릭터에 맞는 스크립트 할당
        ItemMenuImageSetting();  //아이템 이미지 오브젝트 할당
        PantarouItemMenuOff();   //펭타로 전용 이미지 비활성화
        TacoItemMenuOff();       //타코 전용 이미지 비활성화
        CommonItemMenuOff(); //캐릭터 공통 아이템메뉴 오브젝트 비활성화 (max, max 선택시 활성화되는 박스)
    }

    //void FireCtrlScr()
    //{
    //    if (GameObject.Find("Pantarou(Clone)").tag == "Player")
    //    {
    //        pantarouFireCtrl = GameObject.FindWithTag("Player").GetComponent<PantarouFireCtrl>();
    //        tacoFireCtrl = null;
    //    }
    //    else if (GameObject.Find("Taco(Clone)").tag == "Player")
    //    {
    //        tacoFireCtrl = GameObject.FindWithTag("Player").GetComponent<TacoFireCtrl>();
    //        pantarouFireCtrl = null;
    //    }
    //}

    void ItemMenuImageSetting()   //아이템 이미지 오브젝트 할당
    {
        pObjSpeedUp = GameObject.Find("SpeedUpPantarou").GetComponent<Image>();
        pObjsubWeapon = GameObject.Find("SubWeaponPantarou").GetComponent<Image>();
        pObjDoubleBullet = GameObject.Find("DoubleBulletPantarou").GetComponent<Image>();
        pObjSpBullet = GameObject.Find("SpBulletPantarou").GetComponent<Image>();
        pObjOption = GameObject.Find("OptionPantarou").GetComponent<Image>();
        pBarrier = GameObject.Find("BarrierPantarou").GetComponent<Image>();

        tObjSpeedUp = GameObject.Find("SpeedUpTaco").GetComponent<Image>();
        tObjsubWeapon = GameObject.Find("SubWeaponTaco").GetComponent<Image>();
        tObjDoubleBullet = GameObject.Find("DoubleBulletTaco").GetComponent<Image>();
        tObjSpBullet = GameObject.Find("SpBulletTaco").GetComponent<Image>();
        tObjOption = GameObject.Find("OptionTaco").GetComponent<Image>();
        tBarrier = GameObject.Find("BarrierTaco").GetComponent<Image>();

        emptySpeedUp = GameObject.Find("EmptySpeedUp").GetComponent<Image>();
        emptySubWeapon = GameObject.Find("EmptySubWeapon").GetComponent<Image>();
        emptyDoubleBullet = GameObject.Find("EmptyDoubleBullet").GetComponent<Image>();
        emptySpBullet = GameObject.Find("EmptySpBullet").GetComponent<Image>();
        emptyOption = GameObject.Find("EmptyOption").GetComponent<Image>();
        emptyBarrier = GameObject.Find("EmptyBarrier").GetComponent<Image>();

        curEmptySpeedUp = GameObject.Find("CurEmptySpeedUp").GetComponent<Image>();
        curEmptySubWeapon = GameObject.Find("CurEmptySubWeapon").GetComponent<Image>();
        curEmptyDoubleBullet = GameObject.Find("CurEmptyDoubleBullet").GetComponent<Image>();
        curEmptySpBullet = GameObject.Find("CurEmptySpBullet").GetComponent<Image>();
        curEmptyOption = GameObject.Find("CurEmptyOption").GetComponent<Image>();
        curEmptyBarrier = GameObject.Find("CurEmptyBarrier").GetComponent<Image>();
    }

    void PantarouItemMenuOff()
    {
        pObjSpeedUp.enabled = false;
        pObjsubWeapon.enabled = false;
        pObjDoubleBullet.enabled = false;
        pObjSpBullet.enabled = false;
        pObjOption.enabled = false;
        pBarrier.enabled = false;
    }

    void TacoItemMenuOff()
    {
        tObjSpeedUp.enabled = false;
        tObjsubWeapon.enabled = false;
        tObjDoubleBullet.enabled = false;
        tObjSpBullet.enabled = false;
        tObjOption.enabled = false;
        tBarrier.enabled = false;


    }

    void CommonItemMenuOff()  //캐릭터 공통 메뉴 오브젝트 비활성화
    {
        emptySpeedUp.enabled = false;
        emptySubWeapon.enabled = false;
        emptyDoubleBullet.enabled = false;
        emptySpBullet.enabled = false;
        emptyOption.enabled = false;
        emptyBarrier.enabled = false;

        curEmptySpeedUp.enabled = false;
        curEmptySubWeapon.enabled = false;
        curEmptyDoubleBullet.enabled = false;
        curEmptySpBullet.enabled = false;
        curEmptyOption.enabled = false;
        curEmptyBarrier.enabled = false;
    }

    private void Update()
    {
        CharacterNameCheck();      //선택된 캐릭터 확인
        PantarouItemCurMax();      //펭타로 아이템 맥스 시 실행될 함수
        TacoItemCurMax();      //타코 아이템 맥스 시 실행될 함수
    }

    void CharacterNameCheck()
    {
        if (playerName == "Pantarou")
        {
            ChangeItemMenuPantarou();  //아이템카운트에 따라 아이템메뉴 오브젝트 활성화
            TacoItemMenuOff();         //아이템 미획득 상태
        }
        else if (playerName == "Taco")
        {
            ChangeItemMenuTaco();  //아이템카운트에 따라 아이템메뉴 오브젝트 활성화
            PantarouItemMenuOff(); //아이템 미획득 상태
        }
    }

    void PantarouItemCurMax()
    {
        if (pantarouFireCtrl != null)
        {
            if (playerMove.moveSpeed >= 7.5f)  //스피드가 맥스일 경우 스피드업 텍스트 가리기
            {
                emptySpeedUp.enabled = true;
            }

            if (pantarouFireCtrl.isSubweapon == true)
            {
                emptySubWeapon.enabled = true;
            }

            if (pantarouFireCtrl.weaponType == 1)
            {
                emptyDoubleBullet.enabled = true;
                emptySpBullet.enabled = false;
            }

            if (pantarouFireCtrl.weaponType == 2)
            {
                emptySpBullet.enabled = true;
                emptyDoubleBullet.enabled = false;
            }

            if (pantarouFireCtrl.optionNo == 4)
            {
                emptyOption.enabled = true;
            }


            if (pantarouFireCtrl.isBarrier == true)
            {
                emptyBarrier.enabled = true;
            }
            else if (pantarouFireCtrl.isBarrier == false)
            {
                emptyBarrier.enabled = false;
            }
        }
    }

    void TacoItemCurMax()
    {
        if (tacoFireCtrl != null)
        {
            if (playerMove.moveSpeed >= 7.5f)  //스피드가 맥스일 경우 스피드업 텍스트 가리기
            {
                emptySpeedUp.enabled = true;
            }

            if (tacoFireCtrl.isSubweapon == true)
            {
                emptySubWeapon.enabled = true;
            }

            if (tacoFireCtrl.weaponType == 1)
            {
                emptyDoubleBullet.enabled = true;
                emptySpBullet.enabled = false;
            }

            if (tacoFireCtrl.weaponType == 2)
            {
                emptySpBullet.enabled = true;
                emptyDoubleBullet.enabled = false;
            }

            if (tacoFireCtrl.optionNo == 4)
            {
                emptyOption.enabled = true;
            }

            if (tacoFireCtrl.isBarrier == true)
            {
                emptyBarrier.enabled = true;
            }
            else if (tacoFireCtrl.isBarrier == false)
            {
                emptyBarrier.enabled = false;
            }
        }
    }


    void ChangeItemMenuPantarou()  //화면 하단 아이템UI 설정 (펭타로)
    {
        switch (curItemSelectNo)
        {
            case 0:   //아이템 미획득 (게임시작 시 초기화)
                PantarouItemMenuOff();
                CommonItemMenuOff();
                break;
            case 1:   //아이템 획득 후 curItemSelectNo 가 1일 경우
                if (playerMove.moveSpeed >= 7.5f && curItemSelectNo == 1)
                {
                    curEmptySpeedUp.enabled = true;
                    emptySpeedUp.enabled = false;
                    pObjSpeedUp.enabled = false;
                    pBarrier.enabled = false;
                    curEmptyBarrier.enabled = false;
                    
                }
                else if (playerMove.moveSpeed < 7.5f && curItemSelectNo == 1)
                {
                    pObjSpeedUp.enabled = true;
                    pBarrier.enabled = false;
                    curEmptyBarrier.enabled = false;
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        playerMove.MoveSpeedUp();
                        pObjSpeedUp.enabled = false;
                        curItemSelectNo = 0;
                    }
                }
                else if (curItemSelectNo == 1)
                {
                    pObjSpeedUp.enabled = true;
                    curEmptyBarrier.enabled = false;
                }

                    break;

            case 2:   //아이템메뉴가 서브웨폰일 경우
                if(pantarouFireCtrl.isSubweapon == true && curItemSelectNo == 2)
                {
                    curEmptySubWeapon.enabled = true;
                    pObjSpeedUp.enabled = false;
                    curEmptySpeedUp.enabled = false;
                }
                else if (curItemSelectNo == 2)
                {
                    pObjsubWeapon.enabled = true;
                    pObjSpeedUp.enabled = false;
                    curEmptySpeedUp.enabled = false;
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        pantarouFireCtrl.ActiveSubWeapon();
                        pObjsubWeapon.enabled = false;
                        emptySubWeapon.enabled = true;
                        curItemSelectNo = 0;
                    }
                }
                break;

            case 3:  //아이템메뉴가 더블 총알일 경우
                if (pantarouFireCtrl.weaponType == 1 && curItemSelectNo == 3)
                {
                    curEmptySubWeapon.enabled = false;
                    pObjsubWeapon.enabled = false;
                    curEmptyDoubleBullet.enabled = true;
                }
                else if (curItemSelectNo == 3)
                {
                    pObjDoubleBullet.enabled = true;
                    curEmptySubWeapon.enabled = false;
                    pObjsubWeapon.enabled = false;
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        pantarouFireCtrl.weaponType = 1;
                        pObjDoubleBullet.enabled = false;
                        emptySpBullet.enabled = false;
                        emptyDoubleBullet.enabled = true;
                        curItemSelectNo = 0;
                    }
                    
                }
                break;
            case 4:  //아이템메뉴가 특수 총알일 경우
                if (pantarouFireCtrl.weaponType == 2 && curItemSelectNo == 4)
                {
                    pObjDoubleBullet.enabled = false;
                    emptyDoubleBullet.enabled = false;
                    curEmptyDoubleBullet.enabled = false;
                    curEmptySpBullet.enabled = true;
                }
                else if (curItemSelectNo == 4)
                {
                    pObjDoubleBullet.enabled = false;
                    pObjSpBullet.enabled = true;
                    curEmptyDoubleBullet.enabled = false;
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        pantarouFireCtrl.weaponType = 2;
                        emptySpBullet.enabled = true;
                        pObjDoubleBullet.enabled = false;
                        emptyDoubleBullet.enabled = false;
                        
                        curItemSelectNo = 0;
                    }

                }
                break;
            case 5:  //아이템메뉴가 옵션일 경우
                if (pantarouFireCtrl.optionNo == 4 && curItemSelectNo == 5)
                {
                    curEmptyOption.enabled = true;
                    pObjSpBullet.enabled = false;
                    curEmptySpBullet.enabled = false;

                }
                else if (curItemSelectNo == 5)
                {
                    pObjOption.enabled = true;
                    pObjSpBullet.enabled = false;
                    curEmptySpBullet.enabled = false;
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        pantarouFireCtrl.OptionActive();
                        pObjOption.enabled = false;
                        curItemSelectNo = 0;
                    }
                }
                break;
            case 6:   //아이템메뉴가 보호막인 경우
                if (pantarouFireCtrl.isBarrier == true && curItemSelectNo == 6)
                {
                    curEmptyBarrier.enabled = true;
                    pBarrier.enabled = false;
                    pObjOption.enabled = false;
                    curEmptyOption.enabled = false;
                }
                else if (pantarouFireCtrl.isBarrier == false && curItemSelectNo == 6)
                {
                    pBarrier.enabled = true;
                    pObjOption.enabled = false;
                    curEmptyOption.enabled = false;
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        pantarouFireCtrl.BarrierActive();
                        emptyBarrier.enabled = true;
                        pBarrier.enabled = false;
                        curItemSelectNo = 0;
                    }
                }

                break;
        }
    }

    void ChangeItemMenuTaco()  //화면 하단 아이템UI 설정 (타코)
    {
        switch (curItemSelectNo)
        {
            case 0:   //아이템 미획득 (게임시작 시 초기화)
                TacoItemMenuOff();
                CommonItemMenuOff();
                break;
            case 1:   //아이템 획득 후 curItemSelectNo 가 1일 경우
                if (playerMove.moveSpeed >= 7.5f && curItemSelectNo == 1)
                {
                    curEmptySpeedUp.enabled = true;
                    emptySpeedUp.enabled = false;
                    tObjSpeedUp.enabled = false;
                    tBarrier.enabled = false;
                    curEmptyBarrier.enabled = false;

                }
                else if (playerMove.moveSpeed < 7.5f && curItemSelectNo == 1)
                {
                    tObjSpeedUp.enabled = true;
                    tBarrier.enabled = false;
                    curEmptyBarrier.enabled = false;
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        playerMove.MoveSpeedUp();
                        tObjSpeedUp.enabled = false;
                        curItemSelectNo = 0;
                    }
                }
                else if (curItemSelectNo == 1)
                {
                    tObjSpeedUp.enabled = true;
                    curEmptyBarrier.enabled = false;
                }

                break;

            case 2:   //아이템메뉴가 서브웨폰일 경우
                if (tacoFireCtrl.isSubweapon == true && curItemSelectNo == 2)
                {
                    curEmptySubWeapon.enabled = true;
                    tObjSpeedUp.enabled = false;
                    curEmptySpeedUp.enabled = false;
                }
                else if (curItemSelectNo == 2)
                {
                    tObjsubWeapon.enabled = true;
                    tObjSpeedUp.enabled = false;
                    curEmptySpeedUp.enabled = false;
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        tacoFireCtrl.ActiveSubWeapon();
                        tObjsubWeapon.enabled = false;
                        emptySubWeapon.enabled = true;
                        curItemSelectNo = 0;
                    }
                }
                break;

            case 3:  //아이템메뉴가 더블 총알일 경우
                if (tacoFireCtrl.weaponType == 1 && curItemSelectNo == 3)
                {
                    curEmptySubWeapon.enabled = false;
                    tObjsubWeapon.enabled = false;
                    curEmptyDoubleBullet.enabled = true;
                }
                else if (curItemSelectNo == 3)
                {
                    tObjDoubleBullet.enabled = true;
                    curEmptySubWeapon.enabled = false;
                    tObjsubWeapon.enabled = false;
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        tacoFireCtrl.weaponType = 1;
                        tObjDoubleBullet.enabled = false;
                        emptySpBullet.enabled = false;
                        emptyDoubleBullet.enabled = true;
                        curItemSelectNo = 0;
                    }

                }
                break;
            case 4:  //아이템메뉴가 특수 총알일 경우
                if (tacoFireCtrl.weaponType == 2 && curItemSelectNo == 4)
                {
                    tObjDoubleBullet.enabled = false;
                    emptyDoubleBullet.enabled = false;
                    curEmptyDoubleBullet.enabled = false;
                    curEmptySpBullet.enabled = true;
                }
                else if (curItemSelectNo == 4)
                {
                    tObjDoubleBullet.enabled = false;
                    tObjSpBullet.enabled = true;
                    curEmptyDoubleBullet.enabled = false;
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        tacoFireCtrl.weaponType = 2;
                        emptySpBullet.enabled = true;
                        tObjDoubleBullet.enabled = false;
                        emptyDoubleBullet.enabled = false;

                        curItemSelectNo = 0;
                    }

                }
                break;
            case 5:  //아이템메뉴가 옵션일 경우
                if (tacoFireCtrl.optionNo == 4 && curItemSelectNo == 5)
                {
                    curEmptyOption.enabled = true;
                    tObjSpBullet.enabled = false;
                    curEmptySpBullet.enabled = false;

                }
                else if (curItemSelectNo == 5)
                {
                    tObjOption.enabled = true;
                    tObjSpBullet.enabled = false;
                    curEmptySpBullet.enabled = false;
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        tacoFireCtrl.OptionActive();
                        tObjOption.enabled = false;
                        curItemSelectNo = 0;
                    }
                }
                break;
            case 6:   //아이템메뉴가 보호막인 경우
                if (tacoFireCtrl.isBarrier == true && curItemSelectNo == 6)
                {
                    curEmptyBarrier.enabled = true;
                    tBarrier.enabled = false;
                    tObjOption.enabled = false;
                    curEmptyOption.enabled = false;
                }
                else if (tacoFireCtrl.isBarrier == false && curItemSelectNo == 6)
                {
                    tBarrier.enabled = true;
                    tObjOption.enabled = false;
                    curEmptyOption.enabled = false;
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        tacoFireCtrl.BarrierActive();
                        emptyBarrier.enabled = true;
                        tBarrier.enabled = false;
                        curItemSelectNo = 0;
                    }
                }

                break;
        }
    }

    public void GetItem(int itemCount)   //플레이어가 아이템을 획득하면 실행되는 함수 (인터페이스 활용)
    {
        curItemSelectNo += itemCount;
        if (curItemSelectNo == 7)
        {
            curItemSelectNo = 1;
        }
    }
}
