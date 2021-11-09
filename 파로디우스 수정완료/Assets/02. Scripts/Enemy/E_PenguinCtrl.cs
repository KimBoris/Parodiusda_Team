using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_PenguinCtrl : MonoBehaviour
{
    public Animator PenguinAnim;
    public GameObject ItemPrefeb;
    public float penguinSpeed;
    public int enemyHp;

    public Vector2 contactPoint;

    Vector3 curPos;
    Vector3 targetPos;

    void Start()
    {
        enemyHp = 1;
        penguinSpeed = 3.0f;
        curPos = gameObject.transform.position;
        targetPos = new Vector3(1.3f, -3.6f, 0);

        if (gameObject.transform.position.y > 0)
        {
            Physics2D.gravity = new Vector3(0, 0, 0);
            gameObject.transform.rotation = Quaternion.Euler(180, 0, 0);
            targetPos = new Vector3(1.3f, 3.6f, 0);
        }
    }

    void Update()
    {
        curPos = gameObject.transform.position;
        penguinMove();

        if (curPos.x < -9f || curPos.y < -7f || curPos.y > 7f)
            Destroy(gameObject);
    }

    void penguinMove()
    { transform.position = Vector3.MoveTowards(curPos, targetPos, Time.deltaTime * penguinSpeed); }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Point60")
        {
            if (gameObject.transform.position.y < 0)
            {
                targetPos.x = -10f;
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                targetPos.x = -10f;
                gameObject.transform.rotation = Quaternion.Euler(180, 180, 0);
            }
        }

        contactPoint = collision.ClosestPoint(collision.transform.position);
    }

    public void Damage(int playerAtkDamage)
    {
        enemyHp -= playerAtkDamage;

        if (enemyHp <= 0)
        {
            GameManager.instance.ScoreAdd(100);
            if (PenguinAnim.name == "ImageRedMonsterPang")
                Instantiate(ItemPrefeb, contactPoint, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }
}
