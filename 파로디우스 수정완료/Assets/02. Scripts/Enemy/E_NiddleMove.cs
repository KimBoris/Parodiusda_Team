using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_NiddleMove : MonoBehaviour, IDamage
{
    public Animator NiddleAnim;
    public int enemyHp;
    public float E_moveSpeed;
    public float delayTime;

    public GameObject ItemPrefeb;
    public GameObject destroyEff;
    public Transform Player;

    public Transform target;

    public Vector2 contactPoint;

    Vector3 towardPoint;
    Vector3 dir;

    private void OnEnable()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        NiddleAnim = GetComponentInChildren<Animator>();
        E_moveSpeed = 5f;
        delayTime = 3f;
        enemyHp = 0;

        towardPoint = new Vector3(-13f, gameObject.transform.position.y, 0);
        dir = Player.transform.position - gameObject.transform.position;
    }

    void Update()
    {
        ENiddleMove();
        dir = Player.transform.position - gameObject.transform.position;

        if (gameObject.transform.position.x < -12f) //화면 밖을 벗어났을 때 삭제
            Destroy(gameObject);
    }

    void ENiddleMove()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, towardPoint, E_moveSpeed * Time.deltaTime);
        if (gameObject.transform.position.x < -6.4f)
            towardPoint.x = -13f;
    }

    void ENiddleRotate()
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (angle < -90)
        { angle = angle + 180; }
        else if(angle > 90)
        { angle = angle - 180; }
        
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision) //Point를 지나칠 때 마다 Y축 변동
    {
        if (collision.name == "Point80")    //80%지점
        {
            if (gameObject.transform.position.y > Player.transform.position.y)  //적의 Y축이 더 낮을때
            {   
                //플레이어의 위치를 향해 Y축 감소
                new WaitForSeconds(delayTime);
                towardPoint = new Vector3(1.3f, gameObject.transform.position.y - (gameObject.transform.position.y - Player.transform.position.y), 0);
            }
            else if(gameObject.transform.position.y < Player.transform.position.y)  //적의 Y축이 더 높을때
            {
                new WaitForSeconds(delayTime);
                towardPoint = new Vector3(1.3f, gameObject.transform.position.y + (Player.transform.position.y - gameObject.transform.position.y), 0);
            }
        }

        if (collision.name == "Point60")    //60%지점
        {
            if (gameObject.transform.position.y > Player.transform.position.y)  //적의 Y축이 더 낮을때
            {
                //플레이어의 위치를 향해 Y축 감소
                new WaitForSeconds(delayTime);
                towardPoint = new Vector3(-1.3f, gameObject.transform.position.y - (gameObject.transform.position.y - Player.transform.position.y), 0);
            }
            else if (gameObject.transform.position.y < Player.transform.position.y)  //적의 Y축이 더 높을때
            {
                new WaitForSeconds(delayTime);
                towardPoint = new Vector3(-1.3f, gameObject.transform.position.y + (Player.transform.position.y - gameObject.transform.position.y), 0);
            }
        }

        if (collision.name == "Point40")    //40%지점
        {
            if (gameObject.transform.position.y > Player.transform.position.y)  //적의 Y축이 더 낮을때
            {
                //플레이어의 위치를 향해 Y축 감소
                new WaitForSeconds(delayTime);
                towardPoint = new Vector3(-3.9f, gameObject.transform.position.y - (gameObject.transform.position.y - Player.transform.position.y), 0);
            }
            else if (gameObject.transform.position.y < Player.transform.position.y)  //적의 Y축이 더 높을때
            {
                new WaitForSeconds(delayTime);
                towardPoint = new Vector3(-3.9f, gameObject.transform.position.y + (Player.transform.position.y - gameObject.transform.position.y), 0);
            }
        }

        if (collision.name == "Point20")    //20%지점
        {
            if (gameObject.transform.position.y > Player.transform.position.y)  //적의 Y축이 더 낮을때
            {
                //플레이어의 위치를 향해 Y축 감소
                new WaitForSeconds(delayTime);
                towardPoint = new Vector3(-6.5f, Player.transform.position.y, 0);
            }
            else if (gameObject.transform.position.y < Player.transform.position.y)  //적의 Y축이 더 높을때
            {
                new WaitForSeconds(delayTime);
                towardPoint = new Vector3(-6.5f, Player.transform.position.y, 0);
            }
        }

        if (gameObject.transform.position.x > Player.transform.position.x)  //플레이어 앞에 적이 있을 때
        { ENiddleRotate(); }
        
        else                                //플레이어보다 뒤에 있을때
        {
            dir = Vector3.zero;
            ENiddleRotate();
        }

        if (collision.tag == "Player")
        {
            towardPoint.x = -13f;
            Destroy(gameObject);
        }

        contactPoint = collision.ClosestPoint(collision.transform.position);
    }

    public void Damage(int playerAtkDamage)   //플레이어 총알에 맞았을 때 실행될 함수  / IDamage 인터페이스를 통해 총알 피격에 의한 데미지 구현
    {
        enemyHp -= playerAtkDamage;

        if (enemyHp <= 0)
        {

            Instantiate(destroyEff, this.transform.position, Quaternion.identity);
            GameManager.instance.ScoreAdd(100);

            if (NiddleAnim.name == "ImageRedSyringe")
                Instantiate(ItemPrefeb, contactPoint, Quaternion.identity);

            this.gameObject.SetActive(false);
        }
    }
}
