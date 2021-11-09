using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadBomb : MonoBehaviour
{
    int spreadBombDamage;

    private void Awake()
    {
        spreadBombDamage = 1;
    }

    private void OnEnable()
    {
        spreadBombDamage = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamage damage = collision.GetComponent<IDamage>();
        StartCoroutine(BombRemove());
        if (damage != null && collision.tag == "ENEMY")  //만약 부딪힌 오브젝트의 태그가 ENEMY 라면, 그리고 해당 오브젝트가 damage 를 갖고 있다면 함수 실행
        {
            damage.Damage(spreadBombDamage);


        }
        //    if (collision.tag == "ENEMY" || collision.tag == "BGGuard")
        //{

        //    //collision.hp -= damage;
        //    StartCoroutine(BombRemove());
        //}
    }

    IEnumerator BombRemove()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
}
