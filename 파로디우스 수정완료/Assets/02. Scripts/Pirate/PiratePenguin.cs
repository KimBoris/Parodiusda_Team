using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiratePenguin : MonoBehaviour, IDamage
{//해적선에서 생성되는 펭귄
    public GameObject itemPrefab;       //펭귄 사망시 떨어뜨리는 아이템
    public GameObject destroyEff;
    
    CapsuleCollider2D penguinCollider; // 펭귄이 갑판에 서있게 하기 위해서

    Animator penguinAnim;   //펭귄 애니메이션 

    Transform player;       //플레이어 위치

    float Speed;
    float downDelay;        //펭귄 넘어지는 모션 (몇초마다)
    float penguinHp;        //펭귄 HP
    float loudDelay;        //펭귄 울부짖는 모션 시전 시간
    float playerX;          //플레이어 위치의 X값
    float penguinX;         //펭귄의 위치의 X값

    int movePattern;        //펭귄 넘어지는 패턴(랜덤)
    int downPattern;        //펭귄 움직이는 패턴(랜덤)

    bool isLive;            //펭귄 살아 있을때 
    bool isLanding;         //펭귄이 바닥에 닿아있는지
    bool isTouchGuard;      //좌우측 콜라이더에 충돌시 방향 전환

    Vector3 dir;//생성시 펭귄이 움직이는 방향



    private void OnEnable()
    {
        Speed = 2f;
        downDelay = 0;
        loudDelay = 0;
        penguinHp = 1;
        isLive = true;
        isLanding = false;
        isTouchGuard = false;
        movePattern = Random.Range(0, 2);
        downPattern = Random.Range(2, 5);
        penguinAnim = GetComponentInChildren<Animator>();
        penguinCollider = GetComponent<CapsuleCollider2D>();
        //에너미끼리 충돌이 있기에 에너미끼리는 충돌을 해제한다.
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"), true);
        penguinCollider.isTrigger = false;//확인 재확인
    }

    void Update()
    {
        playerX = player.position.x;
        penguinX = this.transform.position.x;
        switch (movePattern)//펭귄의 무브 패턴
        {
            case 0:
                Left();
                movePattern = 2;
                break;
            case 1:
                Right();
                movePattern = 3;
                break;
            case 2:
                MoveFromLeft();
                break;
            case 3:
                MoveFromRight();
                break;
            case 4:
                Loud();
                loudDelay += Time.deltaTime;
                if (loudDelay > 0.8f)
                {
                    movePattern = 2;
                    loudDelay = 0;
                }
                break;
            case 5:
                this.transform.position += Vector3.left * 5 * Time.deltaTime;
                Quaternion.Euler(180, 0, 0);
                break;
        }

        if (this.gameObject.transform.position.y < -5.7f||this.transform.position.x > 7.1f||this.transform.position.x<-7.5f)//펭귄위치 바다 밑으로 갈 시 꺼줌
        {
            this.gameObject.SetActive(false);
        }

        //펭귄 넘어지는 모션
        if (isLive)
        {
            downDelay += Time.deltaTime;
            if (downDelay > downPattern)
            {
                //넘어지는 모션 넣어주기
                penguinAnim.SetTrigger("Down");
                downDelay = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sea")
        {
            penguinCollider.isTrigger = true;
        }
        else if (collision.tag == "PirateGuard")
        {
            this.transform.rotation = Quaternion.identity;
            dir = Vector3.right;
            isTouchGuard = true;
        }
        else if (collision.tag == "PirateBottom")
        {
            isLanding = true;
        }
        else if (collision.tag == "EnemyBlock")
        {
            movePattern = 5;
        }

    }

    public void Right()//오른쪽 방향을 가진 펭귄 생성
    {
        dir = Vector3.right * 2;
        this.transform.rotation = Quaternion.identity;
    }

    public void Left()//왼쪽 방향을 가진 펭귄 생성
    {
        dir = Vector3.left * 2;
        this.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    public void MoveFromLeft()
    {
        this.transform.position += dir * Speed * Time.deltaTime;
        if (isTouchGuard == true)
        {
            this.transform.rotation = Quaternion.identity;
            if (playerX + 1.2f <= penguinX && penguinX <= playerX + 2f)
            {
                movePattern = 4;
            }
        }
    }
    public void MoveFromRight()
    {
        this.transform.position += dir * Speed * Time.deltaTime;
    }
    public void Loud()
    {//펭귄 울부짖는 모션
        penguinAnim.SetTrigger("Loud");
        this.transform.rotation = Quaternion.Euler(0, 180, 0);
        this.transform.position -= dir * 0.4f * Time.deltaTime;
    }

    public void Damage(int playerAtkDamage)
    {
        penguinHp -= playerAtkDamage;

        if (penguinHp <= 0)
        {
            GameManager.instance.ScoreAdd(100);
            Instantiate(destroyEff, this.transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }

}
