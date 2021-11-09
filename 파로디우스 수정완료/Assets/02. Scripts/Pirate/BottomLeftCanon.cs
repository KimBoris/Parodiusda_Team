using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���� �ϴ� ����
public class BottomLeftCanon : MonoBehaviour, IDamage
{
    public int cannon1Hp;               

    public ObjPoolingMgr objPoolingMgr;//�ҷ��� ���� ������Ʈ Ǯ��

    public string[] pirateBullets;     //������Ʈ Ǯ���� �ҷ��� ���� �ҷ�

    float fireDelay;                   //�ִϸ��̼�
    float fireTime;                    
    float playerY;                     //�÷��̾� ��ġ
    float pirateY;                     //Pirate��ġ

    Animator cannon1Anim;              //������ �ҷ�

    Transform player;                  //�÷��̾� ��ġ
    Transform piratePos;               //������ ��ġ

    GameObject pirateBullet;           //������ �ҷ�

    CapsuleCollider2D collider1;       //�ݶ��̴�

    void Awake()
    {
        pirateBullets = new string[] { "PirateBullet" };
        fireTime = 0;
        cannon1Hp = 8;
        fireDelay = 5;
        collider1 = GetComponent<CapsuleCollider2D>();
        cannon1Anim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        piratePos = GameObject.Find("PirateShip(Clone)").GetComponent<Transform>();
        objPoolingMgr = GameObject.Find("ObjPoolingManager").GetComponent<ObjPoolingMgr>();
    }

    private void Update()
    {
        pirateY = piratePos.position.y;
        playerY = player.position.y;
        if (cannon1Hp > 0)//�÷��̾� ��ġ�� ���� ĳ���� ���ϸ��̼� ����
        {
            if (pirateY - 3 <= playerY)//���� 0
            {
                Angle0();
            }
            else if (pirateY - 3 >= playerY && pirateY - 4 < playerY) //���� 45
            {
                Angle30();
            }
            else if (pirateY - 4 >= playerY && pirateY - 5 < playerY)//���� 90
            {
                Angle45();
            }
            else if (pirateY - 5 >= playerY && pirateY - 6 < playerY)//���� 90
            {
                Angle60();
            }
            else if (pirateY - 6 >= playerY)//���� 90
            {
                Angle90();
            }

            fireTime += Time.deltaTime;
            if (fireTime > fireDelay && cannon1Hp > 0)
            {
                pirateBullet = objPoolingMgr.MakeObj(pirateBullets[0]);
                pirateBullet.transform.position = this.transform.position;
                fireTime = 0;
            }
        }
    }
    void IDamage.Damage(int damage)
    {
        cannon1Hp -= damage;
        cannon1Anim.SetInteger("Cannon1Hp", cannon1Hp);
        if (cannon1Hp < 1)
        {
            cannon1Anim.SetTrigger("CannonDown");
            collider1.enabled = false;
        }

    }

    void Angle0()
    {
        cannon1Anim.SetInteger("Cannon1Angle", 0);
    }
    void Angle30()
    {
        cannon1Anim.SetInteger("Cannon1Angle", 1);
    }
    void Angle45()
    {
        cannon1Anim.SetInteger("Cannon1Angle", 2);
    }
    void Angle60()
    {
        cannon1Anim.SetInteger("Cannon1Angle", 3);
    }
    void Angle90()
    {
        cannon1Anim.SetInteger("Cannon1Angle", 4);
    }


}







