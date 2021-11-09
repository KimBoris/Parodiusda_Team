using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPantarou : MonoBehaviour
{
    public ObjPoolingMgr objPoolingMgr;  //오브젝트 풀링 매니저
    public PantarouFireCtrl pantarouFireCtrl;
    public Transform target0;
    public Transform target1;
    public Transform target2;
    public Transform target3;
    public Transform firePos;  //총알 발사 위치

    public string[] bullets;
    public string[] subWeapons;
    public bool isSubweapon;   //서브웨폰 활성화 여부

    float fireDelay;
    float fireTime;
    float subFireTime;
    float subFireDelay;

    [SerializeField] float dampSpeed;
    public Animator anim;
    GameObject objBullet;

    int weaponType;

    private void Awake()
    {
        objPoolingMgr = GameObject.Find("ObjPoolingManager").GetComponent<ObjPoolingMgr>();
        pantarouFireCtrl = GameObject.Find("Pantarou(Clone)").GetComponent<PantarouFireCtrl>();
        bullets = new string[] { "BulletNormalPantarou", "BulletDoublePantarou", "BulletSpreadPantarou" };
        subWeapons = new string[] { "BulletPotonPantarou" };
        weaponType = 0;
        fireTime = 0;
        subFireTime = 0;
        fireDelay = 0.3f;
        subFireDelay = 0.5f;
        dampSpeed = 10;
        anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        TargetControl();  //옵션유닛별 이동 위치 타겟 설정
    }

    void TargetControl()  //옵션유닛별 이동 위치 타겟 설정
    {
        if (GameObject.Find("Pantarou(Clone)") != null)
        {
            target0 = GameObject.Find("Pantarou(Clone)").GetComponent<Transform>();
        }

        if (GameObject.Find("OptionPantarou0(Clone)") != null)
        {
            target1 = GameObject.Find("OptionPantarou0(Clone)").GetComponent<Transform>();
        }

        if (GameObject.Find("OptionPantarou1(Clone)") != null)
        {
            target2 = GameObject.Find("OptionPantarou1(Clone)").GetComponent<Transform>();
        }

        if (GameObject.Find("OptionPantarou2(Clone)") != null)
        {
            target3 = GameObject.Find("OptionPantarou2(Clone)").GetComponent<Transform>();
        }
    }

    private void Update()
    {
        weaponType = pantarouFireCtrl.weaponType;
        isSubweapon = pantarouFireCtrl.isSubweapon;

        MoveDirSetting(); //타겟을 따라 이동
        MovingAnim();    //위아래 이동에 따른 애니메이션 구현
        FireBullet();    //총알 생성 함수 (실행 = 영문자 'K')
        AutoFireBullet();//총알 자동 발사 함수 (실행 = ';')
        FireSubWeapon(); //서브웨폰 생성 함수 (실행 = 영문자 'L')




    }

    void MoveDirSetting()   //플레이어 펭타로를 따라 가는 함수
    {
        if (this.gameObject.name == "OptionPantarou0(Clone)")
        {
            Vector3 newPos = target0.position + new Vector3(0, 0, 0);
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * dampSpeed);

        }
        else if (this.gameObject.name == "OptionPantarou1(Clone)")
        {

            Vector3 newPos = target1.position + new Vector3(0, 0, 0);
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * dampSpeed);

        }
        else if (this.gameObject.name == "OptionPantarou2(Clone)")
        {

            Vector3 newPos = target2.position + new Vector3(0, 0, 0);
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * dampSpeed);

        }
        else if (this.gameObject.name == "OptionPantarou3(Clone)")
        {

            Vector3 newPos = target3.position + new Vector3(0, 0, 0);
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * dampSpeed);

        }
    }

    void MovingAnim()  //상하 이동에 따른 애니메이션
    {
        float v = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Vertical") || Input.GetButtonUp("Vertical"))
        {
            anim.SetInteger("Input", (int)v);
        }
    }

    void FireBullet()   //총알 생성 함수
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

    void AutoFireBullet()  //오토파이어
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

    void FireSubWeapon()   //서브웨폰 사용 함수
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
                    subFireTime = 0;
                }
            }
        }
    }

    //옵션 유닛이 늘어날 경우, 2번째부터의 유닛은 이전의 옵션유닛을 따라다녀야하는데...
    //옵션이 하나 생성될 때마다 해당 옵션유닛에 번호를 부여하고, 옵션유닛이 생성될 때 해당 시점에 활성화된 옵션유닛이 있을 경우
    //해당 유닛의 숫자를 확인하여 마지막 유닛의 뒤에 붙어 다닌다
    //예를 들어 옵션 유닛이 현재 2기가 있다면, 해당 옵션유닛들은 0, 1 또는 1, 2 의 번호를 부여하고 3번째 유닛은 1~2번째 유닛의 존재를 확인한 후에 자신에게 2 또는 3 번호를 부여하고
    //바로 앞 번호 유닛의 위치를 할당 받은 후에 움직임을 구현한다
    //20210912, 22:28분 작성 (이 주석은 위 내용을 구현한 후에 삭제할 것)
}
