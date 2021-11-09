using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManagerKBK : MonoBehaviour
{


    public GameObject BottomBluePenguin;
    public GameObject TopRedPenguin;
    public GameObject PingPong;
    public int PenguinCount; //몬스터가 여러개 생성되는 것을 방지하기 위해
    public float SpawnTime;
    public int pingpongCount;
    private void Awake()
    {
        PenguinCount = 0;
    }
    void Update()
    {
        SpawnTime += Time.deltaTime;

        MakeEnemy();
    }

    public void MakeEnemy()
    {

        //펭귄 소환
        if (PenguinCount == 0 && SpawnTime > 42)
        {
            Instantiate(BottomBluePenguin, new Vector3(-6.5f, -3.3f, 0), Quaternion.identity);
            PenguinCount++;
        }
        else if (PenguinCount == 1 && SpawnTime > 60)
        {
            Instantiate(TopRedPenguin, new Vector3(-6.5f, 3.7f, 0), Quaternion.Euler(180,0,0));
            PenguinCount++;
        }
        else if (PenguinCount == 2 && SpawnTime > 74)
        {
            Instantiate(BottomBluePenguin, new Vector3(-6.5f, -3.4f, 0), Quaternion.identity);
            PenguinCount++;
        }
        else if (PenguinCount == 2 && SpawnTime > 80)
        {
            Instantiate(BottomBluePenguin, new Vector3(-6.5f, -3.4f, 0), Quaternion.identity);
            PenguinCount++;
        }

        //핑퐁 소환
        if (pingpongCount == 0 && SpawnTime > 61)
        {
            Instantiate(PingPong, new Vector3(7, -1.7f, 0), Quaternion.identity);
            pingpongCount++;
        }
        if (pingpongCount == 1 && SpawnTime > 70)
        {
            Instantiate(PingPong, new Vector3(7, -1.7f, 0), Quaternion.identity);
            pingpongCount++;
        }
        if (pingpongCount == 2 && SpawnTime > 73)
        {
            Instantiate(PingPong, new Vector3(7, -1.7f, 0), Quaternion.identity);
            pingpongCount++;
        }
    }
}
