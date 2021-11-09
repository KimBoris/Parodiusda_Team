using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPantarou : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;
    int bulletDamage;

    public StageManager stageMgr;
    PantarouFireCtrl fireCtrl;
    SpriteRenderer spriteRenderer;

    string bulletName;

    private void Awake()
    {
        bulletName = this.gameObject.name;
        fireCtrl = GetComponent<PantarouFireCtrl>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        stageMgr = GameObject.Find("StageManager").GetComponent<StageManager>();
    }
    private void OnEnable()
    {
        bulletName = this.gameObject.name;
        bulletDamage = 1;
        moveSpeed = 20;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        StartCoroutine(BulletInvisible());   //총알 반투명 효과 실행 함수 (스프라이트 랜더러값 반복 조절)

    }

    void Update()
    {
        if (bulletName == "BulletNormalPantarou(Clone)")
        {
            BulletMovingNormal();
        }
        else if (bulletName == "BulletDoublePantarou(Clone)")
        {
            BulletMovingDouble();
            //Debug.Log("더블타입");
        }
        else if (bulletName == "BulletSpreadPantarou(Clone)")
        {
            BulletMovingSpread();
        }

        if (this.transform.position.x > 6.6f || this.transform.position.y > 5.6f)
        {
            this.gameObject.SetActive(false);
        }
    }

    void BulletMovingNormal()
    {
        this.transform.position += new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
    }

    void BulletMovingDouble()
    {
        this.transform.position += new Vector3(1, 1, 0).normalized * moveSpeed * Time.deltaTime;
    }

    void BulletMovingSpread()
    {
        this.transform.position += new Vector3(1, 0, 0).normalized * (moveSpeed + 5) * Time.deltaTime;
    }

    IEnumerator BulletInvisible()   //총알 반투명 효과 실행 함수 (스프라이트 랜더러값 반복 조절)
    {
        if(this.gameObject.name != "BulletSpreadPantarou(Clone)") {
            while (this.gameObject != null)
            {
                //Debug.Log("총알 투명");
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);
                yield return new WaitForSeconds(0.05f);
                spriteRenderer.color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)   //총알이 어딘가에 부딪혔을 때
    {
        IDamage damage = collision.GetComponent<IDamage>();

        if (collision.tag == "NONTARGET" || collision.tag == "BGGuard")
        {

            if (this.gameObject.name == "BulletSpreadPantarou(Clone)")   //현재 오브젝트가 펭타로의 스프레트건일 경우, 폭발효과 추가
            {
                stageMgr.SpreadGunBomb(this.transform.position);
                //this.gameObject.SetActive(false);
            }

            this.gameObject.SetActive(false);
        }

        else if (damage != null && collision.tag == "ENEMY")  //만약 부딪힌 오브젝트의 태그가 ENEMY 라면, 그리고 해당 오브젝트가 damage 를 갖고 있다면 함수 실행
        {
            damage.Damage(bulletDamage);


            if (this.gameObject.name == "BulletSpreadPantarou(Clone)")   //현재 오브젝트가 펭타로의 스프레트건일 경우, 폭발효과 추가
            {
                stageMgr.SpreadGunBomb(this.transform.position);
                //this.gameObject.SetActive(false);
            }
            this.gameObject.SetActive(false);
        }
    }

}

