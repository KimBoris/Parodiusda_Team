using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_BeeBoxCtrl : MonoBehaviour, IDamage
{
    public ObjPoolingMgr objPoolingMgr;   //objPoolingMgr 를 담을 변수
    public Animator beeBoxAnim;   //애니메이터를 담을 변수
    public BoxCollider2D objCollider;  //콜라이더를 담을 변수
    public GameObject explosion; //폭발 이펙트를 담을 오브젝트 변수
      
    public GameObject enemyBee;  //벌 유닛을 담을 오브젝트 변수
    public int enemyHp;  //박스의 체력

    public float fireDir;  //벌을 생성할 타이밍을 확인하기 위한 거리값 변수 (objPoolingMgr 와의 거리값을 계산)
    public float fireTime;  //벌 생성 타이밍
    bool isEnemyDown;

    private void OnEnable()
    {
        objPoolingMgr = GameObject.Find("ObjPoolingManager").GetComponent<ObjPoolingMgr>();  //objManager 할당
        beeBoxAnim = GetComponentInChildren<Animator>();  //박스 애니메이터 할당
        beeBoxAnim.SetInteger("FireBee", 0);
        objCollider = this.gameObject.GetComponent<BoxCollider2D>();  //게임 시작 시에는 콜라이더 비활성화 처리
        objCollider.enabled = false;
        enemyHp = 5;
        fireTime = 0;
        isEnemyDown = false;
    }

    private void Update()
    {
        fireDir = objPoolingMgr.transform.position.x - this.transform.position.x;
        OnCollider();  //플레이화면에 가까워지면 콜라이더 활성화
        if (isEnemyDown == false)  //박스가 죽지 않았을 경우에만 벌 유닛 생성
        {
            MoveOnFireBee();
        }
    }



    void OnCollider()  //화면 정중앙과의 x좌표 위치값이 -6 이하일 경우 콜라이더 활성화
    {
        if (fireDir > -6)
        {
            objCollider.enabled = true;
        }
    }

    void MoveOnFireBee()
    {
        if(fireDir > -5 && fireDir < -3.5f)
        {
            beeBoxAnim.SetInteger("FireBee", 1);
            FireBee();
        }
        else if (fireDir < 4.5f && fireDir > 3f)
        {
            beeBoxAnim.SetInteger("FireBee", 1);
            FireBee();
        }
        else
        {
            beeBoxAnim.SetInteger("FireBee", 0);
        }
    }

    void FireBee()
    {
        fireTime += Time.deltaTime;
        if (fireTime >= 0.3f)
        {
            Instantiate(enemyBee, this.transform.position, Quaternion.identity);
            fireTime = 0;
        }
    }

    public void Damage(int playerAtkDamage)
    {
        enemyHp -= playerAtkDamage;

        if (enemyHp <= 0)
        {
            GameManager.instance.ScoreAdd(100);
            if (isEnemyDown == false)
            {
                Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            }
            beeBoxAnim.SetBool("Destroy", true);
            isEnemyDown = true;
            //this.gameObject.SetActive(false);
        }
    }
}
