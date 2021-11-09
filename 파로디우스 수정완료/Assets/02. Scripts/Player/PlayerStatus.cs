using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour, IDamage
{

    StageManager stageMgr;
    Animator anim;
    public int playerHp;
    public bool isHit;

    // Start is called before the first frame update

    private void OnEnable()
    {
        isHit = false;
        anim = GetComponentInChildren<Animator>();
        playerHp = GameManager.instance.playerHp;

        stageMgr = GameObject.Find("StageManager").GetComponent<StageManager>();  //스테이지매니저 스크립트 담아두기
    }

    public void Damage(int enemyBulletDamage)
    {
        if (!isHit)
        {
            GameManager.instance.playerHp -= enemyBulletDamage;
            isHit = true;

            if (playerHp > 0)
            {
                //moveSpeed = 0;
                anim.SetTrigger("PlayerDown");
                StartCoroutine(PlayerDamage());
            }
            else if (playerHp <= 0)
            {
                stageMgr.GameOverDirection();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //IDamage damage = collision.GetComponent<IDamage>();

        if (collision.tag == "ENEMY" || collision.tag == "ENEMYBULLET" || collision.tag == "BGGuard")
        {
            if (!isHit)
            {
                GameManager.instance.playerHp--;
                isHit = true;

                if (playerHp > 0)
                {
                    //moveSpeed = 0;
                    anim.SetTrigger("PlayerDown");
                    StartCoroutine(PlayerDamage());
                }
                else if (playerHp <= 0)
                {
                    stageMgr.GameOverDirection();
                }
            }
        }
    }

    IEnumerator PlayerDamage()
    {

        //for (int i = 0; i < 10; i++)
        //{
        //    this.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 0.1f);
        //    yield return new WaitForSeconds(0.1f);
        //    this.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        //    yield return new WaitForSeconds(0.1f);
        //}
        //this.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        //isHitPlayer = true;
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0.001f;
        yield return new WaitForSeconds(0.003f);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("GameStartScene");
        Time.timeScale = 1;

    }
}
