using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//우측 하단 대포
public class BottomRightCannon : MonoBehaviour, IDamage
{
    public ObjPoolingMgr objPoolingMgr;//불렛을 만들어낼 오브젝트 풀링

    public int cannon2Hp;              //체력

    public string[] pirateBullets;     //오브젝트 풀에서 불러올 해적 불렛

    float fireDelay;                   //불렛 발사 딜레이
    float fireTime;                    //불렛 발사 시간
    float playerY;                     //플레이어 위치의 Y값
    float pirateY;                     //Pirate위치의 Y값

    Animator cannon2Anim;              //애니메이션

    Transform player;                  //플레이어 위치
    Transform piratePos;               //Pirate위치

    GameObject pirateBullet;           //해적선 불렛

    CapsuleCollider2D collider2;       //콜라이더

    void Awake()
    {
        pirateBullets = new string[] { "PirateBullet" };
        cannon2Hp = 8;
        fireDelay = 5;
        fireTime = 0;
        collider2 = GetComponent<CapsuleCollider2D>();
        cannon2Anim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        piratePos = GameObject.Find("PirateShip(Clone)").GetComponent<Transform>();
        objPoolingMgr = GameObject.Find("ObjPoolingManager").GetComponent<ObjPoolingMgr>();
    }

    private void Update()
    {
        pirateY = piratePos.position.y;
        playerY = player.position.y;
        if (cannon2Hp > 0)//플레이어 위치에 따라 캐논의 에니메이션 변경
        {
            if (pirateY - 3 <= playerY)//좌측 0
            {
                Angle0();
            }
            else if (pirateY - 3 > playerY && pirateY - 4 < playerY) //좌측 45
            {
                Angle30();
            }
            else if (pirateY - 4 >= playerY && pirateY - 5 < playerY)//좌측 90
            {
                Angle45();
            }
            else if (pirateY - 5 >= playerY && pirateY - 6 < playerY)//좌측 90
            {
                Angle60();
            }
            else if (pirateY - 6 >= playerY)//좌측 90
            {
                Angle90();
            }
        }

        fireTime += Time.deltaTime;
        if (fireTime > fireDelay && cannon2Hp > 0)
        {
            pirateBullet = objPoolingMgr.MakeObj(pirateBullets[0]);
            pirateBullet.transform.position = this.transform.position;
            fireTime = 0;
        }
    }
    void IDamage.Damage(int damage)
    {
        cannon2Hp -= damage;
        cannon2Anim.SetInteger("Cannon2Hp", cannon2Hp);
        if (cannon2Hp < 1)
        {
            cannon2Anim.SetTrigger("Cannon2Down");
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
            collider2.enabled = false;//더이상 충돌이 일어나지 않게 콜라이더를 꺼준다
        }
    }
    void Angle0()
    {
        cannon2Anim.SetInteger("Cannon2Angle", 0);
    }
    void Angle30()
    {
        cannon2Anim.SetInteger("Cannon2Angle", 1);
    }
    void Angle45()
    {
        cannon2Anim.SetInteger("Cannon2Angle", 2);
    }
    void Angle60()
    {
        cannon2Anim.SetInteger("Cannon2Angle", 3);
    }
    void Angle90()
    {
        cannon2Anim.SetInteger("Cannon2Angle", 4);
    }





}
