using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateBullet : MonoBehaviour
{
    public Transform player;         //�÷��̾� ��ġ
    public Vector3 playerPos;        //����
    public GameObject Pirate;        //������ ��ġ

    float bulletSpeed;               //�ҷ� ���ǵ�
    float bulletPattern;             //�ҷ� ���� ( �������� > �߻� )
    float bulletDelay;               //�ҷ� �߻� ������
    
    int bulletDamage;                //�ҷ� ������

    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        bulletPattern = 0;
        bulletDamage = 1;
        bulletSpeed = 5;
    }
    void Update()
    {
        this.transform.position += playerPos * bulletSpeed * Time.deltaTime;
        if (player != null)
        {
            switch (bulletPattern)
            {
                case 0:
                    madeBullet();//�ҷ� ���� �� ���� ����
                    break;
                case 1:
                    Shot();//������ �������� �߻�
                    break;
            }
        }
    }
    void madeBullet()
    {//�ҷ����� ����
        playerPos = player.position - this.gameObject.transform.position;
        playerPos = playerPos.normalized;
        bulletPattern = 1;
    }

    void Shot()
    {//�ҷ� �߻� �ֱ�
        bulletDelay += Time.deltaTime;
        if (bulletDelay > 10f)
        {
            this.gameObject.SetActive(false);
            bulletDelay = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamage damage = collision.GetComponent<IDamage>();

        if (collision.tag == "Player")
        {//�÷��̾� Ÿ�ݽ�
            damage.Damage(bulletDamage);
            this.gameObject.SetActive(false);
        }
        else if (collision.name == "PantarouBarrier(Clone)")
        {//��Ÿ�� ��ȣ���� Ÿ�ݽ�
            damage.Damage(bulletDamage);
            this.gameObject.SetActive(false);
        }
        else if (collision.name == "TacoBarrier(Clone)")
        {//Ÿ�� ��ȣ���� Ÿ�ݽ�
            damage.Damage(bulletDamage);
            this.gameObject.SetActive(false);
        }
    }
}
