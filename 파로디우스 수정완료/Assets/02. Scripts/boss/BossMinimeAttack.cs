using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinimeAttack : MonoBehaviour
{
    public GameObject Boss;     //����������Ʈ ������ ���ؼ�

    public ObjPoolingMgr objPoolingMgr; //������Ʈ Ǯ�� �Ŵ���

    public string[] minimeBullets;//������Ʈ Ǯ������ ����� �̴Ϲ� �ҷ���

    float fireDelay; //�Ѿ� �߻� ������
    float fireTime;  //�Ѿ� �߻� �ð�
   
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
        
        // �̴Ϲ� �Ѿ� �߻�(����)
        if (bossmove.isBossLive == true)
        {
            setBullet();//�̴Ϲ� �ҷ� �߻� �Լ�
        }
    }
    void setBullet()//�̴Ϲ� �ҷ� �߻�
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
