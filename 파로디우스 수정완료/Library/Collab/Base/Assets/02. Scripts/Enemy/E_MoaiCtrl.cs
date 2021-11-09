using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_MoaiCtrl : MonoBehaviour, IDamage   //IDamage 라는 인터페이스를 활용, 피격 함수 구현
{
    public float moaiSpeed;
    public int enemyHp;
    public int attackCount;

    public Transform Player;
    public MonsterManager monsterManager;
    public E_MoaiCheck moaiCheck1;
    public E_MoaiCheck moaiCheck2;

    public Vector2 contactPoint;

    Vector3 targetPos;
    bool isMoaiBack;

    private void OnEnable()
    {
        monsterManager = GameObject.Find("MonsterManager").GetComponent<MonsterManager>();

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
    }

    void Start()
    {
        moaiSpeed = 5.0f;

        isMoaiBack = false;
        attackCount = 0;

        targetPos = new Vector3(-3.9f, gameObject.transform.position.y, 0);
    }

    void Update()
    {
        MoaiMove();   //모아이 이동 함수

        if (gameObject.transform.position.x > 14)
        { Destroy(gameObject); }
    }

    void MoaiMove()  //모아이 이동
    { transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, Time.deltaTime * moaiSpeed); }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Point20")
        {
            targetPos.x = 1.3f; //60% Point에 닿음
            targetPos.y = Player.transform.position.y;
            isMoaiBack = true;
        }

        if (isMoaiBack == true && collision.name == "Point60")
        {
            targetPos = new Vector3(15f, gameObject.transform.position.y, 0);
            transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, Time.deltaTime * moaiSpeed);
        }
        contactPoint = collision.ClosestPoint(collision.transform.position);
    }

    public void Damage(int playerAtkDamage)   //플레이어 총알에 맞았을 때 실행될 함수  / IDamage 인터페이스를 통해 총알 피격에 의한 데미지 구현
    {
        //if (monsterManager.moaimakeLimit % 2 == 0)
            if (moaiCheck1.name == "MoaiGroup1(Clone)")
        {
            enemyHp -= playerAtkDamage;
            if (enemyHp <= 0)
            {
                GameManager.instance.ScoreAdd(100);
                moaiCheck1.attackCount++;
            }
        }

        //else if (monsterManager.moaimakeLimit % 2 == 1)
             if (moaiCheck2.name == "MoaiGroup2(Clone)")
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