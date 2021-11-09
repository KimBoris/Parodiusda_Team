using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeScript : MonoBehaviour, IDamage
{                           //������ ������
    public int ScopeHp;     //������ ü��

    Animator scopeAnim;     //������ ������ �ִϸ��̼�

    CircleCollider2D scopeCollider;//������ �ݶ��̴�

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
