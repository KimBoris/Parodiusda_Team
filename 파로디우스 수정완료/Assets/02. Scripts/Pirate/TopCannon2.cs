using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCannon2 : MonoBehaviour, IDamage
{
    [SerializeField]//����ȭ : �ٸ���ũ��Ʈ�� ��ġ�� �ʵ���
    GameObject DestroyEff;                  //���� ���� �� ����Ʈ

    public int cannon4Hp;                   //ü��

    public string[] pirateBullets;          //������Ʈ Ǯ���� �ҷ��� ���� �ҷ�

    public ObjPoolingMgr objPoolingMgr;     //������Ʈ Ǯ�� �Ŵ���    

    float fireDelay;                        //�ҷ� �߻� ������
    float fireTime;                         //�ҷ� �߻� �ð�
    float playerY;                          //Player��ġ�� Y��
    float pirateY;                          //Pirate�� ��ġ�� Y��

    Animator cannon4Anim;                   //�ִϸ��̼� ĳ��

    Transform player;                       //������ �ҷ�
    Transform piratePos;                    //Pirate��ġ

    CircleCollider2D collider4;             //�ݶ��̴�

    GameObject pirateBullet;                //Pirate�ҷ�

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
