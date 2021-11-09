using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiratePenguin : MonoBehaviour, IDamage
{//���������� �����Ǵ� ���
    public GameObject itemPrefab;       //��� ����� ����߸��� ������
    public GameObject destroyEff;
    
    CapsuleCollider2D penguinCollider; // ����� ���ǿ� ���ְ� �ϱ� ���ؼ�

    Animator penguinAnim;   //��� �ִϸ��̼� 

    Transform player;       //�÷��̾� ��ġ

    float Speed;
    float downDelay;        //��� �Ѿ����� ��� (���ʸ���)
    float penguinHp;        //��� HP
    float loudDelay;        //��� ���¢�� ��� ���� �ð�
    float playerX;          //�÷��̾� ��ġ�� X��
    float penguinX;         //����� ��ġ�� X��

    int movePattern;        //��� �Ѿ����� ����(����)
    int downPattern;        //��� �����̴� ����(����)

    bool isLive;            //��� ��� ������ 
    bool isLanding;         //����� �ٴڿ� ����ִ���
    bool isTouchGuard;      //�¿��� �ݶ��̴��� �浹�� ���� ��ȯ

    Vector3 dir;//������ ����� �����̴� ����



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
        //���ʹ̳��� �浹�� �ֱ⿡ ���ʹ̳����� �浹�� �����Ѵ�.
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"), true);
        penguinCollider.isTrigger = false;//Ȯ�� ��Ȯ��
    }

    void Update()
    {
        playerX = player.position.x;
        penguinX = this.transform.position.x;
        switch (movePattern)//����� ���� ����
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

        if (this.gameObject.transform.position.y < -5.7f||this.transform.position.x > 7.1f||this.transform.position.x<-7.5f)//�����ġ �ٴ� ������ �� �� ����
        {
            this.gameObject.SetActive(false);
        }

        //��� �Ѿ����� ���
        if (isLive)
        {
            downDelay += Time.deltaTime;
            if (downDelay > downPattern)
            {
                //�Ѿ����� ��� �־��ֱ�
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

    public void Right()//������ ������ ���� ��� ����
    {
        dir = Vector3.right * 2;
        this.transform.rotation = Quaternion.identity;
    }

    public void Left()//���� ������ ���� ��� ����
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
    {//��� ���¢�� ���
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
