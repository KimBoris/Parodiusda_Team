using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Pingpong : MonoBehaviour, IDamage
{//통통 튀는 펭귄
    public GameObject DestroyEff;   //사망시 이펙트

    Rigidbody2D rigid;      //리지드 바디

    CapsuleCollider2D capCollider;  //콜라이더

    Vector3 dir;            //방향

    int pingpongHp;         //체력
    int movePattern;        //방향전환 패턴

    bool isLanding;         //땅에 닿았을때 리지드 바디로 힘을 주기위해

    private void OnEnable()
    {
        pingpongHp = 1;
        movePattern = 1;
        isLanding = true;
        dir = Vector3.right;
        rigid = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        //에너미끼리 충돌 방지
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"), true);

    }
    void Update()
    {
        this.transform.position += dir * 0.4f * Time.deltaTime;

        switch (movePattern)
        {//땅에 닿았을 시에
            case 0:
                if (isLanding == true)
                {
                    dir = Vector3.right;//오른쪽으로 점프
                    rigid.AddForce(new Vector3(20, 280, 0), ForceMode2D.Force);
                    isLanding = false;
                }
                break;

            case 1:
                if (isLanding == true)
                {
                    dir = Vector3.left * 10;//왼쪽으로 점프
                    rigid.AddForce(new Vector3(-20, 280, 0), ForceMode2D.Force);
                    isLanding = false;
                }
                break;
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "SUBWEAPONTRIGGER")//바닥에 닿았을때
        {
            rigid.velocity = Vector3.zero;//가속도가 붙는것을 방지
            isLanding = true;
        }
        if (collision.tag == "EnemyBlock" && movePattern == 0)//벽에 닿았을때
        {
            movePattern = 1;
        }
        else if (collision.tag == "EnemyBlock" && movePattern == 1)//벽에 닿았을때
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
