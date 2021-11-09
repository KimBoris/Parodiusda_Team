using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_CannonPCtrl : MonoBehaviour, IDamage
{
    public GameObject Player;
    public ObjPoolingMgr objPoolingMgr;
    public Animator cannonAnim;
    public GameObject itemWeapon;
    public GameObject destroyEff;

    public string[] cannonPBullets;
    public int enemyHp;

    public float fireDelay, fireTime;  //총알 생성 주기 확인용 시간 함수
    public float cannonDir; //플레이어와 캐논간 거리값
    public float cannonOnMoveDir; //플레이어와 오브젝트풀과의 거리값
    public CircleCollider2D objCollider;   //콜라이더 활성/비활성화를 조정하기 위한 콜라이더 변수

    GameObject cannonPBullet;
 
    private void OnEnable()
    {
        objPoolingMgr = GameObject.Find("ObjPoolingManager").GetComponent<ObjPoolingMgr>();
        cannonAnim = GetComponentInChildren<Animator>();
        objCollider = this.gameObject.GetComponent<CircleCollider2D>();  //게임 시작 시 콜라이더는 비활성화 시킴
        objCollider.enabled = false;
        cannonPBullets = new string[] { "BossMinimeBullet" };
        enemyHp = 3;
        fireTime = 0f;
    }

    void Update()
    {
        Player = GameObject.FindWithTag("Player");
        cannonDir = Player.transform.position.x - this.transform.position.x;  //플레이어와의 거리값
        cannonOnMoveDir = objPoolingMgr.transform.position.x - this.transform.position.x; //objPoolingMgr 과의 거리값 (화면의 정중앙 위치와의 거리)
        fireDelay = Random.Range(3.5f, 5.0f);  //총알 생성 간격은 랜덤레인지로 처리
        OnCollider();  //콜라이더를 활성화 시킬 함수
        checkPlayerPos();   //플레이어 위치에 따른 캐논의 방향 전환

        if (enemyHp > 0)
        {
            setBullet();   //체력이 있을 경우 bullet 발사
        }

        if (this.transform.position.x < -8f)  // x좌표 기준 -8 이하가 되면 오브젝트 비활성화
        {
            gameObject.SetActive(false);
        }
    }

    void OnCollider()  //화면 정중앙과의 x좌표 위치값이 -6 이상일 경우 콜라이더 활성화
    {
        if (cannonOnMoveDir > -6)
        {
            objCollider.enabled = true;
        }
    }

    void checkPlayerPos()  //플레이어와의 거리에 따라 유닛이 바라보는 방향을 변경
    {
        if (cannonAnim.name == "ImageBlueCannonPang")
        {
            if (cannonDir < 0 || cannonDir > 0)   //블루캐논펭귄
            {
                //왼쪽 or 오른쪽 시선처리
                if (cannonDir <= -5)
                {
                    cannonAnim.SetInteger("BlueCannonPang", -6);
                }
                else if (cannonDir > -5 && cannonDir <= -1)
                {
                    cannonAnim.SetInteger("BlueCannonPang", -2);
                }
                else if (cannonDir > -1 && cannonDir <= 1)
                {
                    cannonAnim.SetInteger("BlueCannonPang", 0);
                }
                else if (cannonDir > 1 && cannonDir <= 5)
                {
                    cannonAnim.SetInteger("BlueCannonPang", 2);
                }
                else if (cannonDir > 5)
                { cannonAnim.SetInteger("BlueCannonPang", 6); }
            }
        }
        else if (cannonAnim.name == "ImageRedCannonPang")
        {
            if (cannonDir < 0 || cannonDir > 0)   //레드캐논펭귄
            {
                //왼쪽 or 오른쪽 시선처리
                if (cannonDir <= -5)
                {
                    cannonAnim.SetInteger("RedCannonPang", -6);
                }
                else if (cannonDir > -5 && cannonDir <= -1)
                {
                    cannonAnim.SetInteger("RedCannonPang", -2);
                }
                else if (cannonDir > -1 && cannonDir <= 1)
                {
                    cannonAnim.SetInteger("RedCannonPang", 0);
                }
                else if (cannonDir > 1 && cannonDir <= 5)
                {
                    cannonAnim.SetInteger("RedCannonPang", 2);
                }
                else if (cannonDir > 5)
                { cannonAnim.SetInteger("RedCannonPang", 6); }
            }
        }
    }

    void setBullet()  //플레이화면과 가까워지면 총알 발사 
    {
        fireTime += Time.deltaTime;
        if (cannonOnMoveDir > -6)
        {
            if (fireTime > fireDelay)
            {
                cannonPBullet = objPoolingMgr.MakeObj(cannonPBullets[0]);
                cannonPBullet.transform.position = transform.position;
                fireTime = 0;
            }
        }
    }

    public void Damage(int playerAtkDamage)  //플레이어에게 피격될 경우 실행되는 damage 함수
    {
        enemyHp -= playerAtkDamage;

        if (enemyHp <= 0)
        {
            GameManager.instance.ScoreAdd(100);

            if (this.cannonAnim.name == "ImageRedCannonPang")
            {
                Instantiate(destroyEff, this.transform.position, Quaternion.identity);
                Instantiate(itemWeapon, this.transform.position, Quaternion.identity);

                this.gameObject.SetActive(false);
            }
            else if (this.cannonAnim.name == "ImageBlueCannonPang")
            {
                Instantiate(destroyEff, this.transform.position, Quaternion.identity);
                this.gameObject.SetActive(false);
            }
        }
        
    }
}
