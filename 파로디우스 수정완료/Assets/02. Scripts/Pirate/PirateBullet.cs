using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateBullet : MonoBehaviour
{
    public Transform player;         //플레이어 위치
    public Vector3 playerPos;        //방향
    public GameObject Pirate;        //해적선 위치

    float bulletSpeed;               //불렛 스피드
    float bulletPattern;             //불렛 패턴 ( 방향조절 > 발사 )
    float bulletDelay;               //불렛 발사 딜레이
    
    int bulletDamage;                //불렛 데미지

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
                    madeBullet();//불렛 생성 및 방향 설정
                    break;
                case 1:
                    Shot();//지정된 방향으로 발사
                    break;
            }
        }
    }
    void madeBullet()
    {//불렛방향 설정
        playerPos = player.position - this.gameObject.transform.position;
        playerPos = playerPos.normalized;
        bulletPattern = 1;
    }

    void Shot()
    {//불렛 발사 주기
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
        {//플레이어 타격시
            damage.Damage(bulletDamage);
            this.gameObject.SetActive(false);
        }
        else if (collision.name == "PantarouBarrier(Clone)")
        {//펜타로 보호막에 타격시
            damage.Damage(bulletDamage);
            this.gameObject.SetActive(false);
        }
        else if (collision.name == "TacoBarrier(Clone)")
        {//타코 보호막에 타격시
            damage.Damage(bulletDamage);
            this.gameObject.SetActive(false);
        }
    }
}
