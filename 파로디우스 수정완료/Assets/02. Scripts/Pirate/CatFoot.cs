using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFoot : MonoBehaviour
{
    public GameObject FootExplosionEff;
   
    PirateFace pirateFace;
    
    int i;
    int index;  
    
    bool isLive;//해적선 생사여부
    
    Transform[] footPos = new Transform[3];//해적선 발의 위치
    
    void Awake()
    {
        isLive = true;
        i = 0;
        index = 3;
        pirateFace = GameObject.Find("CatFace").GetComponent<PirateFace>();

    }

    private void Update()
    {
        if (pirateFace.pirateFaceHp <= 0)
        {
            for (i = 0; i < index; i++)
            {
                footPos[i] = transform.GetChild(i);
                Explosion();
                this.transform.GetChild(i).GetComponent<Animator>().SetInteger("Face'sHp", pirateFace.pirateFaceHp);
            }
            isLive = false;
        }
    }

    void Explosion()//해적선 죽을때 발이 터지는 애니메이션
    {
        if (isLive)
        {
            FootExplosionEff = Instantiate(FootExplosionEff, footPos[i].position, Quaternion.identity);
            Destroy(FootExplosionEff, 0.7f);
        }
    }
}
