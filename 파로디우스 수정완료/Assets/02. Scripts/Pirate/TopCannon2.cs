using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCannon2 : MonoBehaviour, IDamage
{
    [SerializeField]//직렬화 : 다른스크립트에 겹치지 않도록
    GameObject DestroyEff;                  //대포 터질 떄 이펙트

    public int cannon4Hp;                   //체력

    public string[] pirateBullets;          //오브젝트 풀에서 불러올 해적 불렛

    public ObjPoolingMgr objPoolingMgr;     //오브젝트 풀링 매니저    

    float fireDelay;                        //불렛 발사 딜레이
    float fireTime;                         //불렛 발사 시간
    float playerY;                          //Player위치의 Y값
    float pirateY;                          //Pirate의 위치의 Y값

    Animator cannon4Anim;                   //애니메이션 캐논

    Transform player;                       //해적선 불렛
    Transform piratePos;                    //Pirate위치

    CircleCollider2D collider4;             //콜라이더

    GameObject pirateBullet;                //Pirate불렛

    private void Awake()
    {
        pirateBullets = new string[] { "PirateBullet" };
        fireTime = 0;
        cannon4Hp = 8;
        fireDelay = 6;
        collider4 = GetComponent<CircleCollider2D>();
        cannon4Anim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        piratePos = GameObject.Find("PirateShip(Clone)").GetComponent<Transform>();
        objPoolingMgr = GameObject.Find("ObjPoolingManager").GetComponent<ObjPoolingMgr>();
    }
    private void Update()
    {
        pirateY = piratePos.position.y;
        playerY = player.position.y;
        if (cannon4Hp > 0)
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
        if (fireTime > fireDelay && cannon4Hp > 0)
        {
            pirateBullet = objPoolingMgr.MakeObj(pirateBullets[0]);
            pirateBullet.transform.position = this.transform.position;
            fireTime = 0;
        }
    }

    void IDamage.Damage(int damage)
    {
        cannon4Hp -= damage;
        cannon4Anim.SetInteger("TopCannon2Hp", cannon4Hp);
        if (cannon4Hp < 1)
        {
            Instantiate(DestroyEff, this.transform.position, Quaternion.identity);
            collider4.enabled = false;
        }
    }

    void Angle0()
    {
        cannon4Anim.SetInteger("Cannon4Angle", 0);
    }
    void Angle45()
    {
        cannon4Anim.SetInteger("Cannon4Angle", 1);
    }

    void Angle90()
    {
        cannon4Anim.SetInteger("Cannon4Angle", 2);
    }
}
