using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour, IDamage
{//������ �Ʒ��� �ִ� �����緯
    public int propellerHp;

    Animator propellerAnim;//�����緯 �ִϸ��̼�

    CapsuleCollider2D capsuleCollider;//�����緯 �ݶ��̴�
    private void Awake()
    {
        propellerAnim = GetComponentInChildren<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        propellerHp = 8;
    }
    
    void IDamage.Damage(int damage)
    {
        propellerHp -= damage;
        if (propellerHp <= 0)
        {
            capsuleCollider.enabled = false;
            propellerAnim.SetTrigger("PropellerDown");
        }
    }
}
