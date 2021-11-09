using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public ObjPoolingMgr objPoolingMgr;  //오브젝트 풀링 매니저
  
    public GameObject objMinime;         //미니미 오브젝트
    public GameObject objMinimeBullet;   //미니미 불렛 오브젝트

    public string[] minimes;             //오브젝트풀에서 불러올 미니미 이름
    public string[] minimesBullet;       //오브젝트 풀에서 불러올 미니미 총알

    public int attackPattern;            //보스공격패터너 0. 등장 1. 미니미 생성
    public int minimeCount;              //미니미 생성 카운트(한번에 최대 8)

    public float delay;                  //미니미 생성 시간
    public float bossTime;               //보스등장시간
    public float minimeTime;             //미니미 생존 시간

    public bool isBossOn;                //보스 생성여부
    public bool isRespawn;               //미니미 재생성여부

    GameObject player;                   //게임 플레이어 추적장치

    private void Awake()
    {
        minimeCount = 0;
        isBossOn = false;
        isRespawn = false;
    }
    void OnEnable()
    {
        delay = 0f;
        isBossOn = true;
        attackPattern = 0;
        minimes = new string[] { "BossMinime" };
        objPoolingMgr = GameObject.Find("ObjPoolingManager").GetComponent<ObjPoolingMgr>();
        player = GameObject.FindWithTag("Player").GetComponent<GameObject>();

    }
    void Update()
    {
        switch (attackPattern)//보스 공격 패턴
        {
            case 0:
                bossTime += Time.deltaTime;
                if (bossTime > 3)
                {
                    attackPattern = 1;
                    delay = 0;
                }
                break;

            case 1: //미니미 생성

                delay += Time.deltaTime;
                if (delay > 0.5f)
                {
                    minimeMake();

                    if (minimeCount == 8)
                    {
                        attackPattern = 2;
                    }
                    delay = 0;
                }
                break;

            case 2: //미니미 집으로 돌아간다
                minimeTime += Time.deltaTime;
                if (minimeTime > 10)
                {
                    minimeCount = 0;
                    attackPattern = 3;
                }
                break;
            case 3:
                minimeTime = 0;
                attackPattern = 1;
                break;
        }
    }
    void minimeMake()//미니미 생성함수
    {
        objMinime = objPoolingMgr.MakeObj(minimes[0]);
        if (objMinime != null)
        {
            objMinime.transform.SetParent(GameObject.Find("Boss(Clone)").transform);
            objMinime.transform.position = this.gameObject.transform.position;
            minimeCount++;
        }
    }
}





