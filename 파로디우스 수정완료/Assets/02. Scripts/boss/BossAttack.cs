using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public ObjPoolingMgr objPoolingMgr;  //������Ʈ Ǯ�� �Ŵ���
  
    public GameObject objMinime;         //�̴Ϲ� ������Ʈ
    public GameObject objMinimeBullet;   //�̴Ϲ� �ҷ� ������Ʈ

    public string[] minimes;             //������ƮǮ���� �ҷ��� �̴Ϲ� �̸�
    public string[] minimesBullet;       //������Ʈ Ǯ���� �ҷ��� �̴Ϲ� �Ѿ�

    public int attackPattern;            //�����������ͳ� 0. ���� 1. �̴Ϲ� ����
    public int minimeCount;              //�̴Ϲ� ���� ī��Ʈ(�ѹ��� �ִ� 8)

    public float delay;                  //�̴Ϲ� ���� �ð�
    public float bossTime;               //��������ð�
    public float minimeTime;             //�̴Ϲ� ���� �ð�

    public bool isBossOn;                //���� ��������
    public bool isRespawn;               //�̴Ϲ� ���������

    GameObject player;                   //���� �÷��̾� ������ġ

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
        switch (attackPattern)//���� ���� ����
        {
            case 0:
                bossTime += Time.deltaTime;
                if (bossTime > 3)
                {
                    attackPattern = 1;
                    delay = 0;
                }
                break;

            case 1: //�̴Ϲ� ����

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

            case 2: //�̴Ϲ� ������ ���ư���
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
    void minimeMake()//�̴Ϲ� �����Լ�
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





