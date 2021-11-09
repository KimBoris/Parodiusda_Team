using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_MoaiCheck : MonoBehaviour    //모아이를 그룹화 할 코드
{
    public E_MoaiCtrl moaiCtrl;             //모아이 개별을 컨트롤하는 코드의 변수
    public GameObject ItemPrefeb;           //모아이 1세트를 쓰러뜨리고 생성할 아이템

    public int itemLimit;                   //아이템 갯수 제한
    public int attackCount;

    private void OnEnable()
    {
        for (int i = 0; i < 5; i++)
        { moaiCtrl = GameObject.Find("Moai" + i).GetComponent<E_MoaiCtrl>(); }
        attackCount = 0;
        itemLimit = 0;
    }
    //void Start()
    //{
    //    for (int i = 0; i < 5; i++)
    //    { moaiCtrl = GameObject.Find("Moai" + i).GetComponent<E_MoaiCtrl>(); }
    //    attackCount = 0;
    //    itemLimit = 0;
    //}

    void Update()
    {
        if (gameObject.transform.childCount <= 0)
        {
            if (attackCount >= 4)
            {
                if (this.gameObject.name == "MoaiGroup1(Clone)")
                {
                    Instantiate(ItemPrefeb, moaiCtrl.contactPoint1, Quaternion.identity);
                    Destroy(gameObject);
                }
               if (this.gameObject.name == "MoaiGroup2(Clone)")
                {
                    Instantiate(ItemPrefeb, moaiCtrl.contactPoint2, Quaternion.identity);
                    Debug.Log("moaiCtrl.contactPoint2 : " + moaiCtrl.contactPoint2);
                    Destroy(gameObject);
                }
            }

            else
                Destroy(gameObject);
        }
    }
}
