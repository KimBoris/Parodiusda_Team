using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    float bossSpawnTime;//보스등장시간
    public GameObject Boss;
    public GameObject Pirate;
    bool ismakeBoss;
    bool ismakePirate;
    private void Awake()
    {
        ismakeBoss = false;
        ismakePirate = false;
    }
    void Update()
    {
        bossSpawnTime += Time.deltaTime;
        MakeBoss();
        MakePirate();


        //if (ismakeBoss == false)
        //{
        //    if (bossSpawnTime >= 135)
        //    {
        //        ismakeBoss = true;
        //        Instantiate(Boss, new Vector3(3.1f, -10, 0), Quaternion.identity);
        //    }
        //}

            //if (ismakePirate == false)
            //{
            //    if (bossSpawnTime >= 91)
            //    {
            //        ismakePirate = true;
            //        Instantiate(Pirate, new Vector3(-1, -8.5f, 0), Quaternion.identity);
            //    }
            //}


    }

    public void MakeBoss()
    {
        if (ismakeBoss == false && bossSpawnTime >= 125 || Input.GetKeyDown(KeyCode.Alpha2))
        {
            ismakeBoss = true;
            Instantiate(Boss, new Vector3(3.1f, -10, 0), Quaternion.identity);
        }
    }

    

    public void MakePirate()
    {
        if (ismakePirate == false && bossSpawnTime >= 91 || Input.GetKeyDown(KeyCode.Alpha1))
        {
            ismakePirate = true;
            Instantiate(Pirate, new Vector3(-1, -8.5f, 0), Quaternion.identity);
        }
    }
}
