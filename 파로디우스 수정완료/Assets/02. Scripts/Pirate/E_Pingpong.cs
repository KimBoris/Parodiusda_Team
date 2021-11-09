using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Pingpong : MonoBehaviour, IDamage
{//���� Ƣ�� ���
    public GameObject DestroyEff;   //����� ����Ʈ

    Rigidbody2D rigid;      //������ �ٵ�

    CapsuleCollider2D capCollider;  //�ݶ��̴�

    Vector3 dir;            //����

    int pingpongHp;         //ü��
    int movePattern;        //������ȯ ����

    bool isLanding;         //���� ������� ������ �ٵ�� ���� �ֱ�����

    private void OnEnable()
    {
        pingpongHp = 1;
        movePattern = 1;
        isLanding = true;
        dir = Vector3.right;
        rigid = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        //���ʹ̳��� �浹 ����
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"), true);

    }
    void Update()
    {
        this.transform.position += dir * 0.4f * Time.deltaTime;

        switch (movePattern)
        {//���� ����� �ÿ�
            case 0:
                if (isLanding == true)
                {
                    dir = Vector3.right;//���������� ����
                    rigid.AddForce(new Vector3(20, 280, 0), ForceMode2D.Force);
                    isLanding = false;
                }
                break;

            case 1:
                if (isLanding == true)
                {
                    dir = Vector3.left * 10;//�������� ����
                    rigid.AddForce(new Vector3(-20, 280, 0), ForceMode2D.Force);
                    isLanding = false;
                }
                break;
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "SUBWEAPONTRIGGER")//�ٴڿ� �������
        {
            rigid.velocity = Vector3.zero;//���ӵ��� �ٴ°��� ����
            isLanding = true;
        }
        if (collision.tag == "EnemyBlock" && movePattern == 0)//���� �������
        {
            movePattern = 1;
        }
        else if (collision.tag == "EnemyBlock" && movePattern == 1)//���� �������
        {
            movePattern = 0;
        }
    }

    void IDamage.Damage(int damage)
    {
        pingpongHp -= damage;
        if (pingpongHp < 1)
        {
            GameManager.instance.ScoreAdd(100);
            gameObject.SetActive(false);
            Instantiate(DestroyEff, this.transform.position, Quaternion.identity);
        }
    }

}
