using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinimeAttack : MonoBehaviour
{
    public GameObject Boss;     //보스오브젝트 추적을 위해서

    public ObjPoolingMgr objPoolingMgr; //오브젝트 풀링 매니저

    public string[] minimeBullets;//오브젝트 풀링에서 사용할 미니미 불렛명

    float fireDelay; //총알 발사 딜레이
    float fireTime;  //총알 발사 시간
   
    BossMove bossmove;

    GameObject boss;
    GameObject minimeBullet; 
   
    private void OnEnable()
    {
        fireTime = 0;
        fireDelay = 6;

        minimeBullets = new string[] { "BossMinimeBullet" };
    }
    void Update()
    {
        if (Boss != null)
        {
            objPoolingMgr = GameObject.Find("ObjPoolingManager").GetComponent<ObjPoolingMgr>();
            bossmove = GameObject.Find("Boss(Clone)").GetComponent<BossMove>();
            boss = GameObject.Find("Boss(Clone)").GetComponent<GameObject>();
        }
        
        // 미니미 총알 발사(생성)
        if (bossmove.isBossLive == true)
        {
            setBullet();//미니미 불렛 발사 함수
        }
    }
    void setBullet()//미니미 불렛 발사
    {
        fireTime += Time.deltaTime;
        if (fireTime > fireDelay)
        {
            minimeBullet = objPoolingMgr.MakeObj(minimeBullets[0]);
            minimeBullet.transform.position = this.transform.position;
            fireTime = 0;
        }
    }
   
}
