using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateFace : MonoBehaviour, IDamage
{
    public int pirateFaceHp; //해적선의 체력

    float hitDelay;          //피격 딜레이

    bool isPirateHit;        //피격 가능 여부

    Animator pirateAnim;     //해적선 애니메이션
    
    CircleCollider2D faceCollider;//해적선 사망시 콜라이더 꺼주기 위해서
    private void Awake()
    {
        pirateFaceHp = 8;
        isPirateHit = true;
        pirateAnim = GetComponentInChildren<Animator>();
        faceCollider = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        if (isPirateHit == true)
        {
            hitDelay += Time.deltaTime;
            if (hitDelay > 1)
            {
                isPirateHit = false;
                hitDelay = 0;
            }
        }
    }
    void IDamage.Damage(int damage)
    {
        if (isPirateHit == false)
        {
                pirateFaceHp -= damage;
            if (pirateFaceHp >0 )
            {
                isPirateHit = true;
                pirateAnim.SetInteger("CatFaceHp", pirateFaceHp);
                pirateAnim.SetTrigger("Damaged");
            }
            else if (pirateFaceHp <= 0)
            {

                pirateAnim.SetInteger("CatFaceHp", pirateFaceHp);
                pirateAnim.SetTrigger("isFaceDown");
                faceCollider.enabled = false;//해적선 콜라이더를 꺼준다
            }
        }
    }

}
