using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomPenguin : MonoBehaviour, IDamage
{//¾Æ·§ÂÊ¿¡¼­ »ý¼ºµÇ´Â ÆÄ¶õ Æë±Ï
    public GameObject itemPrefab;   //Æë±Ï »ç¸Á½Ã ¶³¾î¶ß¸®´Â ¾ÆÀÌÅÛ
    public GameObject destroyEff;

    Animator penguinAnim;   //Æë±Ï ¾Ö´Ï¸ÞÀÌ¼Ç 

    Transform player;       //ÇÃ·¹ÀÌ¾î À§Ä¡

    Vector3 dir;            //Æë±Ï ¹æÇâ

    float speed;
    float downDelay;        //Æë±Ï ³Ñ¾îÁö´Â ¸ð¼Ç (¸îÃÊ¸¶´Ù)
    float penguinHp;        //Æë±Ï HP
    float loudDelay;        //Æë±Ï ¿ïºÎÂ¢´Â ¸ð¼Ç ½ÃÀü ½Ã°£
    float playerX;          //ÇÃ·¹ÀÌ¾î À§Ä¡ÀÇ X°ª
    float penguinX;         //Æë±ÏÀÇ À§Ä¡ÀÇ X°ª

    int downPattern;        //Æë±Ï ³Ñ¾îÁö´Â ÆÐÅÏ(·£´ý)
    int movePattern;        //Æë±Ï ¿òÁ÷ÀÌ´Â ÆÐÅÏ(·£´ý)

    bool isLive;            //Æë±Ï »ì¾Æ ÀÖÀ»¶§ 



    private void OnEnable()
    {
        downPattern = Random.Range(2, 6);
        movePattern = 0;
        isLive = true;
        speed = 2.5f;
        downDelay = 0;
        loudDelay = 0;
        penguinHp = 1;
        penguinAnim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //¿¡³Ê¹Ì³¢¸® Ãæµ¹ÀÌ ÀÖ±â¿¡ ¿¡³Ê¹Ì³¢¸®´Â Ãæµ¹À» ÇØÁ¦ÇÑ´Ù.
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"), true);
    }

    void Update()
    {
        playerX = player.position.x;
        penguinX = this.transform.position.x;
        switch (movePattern)//Æë±ÏÀÇ ¹«ºê ÆÐÅÏ
        {
            case 0:
                Right();
                break;
            case 1:
                //  Left();
                break;
            case 2:
                Move();
                break;
            case 3:
                Loud();
                loudDelay += Time.deltaTime;
                if (loudDelay > 1f)
                {
                    movePattern = 2;
                    loudDelay = 0;
                }
                break;
        }

        if (this.gameObject.transform.position.y < -5.7f)//Æë±ÏÀ§Ä¡ ¹Ù´Ù ¹ØÀ¸·Î °¥ ½Ã ²¨ÁÜ
        {
            this.gameObject.SetActive(false);
        }

        if (isLive == true)
        {
            downDelay += Time.deltaTime;
            if (downDelay > downPattern)
            {
                penguinAnim.SetTrigger("Down");
                downDelay = 0;
            }
        }
    }

    public void Right()
    {
        dir = Vector3.right * 2;
        this.transform.rotation = Quaternion.identity;
        movePattern = 2;
    }
    public void Move()
    {
        this.transform.rotation = Quaternion.identity;
        this.transform.position += dir * speed * Time.deltaTime;
        if (playerX + 1.2f <= penguinX && penguinX <= playerX + 1.6f)
        {
            movePattern = 3;
        }
    }
    public void Loud()
    {//Æë±Ï ¿ïºÎÂ¢´Â ¸ð¼Ç
        penguinAnim.SetTrigger("Loud");
        this.transform.rotation = Quaternion.Euler(0, 180, 0);
        this.transform.position -= dir * 0.4f * Time.deltaTime;
    }
    public void Damage(int damage)
    {
        penguinHp -= damage;
        if (penguinHp <= 0)
        {
            GameManager.instance.ScoreAdd(100);
            float randItem = Random.Range(0, 10000);
            if (randItem < 3000)
            {
                itemPrefab = Instantiate(itemPrefab, this.transform.position, Quaternion.identity);
            }
            destroyEff = Instantiate(destroyEff, this.transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }
}
