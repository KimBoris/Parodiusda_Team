using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiratePenguinZone : MonoBehaviour
{//����� ������Ű�� ��ġ ( ������ ���� )
    Transform PenguinZone;          //��� ���� ����

    ObjPoolingMgr ObjPoolingMgr;    //������Ʈ Ǯ
    GameObject objBluePenguin;      //������Ʈ Ǯ�� �Ķ���� ������Ʈ

    public string[] penguins;       //������Ʈ Ǯ���� ����� ��� �̸�
    public string[] penguinsBullet; //������Ʈ Ǯ���� ����� ��� �ҷ�

    float pirateOn;                 //������ ���� ����
    float Delay;                    //���� �ֱ�
    private void OnEnable()
    {
        penguins = new string[] { "PiratePenguin" };
        Delay = 0;
        pirateOn = 0;
        PenguinZone = GameObject.Find("PirateShip(Clone)").GetComponentInChildren<Transform>();
        ObjPoolingMgr = GameObject.Find("ObjPoolingManager").GetComponent<ObjPoolingMgr>();
    }
    void Update()
    {
        Delay += Time.deltaTime;
        if (Delay > 2)
        {
            Penguinmake();
            Delay = 0;
        }
    }
    void Penguinmake()//������Ʈ Ǯ���� ����� �ҷ���
    {
        objBluePenguin = ObjPoolingMgr.MakeObj(penguins[0]);
        if (objBluePenguin != null)
        {
            objBluePenguin.transform.position = PenguinZone.transform.position;
        }
    }
}
