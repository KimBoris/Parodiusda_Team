using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantarouBarrier : MonoBehaviour, IDamage
{
    int barrierHp;
    int bulletDamage;
    public Transform playerPos;
    public PantarouFireCtrl pantarouFireCtrl;
    Animator anim;

    private void OnEnable()
    {
        anim = this.gameObject.GetComponentInChildren<Animator>();
        bulletDamage = 1;
        playerPos = GameObject.Find("Pantarou(Clone)").GetComponent<Transform>();
        pantarouFireCtrl = GameObject.Find("Pantarou(Clone)").GetComponent<PantarouFireCtrl>();
        barrierHp = 10;
    }

    private void Update()
    {
        //Vector3 newPos = playerPos.position + new Vector3(0, 0, 0);
        this.transform.position = playerPos.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamage damage = collision.GetComponent<IDamage>();

        if (collision.tag == "ENEMY")
        {
            barrierHp--;
            damage.Damage(bulletDamage);
            anim.SetInteger("BarrierHp", barrierHp);

            if(collision.name == "BossMinime(Clone)")   //보호막이 부딪힌 객체가 보스미니미라면 보스미니미를 비활성화
            {
                collision.gameObject.SetActive(false);
                
            }

            if(barrierHp <= 0)
            {
                pantarouFireCtrl.BarrierFalse(false);
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
            pantarouFireCtrl.BarrierFalse(false);
            StartCoroutine(RemoveBarrier());
        }
    }
}
