using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinimeScript : MonoBehaviour, IDamage
{
    public GameObject Boss;
    public GameObject destroyEff;//미니미 사망시 이펙트생성

    public Transform player;     //플레이어 위치
    int minimeHp;                //미니미 체력
    int movePattern;             //미니미 움직이는 패턴

    float minimeTime;            //미니미 생성후 경과시간
    float moveSpeed;             //미니미 회전 속도

    Transform bossCenter;        //미니미가 회전하는 축 중심(보스 몸통)

    BossMove bossmove;           //보스무브 스크립트의 isBossLive를 체크 하기 위한 변수

    Vector3 playerPos;           //플레이어 방향

    private void OnEnable()
    {
     
        minimeHp = 7;
        moveSpeed = 90;
        movePattern = 0;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        if (Boss != null)//보스가 있을 떄에
        {
            bossmove = GameObject.Find("Boss(Clone)").GetComponent<BossMove>();
            bossCenter = GameObject.Find("Boss(Clone)").GetComponent<Transform>();
        }
        if (this.gameObject != null && player != null && bossmove.isBossLive != false)
        {//미니미, 플레이어, 보스가 있을때에만 공격 패턴에 맞춰 공격
            switch (movePattern)
            {
                case 0:
                    Right(); //생성시 오른쪽으로 이동
                    break;
                case 1:
                    Around();//보스 줌심을 기준으로 회전
                    break;
                case 2:
                    PatternThree();//일정시간 후 미니미가 날아갈때의 위치 적용
                    break;
                case 3:
                    Home();//미니미가 PatternThree()에서 정해진 방향으로 날아가서 사라진다
                    break;
                case 4:
                    Stop();//보스가 사망시 멈춤
                    break;

            }
        }
        else if (bossmove.isBossLive == false) 
        {
            //보스가 죽었다면 패턴을 4번으로 변경
            this.transform.SetParent(GameObject.Find("Boss(Clone)").transform);
            
             movePattern = 4;
        }

    }
    void Right()//패턴 0 : 생성시 우측으로 이동
    {
        this.transform.position += Vector3.right * 5 * Time.deltaTime;
        if (this.transform.position.x > 6)
        {
            movePattern = 1;
            minimeTime = 0;
        }
    }
    void Around()//패턴 1 :보스 주변을 회전
    {
        minimeTime += Time.deltaTime;
        transform.RotateAround(bossCenter.position, Vector3.forward, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.identity;
        if (minimeTime > 7)
        {
            movePattern = 2;
            minimeTime = 0;
        }
    }
    void PatternThree()//패턴 2 : 회전하며 플레이어 위치 추적 후 Home으로 패턴 변경
    {
        playerPos = player.position - this.gameObject.transform.position;
        movePattern = 3;
    }
    void Home()//패턴 3 :보스주변을 회전 후 플레이어 방향으로 돌진
    {
        minimeTime += Time.deltaTime;
        transform.parent = null;
        playerPos = playerPos.normalized;
        this.transform.position += playerPos * 5 * Time.deltaTime;
        if (minimeTime > 3)
        {
            gameObject.SetActive(false);
            minimeTime = 0;
        }
    }
    void Stop()//패턴 4 : 보스 죽었을때 멈추는 패턴
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
    }

    public void Damage(int damage)
    {
        minimeHp -= damage;

        if (minimeHp <= 0)
        {
            Instantiate(destroyEff, this.transform.position, Quaternion.identity);
            GameManager.instance.ScoreAdd(100);
            this.gameObject.SetActive(false);
        }
    }
}
