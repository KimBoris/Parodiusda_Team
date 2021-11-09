using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWayCtrl : MonoBehaviour
{
    float moveSpeed;
    int bulletDamage;
    Rigidbody2D rb;

    private void OnEnable()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        moveSpeed = 1;
        bulletDamage = 1;
    }

    private void Update()
    {
        rb.AddForce(Vector2.right * moveSpeed);   //우측 방향으로 약간 이동 (살짝 튕겨주듯)

        if (this.transform.position.y < -5 || this.transform.position.y > 5)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)   //콜리전 충돌 시 실행할 함수
    {
        IDamage damage = collision.GetComponent<IDamage>();

        //if (collision.tag == "NONTARGET" || collision.tag == "BGGuard")
        //{
        //    this.gameObject.SetActive(false);

        //}

        if (damage != null && collision.tag == "ENEMY")  //만약 부딪힌 오브젝트의 태그가 ENEMY 라면, 그리고 해당 오브젝트가 damage 를 갖고 있다면 함수 실행
        {
            damage.Damage(bulletDamage);

            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)   //배경 오브젝트의 콜리전에 닿을 경우 해당 콜리전 범위에서 빠져나가게 되면 실행
    {
        if (collision.tag == "NONTARGET" || collision.tag == "BGGuard")
        {
            this.gameObject.SetActive(false);

        }
    }
}
