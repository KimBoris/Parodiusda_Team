using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public StageManager stageManager;

    public GameObject Player;

    public GameObject AdultChickPrefeb;
    public GameObject BabyChickPrefeb;
    public GameObject CrabPrefeb;
    public GameObject PenguinPrefeb;
    public GameObject MoaiGroupPrefeb1;
    public GameObject MoaiGroupPrefeb2;
    public GameObject BeePrefeb;
    public GameObject NiddlePrefeb;
    public GameObject DolphinPrefeb;

    public float adultchickRandY = 0;
    public float babychickRandY = 0;

    public float monsterMakeTimer;

    float adultChickmakeDelay;
    float babyChickmakeDelay;
    float moaimakeDelay;
    float niddlemakeDelay;
    float crabmakeDelay;
    float penguinDelay;
    float dolphinDelay;

    public int moaimakeLimit;
    int niddlemakeLimit;
    int adultchickLimit;
    int babychickLimit;
    int crabLimit;
    int penguinLimit;
    int dolphinLimit;

    float moaiwaitTime;
    float niddlewaitTime;
    float adultwaitTime;
    float babywaitTime;
    float crabwatiTime;
    float penguinwaitTime;
    float dolphinwatiTime;

    private void OnEnable()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        Player = GameObject.FindWithTag("Player");

        adultChickmakeDelay = 5f;
        babyChickmakeDelay = 2f;
        moaimakeDelay = 3f;
        niddlemakeDelay = 3f;
        crabmakeDelay = 1f;
        penguinDelay = 3f;
        dolphinDelay = 1f;

        moaimakeLimit = 0;
        niddlemakeLimit = 0;
        adultchickLimit = 0;
        babychickLimit = 0;
        crabLimit = 0;
        dolphinLimit = 0;
    }

    void Update()
    {
        Player = GameObject.FindWithTag("Player");
        monsterMakeTimer = stageManager.timer;
        MakeMonster();
    }

    public void MakeMonster()
    {
        //시작하고 5초이후 3초간격으로 모아이가 4회 생성
        if (monsterMakeTimer > 2.0f)    //2+3초 뒤에 생기도록 조절
        {
            moaiwaitTime += Time.deltaTime;

            if (moaiwaitTime >= moaimakeDelay && moaimakeLimit < 4)
            {
                moaiwaitTime -= moaimakeDelay;

                if (moaimakeLimit % 2 == 0)
                { Instantiate(MoaiGroupPrefeb1, new Vector3(10f, 3.6f, 0), Quaternion.identity); }

                else if (moaimakeLimit % 2 == 1)
                { Instantiate(MoaiGroupPrefeb2, new Vector3(10f, -3.6f, 0), Quaternion.identity); }
                moaimakeLimit++;
            }

            else if (moaiwaitTime >= moaimakeDelay && monsterMakeTimer > 22f && moaimakeLimit < 6)
            {
                moaiwaitTime -= moaimakeDelay;

                if (moaimakeLimit % 2 == 0)
                { Instantiate(MoaiGroupPrefeb1, new Vector3(10f, -3.6f, 0), Quaternion.identity); }

                else if (moaimakeLimit % 2 == 1)
                { Instantiate(MoaiGroupPrefeb2, new Vector3(10f, 3.6f, 0), Quaternion.identity); }
                moaimakeLimit++;
            }

            else if (moaiwaitTime >= moaimakeDelay && monsterMakeTimer > 27f && moaimakeLimit < 8)
            {
                moaiwaitTime -= moaimakeDelay;

                if (moaimakeLimit % 2 == 0)
                { Instantiate(MoaiGroupPrefeb1, new Vector3(10f, -3.6f, 0), Quaternion.identity); }

                else if (moaimakeLimit % 2 == 1)
                { Instantiate(MoaiGroupPrefeb2, new Vector3(10f, 3.6f, 0), Quaternion.identity); }
                moaimakeLimit++;
            }
        }

        //시작하고 12+3초 이후 주사기 생성
        if (monsterMakeTimer > 12.0f)
        {
            niddlewaitTime += Time.deltaTime;

            if (niddlewaitTime >= niddlemakeDelay && niddlemakeLimit < 3)
            {
                niddlewaitTime -= niddlemakeDelay;
                Instantiate(NiddlePrefeb, new Vector3(8f, 1.5f, 0), Quaternion.identity);
                Instantiate(NiddlePrefeb, new Vector3(8f, 0, 0), Quaternion.identity);
                Instantiate(NiddlePrefeb, new Vector3(8f, -1.5f, 0), Quaternion.identity);
                niddlemakeLimit++;
            }

            else if (niddlewaitTime >= niddlemakeDelay && monsterMakeTimer > 27f && niddlemakeLimit < 2)
            {
                niddlewaitTime -= niddlemakeDelay;
                Instantiate(NiddlePrefeb, new Vector3(8f, 1.5f, 0), Quaternion.identity);
                Instantiate(NiddlePrefeb, new Vector3(8f, 0, 0), Quaternion.identity);
                Instantiate(NiddlePrefeb, new Vector3(8f, -1.5f, 0), Quaternion.identity);
                niddlemakeLimit++;
            }
        }

        //닭 생성
        if (monsterMakeTimer > 27.0f && monsterMakeTimer < 90.0f)
        {
            adultwaitTime += Time.deltaTime;

            if (adultwaitTime >= adultChickmakeDelay && adultchickLimit < 2)
            {
                adultwaitTime -= adultChickmakeDelay;
                adultchickRandY = Random.Range(-2f, 2f);
                Instantiate(AdultChickPrefeb, new Vector3(8f, adultchickRandY - 1, 0), Quaternion.identity);
                Instantiate(AdultChickPrefeb, new Vector3(8f, adultchickRandY + 1, 0), Quaternion.identity);
                adultchickLimit++;
            }
        }

        if (monsterMakeTimer > 30.0f)
        {
            //병아리
            babywaitTime += Time.deltaTime;

            if (babywaitTime >= babyChickmakeDelay && babychickLimit < 2)
            {
                babywaitTime -= babyChickmakeDelay;
                babychickRandY = Random.Range(-2f, 2f);
                Instantiate(BabyChickPrefeb, new Vector3(8f, babyChickmakeDelay + 1, 0), Quaternion.identity);
                Instantiate(BabyChickPrefeb, new Vector3(8f, babyChickmakeDelay, 0), Quaternion.identity);
                Instantiate(BabyChickPrefeb, new Vector3(8f, babyChickmakeDelay - 1, 0), Quaternion.identity);
                babychickLimit++;
            }

            if (babywaitTime >= babyChickmakeDelay && monsterMakeTimer > 55.0f && babychickLimit < 7)
            {
                babywaitTime -= babyChickmakeDelay;
                babychickRandY = Random.Range(-2f, 2f);
                Instantiate(BabyChickPrefeb, new Vector3(8f, babyChickmakeDelay + 1, 0), Quaternion.identity);
                Instantiate(BabyChickPrefeb, new Vector3(8f, babyChickmakeDelay, 0), Quaternion.identity);
                Instantiate(BabyChickPrefeb, new Vector3(8f, babyChickmakeDelay - 1, 0), Quaternion.identity);
                babychickLimit++;
            }
        }

        //게 생성
        if (monsterMakeTimer > 45.0f)
        {
            crabwatiTime += Time.deltaTime;
            if (crabwatiTime >= crabmakeDelay && crabLimit < 3)
            {
                crabwatiTime -= crabmakeDelay;
                Instantiate(CrabPrefeb, new Vector3(8f, -3.6f, 0), Quaternion.identity);
                crabLimit++;
            }

            else if (crabwatiTime >= crabmakeDelay && monsterMakeTimer > 50.0f && crabLimit < 5)
            {
                crabwatiTime -= crabmakeDelay;
                Instantiate(CrabPrefeb, new Vector3(8f, 3.6f, 0), Quaternion.identity);
                crabLimit++;
            }

            else if(crabwatiTime >= crabmakeDelay && monsterMakeTimer > 70.0f && crabLimit < 7)
            {
                crabwatiTime -= crabmakeDelay;
                Instantiate(CrabPrefeb, new Vector3(8f, -3.6f, 0), Quaternion.identity);
                crabLimit++;
            }

            else if (crabwatiTime >= crabmakeDelay && monsterMakeTimer > 75.0f && crabLimit < 9)
            {
                crabwatiTime -= crabmakeDelay;
                Instantiate(CrabPrefeb, new Vector3(8f, 3.6f, 0), Quaternion.identity);
                crabLimit++;
            }
        }

        if (monsterMakeTimer > 49f)  //돌고래 생성
        {
            dolphinwatiTime += Time.deltaTime;
            if (dolphinwatiTime >= dolphinDelay && dolphinLimit < 3)
            {
                dolphinwatiTime -= dolphinDelay;
                Instantiate(DolphinPrefeb, new Vector3(6.5f, -2f, 0), Quaternion.identity);
                dolphinLimit++;
            }

            else if (dolphinwatiTime >= dolphinDelay && monsterMakeTimer > 52f && dolphinLimit < 6)
            {
                dolphinwatiTime -= dolphinDelay;
                Instantiate(DolphinPrefeb, new Vector3(6.5f, -2f, 0), Quaternion.identity);
                dolphinLimit++;
            }
        }
/*
        //펭귄 생성
        if (monsterMakeTimer > 42.0f)
        {
            penguinwaitTime += Time.deltaTime;

            if (penguinwaitTime >= penguinDelay && penguinLimit < 2)
            {
                if (penguinLimit % 2 == 0)
                {
                    crabwatiTime -= crabmakeDelay;
                    Instantiate(PenguinPrefeb, new Vector3(-8f, -3.6f, 0), Quaternion.identity);
                    penguinLimit++;
                }
                else if (monsterMakeTimer > 53.0f)
                {
                    crabwatiTime -= crabmakeDelay;
                    Instantiate(PenguinPrefeb, new Vector3(-8f, 3.6f, 0), Quaternion.identity);
                    penguinLimit++;
                }
            }
        }
*/
    }
}
