using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTale : MonoBehaviour
{//������ ����
    public GameObject explosion; //���� ������ ����Ʈ

    Animator catTaleAnim; //������ ����

    PirateFace pirateFace;//������ ��

    bool isLive;          //���� ����


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
    {//������ ���� ������ ����Ʈ
        if(isLive)
        {
            explosion = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(explosion, 1f);
            isLive = false;
        }
    }
}
