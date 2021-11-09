using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TacoFireCtrl : MonoBehaviour
{
    public ObjPoolingMgr objPoolingMgr;  //오브젝트 풀링 매니저
    public ItemMenu itemMenu;  //아이템 메뉴 스크립트
    public Transform firePos;  //기본 총알 발사 위치
    public GameObject optionTaco0;   //옵션유닛0 게임오브젝트 할당
    public GameObject optionTaco1;   //옵션유닛1 게임오브젝트 할당
    public GameObject optionTaco2;   //옵션유닛2 게임오브젝트 할당
    public GameObject optionTaco3;   //옵션유닛3 게임오브젝트 할당
    public GameObject tacoBarrier;   //보호막 게임오브젝트 할당

    //public Text textBulletCount;

    public string[] bullets;
    public string[] subWeapons;
    public bool isSubweapon;   //서브웨폰 활성화 여부
    public bool isBarrier;     //보호막 활성화 여부

    float fireDelay;
    float fireTime;
    float subFireTime;
    float subFireDelay;
    int bulletNo;

    public int weaponType;
    public int optionNo = 0;

    public int bulletCount;   //총알 카운트, 화면에 2개 이상 나오지 않도록 조정
    GameObject objBullet;

    private void Awake()
    {
        objPoolingMgr = GameObject.Find("ObjPoolingManager").GetComponent<ObjPoolingMgr>();
        optionTaco0 = Instantiate(optionTaco0, firePos.position, Quaternion.identity);  //옵션유닛 할당
        optionTaco1 = Instantiate(optionTaco1, firePos.position, Quaternion.identity);
        optionTaco2 = Instantiate(optionTaco2, firePos.position, Quaternion.identity);
        optionTaco3 = Instantiate(optionTaco3, firePos.position, Quaternion.identity);
        //tacoBarrier = Instantiate(tacoBarrier, firePos.position, Quaternion.identity);  //보호막 생성

        bulletCount = 0;
        bullets = new string[] { "BulletNormalTaco", "BulletDoubleTaco", "BulletRippleTaco" };
        subWeapons = new string[] { "TacoBulletUpTwoWay", "TacoBulletDownTwoWay" };
        weaponType = 0;
        fireTime = 0;
        subFireTime = 0;
        fireDelay = 0.2f;
        subFireDelay = 0.7f;

    }

    private void OnEnable()
    {
        OptionUnitRemove();  //게임 시작 시 옵션유닛 비활성화 (아이템 획득 시 순차 활성화)
        itemMenu = GameObject.Find("StageManager").GetComponent<ItemMenu>();
        //tacoBarrier.SetActive(false);
        isBarrier = false;
        isSubweapon = false;  //게임 시작 시 서브웨폰 false 로 초기화
    }

    public void OptionUnitRemove()
    {
        optionTaco0.SetActive(false);
        optionTaco1.SetActive(false);
        optionTaco2.SetActive(false);
        optionTaco3.SetActive(false);
    }

    void OptionUnitActive1()
    {
        optionTaco0.SetActive(true);
        optionTaco0.transform.position = this.transform.position;
        optionTaco1.SetActive(false);
        optionTaco2.SetActive(false);
        optionTaco3.SetActive(false);
    }
    void OptionUnitActive2()
    {
        optionTaco0.SetActive(true);
        optionTaco1.SetActive(true);
        optionTaco1.transform.position = this.transform.position;
        optionTaco2.SetActive(false);
        optionTaco3.SetActive(false);
    }
    void OptionUnitActive3()
    {
        optionTaco0.SetActive(true);
        optionTaco1.SetActive(true);
        optionTaco2.SetActive(true);
        optionTaco2.transform.position = this.transform.position;
        optionTaco3.SetActive(false);
    }
    void OptionUnitActive4()
    {
        optionTaco0.SetActive(true);
        optionTaco1.SetActive(true);
        optionTaco2.SetActive(true);
        optionTaco3.SetActive(true);
        optionTaco3.transform.position = this.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            OptionActive();  //옵션유닛 생성 함수 (실행 = 영문자 'O')
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            WeaponChange();  //무기 변경 함수 (실행 = 영문자 'I(대문자 아이)')
        }

        FireBullet();    //총알 생성 함수 (실행 = 영문자 'K')
        AutoFireBullet();//총알 자동 발사 함수 (실행 = ';')
        FireSubWeapon(); //서브웨폰 생성 함수 (실행 = 영문자 'L')

        if (Input.GetKeyDown(KeyCode.B))
        {
            BarrierActive(); //보호막 생성 함수 (실행 = 영문자 'B')
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            ActiveSubWeapon();   //서브웨폰 활성화 함수 (실행 = 영문자 'N')
        }

    }

    public void OptionActive()  //옵션유닛 생성 함수 (실행 = 영문자 'O')
    {
        if (optionNo < 5)
        {
            optionNo++;
            OptionPlus(optionNo);  //옵션 추가 삭제 함수
        }
        else if (optionNo == 5)
        {
            optionNo = 0;
        }
        //optionNo = (optionNo + 1) % 5;
        //OptionPlus(optionNo);
    }

    public void OptionPlus(int num)  //옵션의 추가 및 삭제
    {
        switch (num)
        {
            case 0:
                OptionUnitRemove();
                break;
            case 1:
                OptionUnitActive1();
                break;
            case 2:
                OptionUnitActive2();
                break;
            case 3:
                OptionUnitActive3();
                break;

            case 4:
                OptionUnitActive4();
                break;

        }
    }

    public void WeaponChange()   //무기 변경 함수 (실행 = 영문자 'I(대문자 아이)')
    {
        if (weaponType < 3)
        {
            weaponType++;
            if (weaponType == 3)
            {
                weaponType = 0;
            }
        }
    }

    void FireBullet()    //총알 생성 함수 (실행 = 영문자 'K')
    {
        fireTime += Time.deltaTime;
        if (fireTime > fireDelay)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                switch (weaponType)
                {
                    case 0:
                        Fire0();
                        break;

                    case 1:
                        Fire1();
                        break;
                    case 2:
                        Fire2();
                        break;

                }
                fireTime = 0;
            }

        }
    }

    void AutoFireBullet()
    {

        if (Input.GetKey(KeyCode.Semicolon))
        {
            fireTime += Time.deltaTime;
            if (fireTime > fireDelay)
            {
                switch (weaponType)
                {
                    case 0:
                        Fire0();
                        break;

                    case 1:
                        Fire1();
                        break;
                    case 2:
                        Fire2();
                        break;

                }
                fireTime = 0;
            }

        }
    }
    void Fire0()   //기본 총알 생성 함수
    {

        //textBulletCount.text = "" + bulletCount.ToString("00");
        objBullet = objPoolingMgr.MakeObj(bullets[weaponType]);
        objBullet.transform.position = firePos.position;
    }
    void Fire1()   //더블 총알 생성 함수
    {

        //textBulletCount.text = "" + bulletCount.ToString("00");
        objBullet = objPoolingMgr.MakeObj(bullets[weaponType - 1]);
        objBullet.transform.position = firePos.position;
        objBullet = objPoolingMgr.MakeObj(bullets[weaponType]);
        objBullet.transform.position = firePos.position;
    }
    void Fire2()    //Spread 총알 생성 함수
    {

        //textBulletCount.text = "" + bulletCount.ToString("00");
        objBullet = objPoolingMgr.MakeObj(bullets[weaponType]);
        objBullet.transform.position = firePos.position;
    }

    void FireSubWeapon()   //서브웨폰 생성 함수 (실행 = 영문자 'L')
    {
        if (isSubweapon == true)
        {
            subFireTime += Time.deltaTime;
            if (subFireTime > subFireDelay)
            {
                if (Input.GetKeyDown(KeyCode.L))
                {
                    objBullet = objPoolingMgr.MakeObj(subWeapons[0]);
                    objBullet.transform.position = firePos.position;
                    objBullet = objPoolingMgr.MakeObj(subWeapons[1]);
                    objBullet.transform.position = firePos.position;
                    subFireTime = 0;
                }
            }
        }
    }

    public void BarrierActive()
    {
        if (isBarrier == false)
        {
            isBarrier = true;
            //tacoBarrier.SetActive(true);
            Instantiate(tacoBarrier, firePos.position, Quaternion.identity);
        }
    }

    public void BarrierFalse(bool active)
    {
        isBarrier = active;
    }
    public void ActiveSubWeapon()
    {
        isSubweapon = true;
    }
}
