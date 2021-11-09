using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditMove : MonoBehaviour, IDamage
{
    public float moveSpeed;
    public int creditHp;
    public EndingCreditManager creditMgr;
    float timer;


    private void OnEnable()
    {
        creditMgr = GameObject.Find("EndingCreditManager").GetComponent<EndingCreditManager>();
        moveSpeed = 1f;
        creditHp = 3;
        timer = 0;
    }
    private void Update()
    {
        this.transform.position += new Vector3(0, 1, 0) * moveSpeed * Time.deltaTime;
        if(this.transform.position.y > 6.5f)
        {
            this.gameObject.SetActive(false);
        }
        ThanksComment();
        timer += Time.deltaTime;

    }

    void ThanksComment()
    {
        if (this.gameObject.name == "EndingCredit (29)" && this.transform.position.y >= 0)
        {
            moveSpeed = 0;
            if (this.gameObject.name == "EndingCredit (29)" && timer > 85)
            {
                this.gameObject.SetActive(false);
            }
        }

        if (this.gameObject.name == "EndingCredit (30)" && this.transform.position.y >= -2.5f)
        {
            moveSpeed = 0;
            if (this.gameObject.name == "EndingCredit (30)" && timer > 85)
            {
                this.gameObject.SetActive(false);
            }
        }

    }

    public void Damage(int damage)
    {
        creditHp -= damage;

        if(creditHp <= 0)
        {
            creditMgr.ScoreUp(100);
            this.gameObject.SetActive(false);
        }
    }
}
