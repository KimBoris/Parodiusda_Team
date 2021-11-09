using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinimeBullet : MonoBehaviour
{
    public Transform player;

    float bulletSpeed;       //불렛 속도
    float bulletDelay;       //불렛 발사 딜레이
    float bulletPattern;     //불렛 패턴 ( 방향 조절 후 발사 하기 위해 )

    int bulletDamage;        //불렛 데미지

    Vector3 playerPos;       //플레이어 위치 추적

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

    void madeBullet()//불렛 발사 방향 조절
    {
        playerPos = player.position - this.gameObject.transform.position;
        playerPos = playerPos.normalized;
        bulletPattern = 1;
    }
    void Shot() // 불렛 발사
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

        if (collision.tag == "Player")                  //플레이어 충돌시
        {
            damage.Damage(bulletDamage);             
            this.gameObject.SetActive(false);
        }
        if (collision.name == "PantarouBarrier(Clone)") //펜타로 쉴드 타격시
        {
            damage.Damage(bulletDamage);
            this.gameObject.SetActive(false);
        }

        if (collision.name == "TacoBarrier(Clone)")     //타고 쉴드 타격시
        {
            damage.Damage(bulletDamage);
            this.gameObject.SetActive(false);
        }
    }
}
