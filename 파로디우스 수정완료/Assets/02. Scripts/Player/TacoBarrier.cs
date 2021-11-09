using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacoBarrier : MonoBehaviour, IDamage
{
    int barrierHp;   //보호막의 체력 (10)
    int bulletDamage; //보호막이 ENEMY 태그를 가진 적과 부딪혔을 경우 해당 적에게 주는 데미지 (1)
    public Transform playerPos;  //보호막이 생성된 후 위치할 자리
    public TacoFireCtrl tacoFireCtrl;  //타코 공격관련 스크립트
    Animator anim;  //보호막의 상태에 따른 스프라이트 변경 (애니메이터의 파라미터를 이용해 변환)

    private void OnEnable()
    {
        anim = this.gameObject.GetComponentInChildren<Animator>();
        playerPos = GameObject.Find("Taco(Clone)").GetComponent<Transform>();
        tacoFireCtrl = GameObject.Find("Taco(Clone)").GetComponent<TacoFireCtrl>();
        bulletDamage = 1;
        barrierHp = 10;
    }

    private void Update()
    {
        //Vector3 newPos = playerPos.position + new Vector3(0, 0, 0);
        this.transform.position = playerPos.position + new Vector3(0.72f, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamage damage = collision.GetComponent<IDamage>();

        if (collision.tag == "ENEMY")
        {
            barrierHp--;
            damage.Damage(bulletDamage);
            Debug.Log(barrierHp);
            anim.SetInteger("BarrierHp", barrierHp);


            if (collision.name == "BossMinime(Clone)")   //보호막이 부딪힌 객체가 보스미니미라면 보스미니미를 비활성화
            {
                collision.gameObject.SetActive(false);
            }

            if (barrierHp <= 0)
            {
                tacoFireCtrl.BarrierFalse(false);
                StartCoroutine(RemoveBarrier());
            }
        }
    }
    IEnumerator RemoveBarrier()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    public void Damage(int damage)
    {
        barrierHp -= damage;
        if (barrierHp <= 0)
        {
            tacoFireCtrl.BarrierFalse(false);
            StartCoroutine(RemoveBarrier());
        }
    }
}
