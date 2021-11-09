using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotonCtrl : MonoBehaviour
{
    float moveSpeed;
    int bulletDamage;
    public bool isOnland;
    Rigidbody2D rb;

    private void OnEnable()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.None;
        rb.gravityScale = 1;
        isOnland = false;
        moveSpeed = 5;
        bulletDamage = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)   //땅에 떨어져서 SUBWEAPONTRIGGER 태그를 가진 콜리전에 닿으면 Y축 고정
    {
        IDamage damage = collision.GetComponent<IDamage>();

        if (collision.tag == "ENEMY")
        {
            damage.Damage(bulletDamage);
        }

        if (this.gameObject != null)
        {
            if (collision.tag == "SUBWEAPONTRIGGER")
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)    //SUBWEAPONTRIGGER 에 닿고 있는 동안 Y축을 계속 고정 시키고 앞으로 이동함
    {
        if (this.gameObject != null)
        {
            if (collision.tag == "SUBWEAPONTRIGGER")
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                isOnland = true;
                //StartCoroutine(PotonMoving());
                this.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(100f, -3.8f, 0), moveSpeed * Time.deltaTime);

            }
            else if(collision.tag == "BGGuard")
            {
                isOnland = false;
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)   //SUBWEAPONTRIGGER 콜리전에서 떨어지면 Y축 고정 취소하여 중력 적용, 화면 아래로 떨어지도록 변경
    {
        if (this.gameObject != null)
        {
            if (collision.tag == "SUBWEAPONTRIGGER")
            {
                rb.constraints = RigidbodyConstraints2D.None;
            }
        }
    }

    private void Update()
    {
        if (this.transform.position.y < -5)    //Y축 좌표 -5 이하가 되면 오브젝트 숨김
        {
            this.gameObject.SetActive(false);
        }
    }
}
