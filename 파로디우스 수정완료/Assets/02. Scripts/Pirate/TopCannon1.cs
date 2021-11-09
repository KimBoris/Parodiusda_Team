using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCannon1 : MonoBehaviour, IDamage
{
    public ObjPoolingMgr objPoolingMgr;//오브젝트 풀링 매니저

    public int cannon3Hp;             //체력

    public string[] pirateBullets;    //오브젝트 풀에서 불러올 해적 불렛

    public GameObject DestroyEff;      //대포 터질 떄 이펙트
                                     
    float fireDelay;                   //불렛 발사 딜레이
    float fireTime;                    //불렛 발사 시간
    float playerY;                     //Player위치의 Y값
    float pirateY;                     //Pirate의 위치의 Y값
    
    Animator cannon3Anim;              //애니메이션 캐논
                                     
    Transform player;                  //해적선 불렛
    Transform piratePos;               //Pirate위치
    
    CircleCollider2D collider3;        //콜라이더

    GameObject pirateBullet;           //Pirate불렛
    private void Awake()
    {
        pirateBullets = new string[] { "PirateBullet" };
        fireTime = 0;
        cannon3Hp = 8;
        fireDelay = 6;
        collider3 = GetComponent<CircleCollider2D>();
        cannon3Anim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        piratePos = GameObject.Find("PirateShip(Clone)").GetComponent<Transform>();
        objPoolingMgr = GameObject.Find("ObjPoolingManager").GetComponent<ObjPoolingMgr>();
    }

    private void Update()
    {
        pirateY = piratePos.position.y;
        playerY = player.position.y;
        if (cannon3Hp > 0)
        {
            if (pirateY + 1 > playerY)//좌측 0
            {
                Angle0();
            }
            else if (pirateY + 1 < playerY && pirateY + 2 > playerY) //좌측 45
            {
                Angle45();
            }
            else if (pirateY + 2 <= playerY && pirateY + 3 > playerY)//좌측 90
            {
                Angle90();
            }
        }
        fireTime += Time.deltaTime;
        if (fireTime > fireDelay && cannon3Hp > 0)
        {
            pirateBullet = objPoolingMgr.MakeObj(pirateBullets[0]);
            pirateBullet.transform.position = this.transform.position;
            fireTime = 0;
        }
    }
    void IDamage.Damage(int damage)
    {
        cannon3Hp -= damage;
        cannon3Anim.SetInteger("TopCannon1Hp", cannon3Hp);
        if (cannon3Hp < 1)
        {
            collider3.enabled = false;
            Instantiate(DestroyEff, this.transform.position, Quaternion.identity);
        }
    }

    void Angle0()
    {
        cannon3Anim.SetInteger("Cannon3Angle", 0);
    }
    void Angle45()
    {
        cannon3Anim.SetInteger("Cannon3Angle", 1);
    }

    void Angle90()
    {
        cannon3Anim.SetInteger("Cannon3Angle", 2);
    }
}

