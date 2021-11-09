using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_CrabCtrl : MonoBehaviour, IDamage
{
    public ObjPoolingMgr objPoolingMgr;
    public Transform firePos;

    public string[] crabBullets;
    public float crabSpeed;
    public int enemyHp;
    public int loopCount;
    public float fireDelay, fireTime;
    
    float randX;
    float maxX, minX;

    GameObject crabBullet;
    public GameObject destroyEff;


    Vector3 curPos;
    Vector3 targetPos;
    public Rigidbody2D rb;

    private void OnEnable()
    {
        objPoolingMgr = GameObject.Find("ObjPoolingManager").GetComponent<ObjPoolingMgr>();
        crabBullets = new string[] { "BossMinimeBullet" };
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        enemyHp = 1;
        crabSpeed = 3.0f;

        fireDelay = 6f;
        fireTime = 0f;

        randX = Random.Range(-3.0f, 3.0f);
        maxX = randX + 1.3f;
        minX = randX - 1.3f;
        targetPos = new Vector3(randX, gameObject.transform.position.y, 0);

        if (gameObject.transform.position.y > 0)
        {
            rb.gravityScale = 0;
            gameObject.transform.rotation = Quaternion.Euler(180, 0, 0);
        }
    }

    void Update()
    {
        curPos = gameObject.transform.position;
        CrabMove();

        if (enemyHp > 0)
            setBullet();

        if (gameObject.transform.position.x < -12f)
            gameObject.SetActive(false);
    }

    void CrabMove()
    {
        transform.position = Vector3.MoveTowards(curPos, targetPos, crabSpeed * Time.deltaTime);

        if (gameObject.transform.position.x > maxX - 0.1f)
        { targetPos.x = minX; }
        if (gameObject.transform.position.x < minX + 0.1f)
        {
            targetPos.x = maxX;
            loopCount++;
        }

        if (loopCount > 4)
        {
            targetPos.x = -13f;
            transform.position = Vector3.MoveTowards(curPos, targetPos, crabSpeed * Time.deltaTime);
        }
    }
    void setBullet()
    {
        fireTime += Time.deltaTime;

        if (fireTime > fireDelay)
        {
            crabBullet = objPoolingMgr.MakeObj(crabBullets[0]);
            crabBullet.transform.position = transform.position;
            fireTime = 0;
        }
    }

    public void Damage(int playerAtkDamage)
    {
        enemyHp -= playerAtkDamage;

        if (enemyHp <= 0)
        {
            Instantiate(destroyEff, this.transform.position, Quaternion.identity);
            GameManager.instance.ScoreAdd(100);
            this.gameObject.SetActive(false);
        }
    }
}
