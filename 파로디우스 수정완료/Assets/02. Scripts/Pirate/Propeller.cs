using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour, IDamage
{//해적선 아래에 있는 프로펠러
    public int propellerHp;

    Animator propellerAnim;//프로펠러 애니메이션

    CapsuleCollider2D capsuleCollider;//프로펠러 콜라이더
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
