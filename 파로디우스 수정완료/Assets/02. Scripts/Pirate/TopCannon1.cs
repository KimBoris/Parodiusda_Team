using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCannon1 : MonoBehaviour, IDamage
{
    public ObjPoolingMgr objPoolingMgr;//������Ʈ Ǯ�� �Ŵ���

    public int cannon3Hp;             //ü��

    public string[] pirateBullets;    //������Ʈ Ǯ���� �ҷ��� ���� �ҷ�

    public GameObject DestroyEff;      //���� ���� �� ����Ʈ
                                     
    float fireDelay;                   //�ҷ� �߻� ������
    float fireTime;                    //�ҷ� �߻� �ð�
    float playerY;                     //Player��ġ�� Y��
    float pirateY;                     //Pirate�� ��ġ�� Y��
    
    Animator cannon3Anim;              //�ִϸ��̼� ĳ��
                                     
    Transform player;                  //������ �ҷ�
    Transform piratePos;               //Pirate��ġ
    
    CircleCollider2D collider3;        //�ݶ��̴�

    GameObject pirateBullet;           //Pirate�ҷ�
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
            if (pirateY + 1 > playerY)//���� 0
            {
                Angle0();
            }
            else if (pirateY + 1 < playerY && pirateY + 2 > playerY) //���� 45
            {
                Angle45();
            }
            else if (pirateY + 2 <= playerY && pirateY + 3 > playerY)//���� 90
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

