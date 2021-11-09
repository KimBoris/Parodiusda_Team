using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeScript : MonoBehaviour, IDamage
{                           //해적선 망원경
    public int ScopeHp;     //망원경 체력

    Animator scopeAnim;     //해적선 망원경 애니메이션

    CircleCollider2D scopeCollider;//망원경 콜라이더

    private void Awake()
    {
        ScopeHp = 8;
        scopeAnim = GetComponentInChildren<Animator>();
        scopeCollider = GetComponent<CircleCollider2D>();

    }
    void IDamage.Damage(int damage)
    {
        ScopeHp -= damage;
        if (ScopeHp <= 0)
        {
            scopeCollider.enabled = false;
            scopeAnim.SetTrigger("ScopeDown");
        }
    }
}
