using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinimeScript : MonoBehaviour, IDamage
{
    public GameObject Boss;
    public GameObject destroyEff;//�̴Ϲ� ����� ����Ʈ����

    public Transform player;     //�÷��̾� ��ġ
    int minimeHp;                //�̴Ϲ� ü��
    int movePattern;             //�̴Ϲ� �����̴� ����

    float minimeTime;            //�̴Ϲ� ������ ����ð�
    float moveSpeed;             //�̴Ϲ� ȸ�� �ӵ�

    Transform bossCenter;        //�̴Ϲ̰� ȸ���ϴ� �� �߽�(���� ����)

    BossMove bossmove;           //�������� ��ũ��Ʈ�� isBossLive�� üũ �ϱ� ���� ����

    Vector3 playerPos;           //�÷��̾� ����

    private void OnEnable()
    {
     
        minimeHp = 7;
        moveSpeed = 90;
        movePattern = 0;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        if (Boss != null)//������ ���� ����
        {
            bossmove = GameObject.Find("Boss(Clone)").GetComponent<BossMove>();
            bossCenter = GameObject.Find("Boss(Clone)").GetComponent<Transform>();
        }
        if (this.gameObject != null && player != null && bossmove.isBossLive != false)
        {//�̴Ϲ�, �÷��̾�, ������ ���������� ���� ���Ͽ� ���� ����
            switch (movePattern)
            {
                case 0:
                    Right(); //������ ���������� �̵�
                    break;
                case 1:
                    Around();//���� �ܽ��� �������� ȸ��
                    break;
                case 2:
                    PatternThree();//�����ð� �� �̴Ϲ̰� ���ư����� ��ġ ����
                    break;
                case 3:
                    Home();//�̴Ϲ̰� PatternThree()���� ������ �������� ���ư��� �������
                    break;
                case 4:
                    Stop();//������ ����� ����
                    break;

            }
        }
        else if (bossmove.isBossLive == false) 
        {
            //������ �׾��ٸ� ������ 4������ ����
            this.transform.SetParent(GameObject.Find("Boss(Clone)").transform);
            
             movePattern = 4;
        }

    }
    void Right()//���� 0 : ������ �������� �̵�
    {
        this.transform.position += Vector3.right * 5 * Time.deltaTime;
        if (this.transform.position.x > 6)
        {
            movePattern = 1;
            minimeTime = 0;
        }
    }
    void Around()//���� 1 :���� �ֺ��� ȸ��
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
    void PatternThree()//���� 2 : ȸ���ϸ� �÷��̾� ��ġ ���� �� Home���� ���� ����
    {
        playerPos = player.position - this.gameObject.transform.position;
        movePattern = 3;
    }
    void Home()//���� 3 :�����ֺ��� ȸ�� �� �÷��̾� �������� ����
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
    void Stop()//���� 4 : ���� �׾����� ���ߴ� ����
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
