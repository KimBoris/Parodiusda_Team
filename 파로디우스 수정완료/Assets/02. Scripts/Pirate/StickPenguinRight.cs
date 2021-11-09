using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickPenguinRight : MonoBehaviour, IDamage
{//¸Þ´Þ¸° Æë±Ï ¿À¸¥ÂÊ
    float enemyHp;

    Animator penguinAnim;

    CapsuleCollider2D capsulecollider;
    void Awake()
    {
        enemyHp = 1;
        penguinAnim = GetComponent<Animator>();
        capsulecollider = GetComponent<CapsuleCollider2D>();
    }
    public void Damage(int damage)
    {
        enemyHp -= damage;
        if (enemyHp <= 0)
        {
            penguinAnim.SetTrigger("Down");
            GameManager.instance.ScoreAdd(100);
            capsulecollider.enabled = false;
        }
    }
}
