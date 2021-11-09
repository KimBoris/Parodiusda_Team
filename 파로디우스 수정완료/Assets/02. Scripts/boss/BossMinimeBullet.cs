using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinimeBullet : MonoBehaviour
{
    public Transform player;

    float bulletSpeed;       //�ҷ� �ӵ�
    float bulletDelay;       //�ҷ� �߻� ������
    float bulletPattern;     //�ҷ� ���� ( ���� ���� �� �߻� �ϱ� ���� )

    int bulletDamage;        //�ҷ� ������

    Vector3 playerPos;       //�÷��̾� ��ġ ����

    private void OnEnable()
    {
        bulletSpeed = 5;
        bulletDamage = 1;
        bulletPattern = 0;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();

    }
    void Update()
    {
        if (player != null)
        {
            switch (bulletPattern)
            {
                case 0:
                    madeBullet();
                    break;
                case 1:
                    Shot();
                    break;
            }
        }
    }

    void madeBullet()//�ҷ� �߻� ���� ����
    {
        playerPos = player.position - this.gameObject.transform.position;
        playerPos = playerPos.normalized;
        bulletPattern = 1;
    }
    void Shot() // �ҷ� �߻�
    {
        this.transform.position += playerPos * bulletSpeed * Time.deltaTime;
        bulletDelay += Time.deltaTime;
        if (bulletDelay > 2.5f)
        {
            this.gameObject.SetActive(false);
            bulletDelay = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamage damage = collision.GetComponent<IDamage>();

        if (collision.tag == "Player")                  //�÷��̾� �浹��
        {
            damage.Damage(bulletDamage);             
            this.gameObject.SetActive(false);
        }
        if (collision.name == "PantarouBarrier(Clone)") //��Ÿ�� ���� Ÿ�ݽ�
        {
            damage.Damage(bulletDamage);
            this.gameObject.SetActive(false);
        }

        if (collision.name == "TacoBarrier(Clone)")     //Ÿ�� ���� Ÿ�ݽ�
        {
            damage.Damage(bulletDamage);
            this.gameObject.SetActive(false);
        }
    }
}
