using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_MoaiCtrl : MonoBehaviour, IDamage   //IDamage 라는 인터페이스를 활용, 피격 함수 구현
{
    public float moaiSpeed;
    public int enemyHp;
    public int attackCount;
    public float moveChange;

    public Transform Player;
    public MonsterManager monsterManager;
    public E_MoaiCheck moaiCheck1;
    public E_MoaiCheck moaiCheck2;

    public Vector2 contactPoint;

    Transform checkParent;
    Vector3 targetPos;
    bool isMoaiBack;

    private void OnEnable()
    {
        
        monsterManager = GameObject.Find("MonsterManager").GetComponent<MonsterManager>();
        checkParent = gameObject.transform.parent;

        if (monsterManager.moaimakeLimit % 2 == 0)
        {
            moaiCheck1 = GameObject.Find("MoaiGroup1(Clone)").GetComponent<E_MoaiCheck>();
            moaiCheck2 = null;
        }
        else if (monsterManager.moaimakeLimit % 2 == 1)
        {
            moaiCheck2 = GameObject.Find("MoaiGroup2(Clone)").GetComponent<E_MoaiCheck>();
            moaiCheck1 = null;
        }
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        enemyHp = 1;
        moveChange = monsterManager.transform.position.x - this.transform.position.x;
        moaiSpeed = 5.0f;

        isMoaiBack = false;
        attackCount = 0;

        targetPos = new Vector3(-7f, gameObject.transform.position.y, 0);
    }

    //void Start()
    //{
    //    moaiSpeed = 5.0f;

    //    isMoaiBack = false;
    //    attackCount = 0;

    //    targetPos = new Vector3(-3.9f, gameObject.transform.position.y, 0);
    //}

    void Update()
    {
        moveChange = monsterManager.transform.position.x - this.transform.position.x;  //화면 정중앙과 모아이 사이의 거리값
        MoaiMove();   //모아이 이동 함수
        MovingChange();  //모아이 이동 패턴 변경 함수
        if (gameObject.transform.position.x > 14)
        { Destroy(gameObject); }
    }

    void MoaiMove()  //모아이 이동
    { transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, Time.deltaTime * moaiSpeed); }

    void MovingChange()
    {
        Vector2 playerPos = Player.transform.position;

        if (moveChange > 5f && isMoaiBack == false)
        {
            targetPos.x = 0f; //60% Point에 닿음
            targetPos.y = Player.transform.position.y;
            targetPos = targetPos.normalized;
            isMoaiBack = true;
        }

        if (isMoaiBack == true && moveChange <= 0)
        {
            targetPos = new Vector3(14.5f, this.transform.position.y, 0);
            transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, Time.deltaTime * moaiSpeed);
        }

        if (isMoaiBack == true && moveChange >0)
        {
            //targetPos = new Vector3(15f, gameObject.transform.position.y, 0);
            //targetPos = new Vector3(0f, Player.transform.position.y, 0);
            targetPos.x = 0;
            transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, Time.deltaTime * moaiSpeed);
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.name == "Point20")
        //{
        //    targetPos.x = 1.3f; //60% Point에 닿음
        //    targetPos.y = Player.transform.position.y;
        //    isMoaiBack = true;
        //}

        //if (isMoaiBack == true && collision.name == "Point60")
        //{
        //    targetPos = new Vector3(15f, gameObject.transform.position.y, 0);
        //    transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, Time.deltaTime * moaiSpeed);
        //}

        if (collision.tag == "BULLET")
        {
            //contactPoint = collision.ClosestPoint(collision.transform.position);
            contactPoint = this.transform.position;
        }
    }

    public void Damage(int playerAtkDamage)   //플레이어 총알에 맞았을 때 실행될 함수  / IDamage 인터페이스를 통해 총알 피격에 의한 데미지 구현
    {
        
        //if (monsterManager.moaimakeLimit % 2 == 0)
        if (checkParent.name == "MoaiGroup1(Clone)")
        {
            enemyHp -= playerAtkDamage;
            if (enemyHp <= 0)
            {
                GameManager.instance.ScoreAdd(100);
                moaiCheck1.attackCount++;
            }
        }

        //else if (monsterManager.moaimakeLimit % 2 == 1)
        else if (checkParent.name == "MoaiGroup2(Clone)")
        {
            enemyHp -= playerAtkDamage;
            if (enemyHp <= 0)
            {
                GameManager.instance.ScoreAdd(100);
                moaiCheck2.attackCount++;
            }
        }
        Destroy(gameObject);
    } 
}