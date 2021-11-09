using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTale : MonoBehaviour
{//해적선 꼬리
    public GameObject explosion; //꼬리 터질때 이펙트

    Animator catTaleAnim; //해적선 꼬리

    PirateFace pirateFace;//해적선 얼굴

    bool isLive;          //생사 여부


    void Start()
    {
        isLive = true;
        catTaleAnim = GetComponentInChildren<Animator>();
        pirateFace = GameObject.Find("CatFace").GetComponent<PirateFace>();    
    }

    void Update()
    {
        if(pirateFace.pirateFaceHp<=0)
        {
            Explosion();
            catTaleAnim.SetInteger("Face'sHp", pirateFace.pirateFaceHp);
        }
    }

    void Explosion()
    {//해적선 꼬리 터지는 이펙트
        if(isLive)
        {
            explosion = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(explosion, 1f);
            isLive = false;
        }
    }
}
