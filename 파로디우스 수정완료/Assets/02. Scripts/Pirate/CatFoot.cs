using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFoot : MonoBehaviour
{
    public GameObject FootExplosionEff;
   
    PirateFace pirateFace;
    
    int i;
    int index;  
    
    bool isLive;//������ ���翩��
    
    Transform[] footPos = new Transform[3];//������ ���� ��ġ
    
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

    void Explosion()//������ ������ ���� ������ �ִϸ��̼�
    {
        if (isLive)
        {
            FootExplosionEff = Instantiate(FootExplosionEff, footPos[i].position, Quaternion.identity);
            Destroy(FootExplosionEff, 0.7f);
        }
    }
}
