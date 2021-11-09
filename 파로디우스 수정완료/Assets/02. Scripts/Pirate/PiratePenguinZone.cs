using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiratePenguinZone : MonoBehaviour
{//펭귄을 생성시키는 위치 ( 해적선 몸통 )
    Transform PenguinZone;          //펭귄 생성 지역

    ObjPoolingMgr ObjPoolingMgr;    //오브젝트 풀
    GameObject objBluePenguin;      //오브젝트 풀의 파란펭귄 오브젝트

    public string[] penguins;       //오브젝트 풀에서 사용할 펭귄 이름
    public string[] penguinsBullet; //오브젝트 풀에서 사용할 펭귄 불렛

    float pirateOn;                 //해적선 생성 여부
    float Delay;                    //생성 주기
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
    void Penguinmake()//오브젝트 풀에서 펭귄을 불러옴
    {
        objBluePenguin = ObjPoolingMgr.MakeObj(penguins[0]);
        if (objBluePenguin != null)
        {
            objBluePenguin.transform.position = PenguinZone.transform.position;
        }
    }
}
