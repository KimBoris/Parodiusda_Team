using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_BeeCtrl : MonoBehaviour, IDamage
{
    public GameObject itemPrefeb;
    public GameObject explosion;
    public Animator beeAnim;

    public float beeSpeed = 20f;
    public float frequency = 13f;
    public float waveHeight = 5f;

    public int enemyHp;

    Vector3 curPos;
    Vector3 targetPos;
    Vector3 localScale;

    bool isTouched;

    void Start()
    {
        beeAnim = GetComponentInChildren<Animator>();
        enemyHp = 1;

        curPos = gameObject.transform.position;
        localScale = transform.localScale;
        targetPos = new Vector3(gameObject.transform.position.x, 0, 0);

        isTouched = false;
    }

    void Update()
    {
        curPos = gameObject.transform.position;

        if (isTouched == true)
            beeMoveWave();
        else beeMoveUp();

        if (gameObject.transform.position.x < -8f)
            gameObject.SetActive(false);
    }

    void beeMoveUp()
    { gameObject.transform.position = Vector3.MoveTowards(curPos, targetPos, Time.deltaTime * beeSpeed); }

    void beeMoveWave()
    {
        beeAnim.SetBool("BeeMove", true);
        localScale.x = -1;
        transform.transform.localScale = localScale;
        targetPos -= transform.right * Time.deltaTime * beeSpeed;
        transform.position = targetPos + transform.up * Mathf.Sin(Time.time * frequency) * waveHeight;
       // transform.Translate(new Vector3(-1f, Mathf.Sin(Time.time), 0f) * 1f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "PointX0")
        { isTouched = true; }
    }

    public void Damage(int playerAtkDamage)
    {
        enemyHp -= playerAtkDamage;

        if (enemyHp <= 0)
        {
            GameManager.instance.ScoreAdd(100);
            float randItem = Random.Range(0, 10000);
            if (randItem < 1000)
            {
                Instantiate(itemPrefeb, gameObject.transform.position, Quaternion.identity);
            }
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }
}
