using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateFace : MonoBehaviour, IDamage
{
    public int pirateFaceHp; //�������� ü��

    float hitDelay;          //�ǰ� ������

    bool isPirateHit;        //�ǰ� ���� ����

    Animator pirateAnim;     //������ �ִϸ��̼�
    
    CircleCollider2D faceCollider;//������ ����� �ݶ��̴� ���ֱ� ���ؼ�
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
                faceCollider.enabled = false;//������ �ݶ��̴��� ���ش�
            }
        }
    }

}
