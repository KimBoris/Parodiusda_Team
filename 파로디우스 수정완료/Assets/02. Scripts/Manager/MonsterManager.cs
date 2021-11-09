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
    float dolphinDelay;

    public int moaimakeLimit;
    int niddlemakeLimit;
    int adultchickLimit;
    int babychickLimit;
    int crabLimit;
    int dolphinLimit;

    float moaiwaitTime;
    float niddlewaitTime;
    float adultwaitTime;
    float babywaitTime;
    float crabwatiTime;
    float dolphinwatiTime;

    bool minuteLoop1 = false;
    bool minuteLoop2 = false;

    private void OnEnable()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        Player = GameObject.FindWithTag("Player");

        adultChickmakeDelay = 5f;
        babyChickmakeDelay = 2f;
        moaimakeDelay = 3f;
        niddlemakeDelay = 3f;
        crabmakeDelay = 2f;
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
        monsterMakeTimer += Time.deltaTime;

        if (monsterMakeTimer > 59.0f && monsterMakeTimer < 60.0f)
        { minuteLoop1 = true; }

        MakeMonster();
    }

    public void MakeMonster()
    {     
            //모아이 생성
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

            //주사기 생성
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

                if (adultwaitTime >= adultChickmakeDelay)
                {
                    adultwaitTime -= adultChickmakeDelay;
                    adultchickRandY = Random.Range(-2f, 2f);
                    Instantiate(AdultChickPrefeb, new Vector3(8f, adultchickRandY - 1, 0), Quaternion.identity);
                    Instantiate(AdultChickPrefeb, new Vector3(8f, adultchickRandY + 1, 0), Quaternion.identity);
                }
            }

            if (monsterMakeTimer > 30.0f)
            {
                //병아리
                babywaitTime += Time.deltaTime;

                if (babywaitTime >= babyChickmakeDelay && monsterMakeTimer < 90.0f)//&& babychickLimit < 2)
                {
                    babywaitTime -= babyChickmakeDelay;
                    babychickRandY = Random.Range(-2f, 2f);
                    Instantiate(BabyChickPrefeb, new Vector3(8f, babyChickmakeDelay + 1, 0), Quaternion.identity);
                    Instantiate(BabyChickPrefeb, new Vector3(8f, babyChickmakeDelay, 0), Quaternion.identity);
                    Instantiate(BabyChickPrefeb, new Vector3(8f, babyChickmakeDelay - 1, 0), Quaternion.identity);
                    //babychickLimit++;
                }
            }

            //게 생성
            if (monsterMakeTimer > 35.0f)
            {
                crabwatiTime += Time.deltaTime;

                if (crabwatiTime >= crabmakeDelay && crabLimit < 3)
                {
                    crabwatiTime -= crabmakeDelay;
                    Instantiate(CrabPrefeb, new Vector3(8f, -3.6f, 0), Quaternion.identity);
                    crabLimit++;
                }

                if (crabwatiTime >= crabmakeDelay && monsterMakeTimer > 40.0f && monsterMakeTimer < 90.0f)
                {
                    crabwatiTime -= crabmakeDelay;
                    crabmakeDelay = 5f;
                    if (crabLimit % 2 == 0)
                        Instantiate(CrabPrefeb, new Vector3(8f, 3.6f, 0), Quaternion.identity);
                    else if (crabLimit % 2 == 1)
                        Instantiate(CrabPrefeb, new Vector3(8f, -3.6f, 0), Quaternion.identity);
                    crabLimit++;
                }

            }

            if (monsterMakeTimer > 49f)  //돌고래 생성
            {
                dolphinwatiTime += Time.deltaTime;

                if (dolphinwatiTime >= dolphinDelay && dolphinLimit < 3)
                {
                    dolphinwatiTime -= dolphinDelay;
                    Instantiate(DolphinPrefeb, new Vector3(9.1f, -6f, 0), Quaternion.identity);
                    dolphinLimit++;
                }

                else if (dolphinwatiTime >= dolphinDelay && monsterMakeTimer > 52f && dolphinLimit < 6)
                {
                    dolphinwatiTime -= dolphinDelay;
                    Instantiate(DolphinPrefeb, new Vector3(9.1f, -6f, 0), Quaternion.identity);
                    dolphinLimit++;
                }
            }
        }
/*
        if (minuteLoop1 == true && minuteLoop2==false)
        {
            if (monsterMakeTimer > 0.0f)
            {
                babywaitTime += Time.deltaTime;
                crabwatiTime += Time.deltaTime;
                adultwaitTime += Time.deltaTime;

                //병아리
                if (babywaitTime >= babyChickmakeDelay && monsterMakeTimer < 30.0f)
                {
                    babywaitTime -= babyChickmakeDelay;
                    babychickRandY = Random.Range(-2f, 2f);
                    Instantiate(BabyChickPrefeb, new Vector3(8f, babyChickmakeDelay + 1, 0), Quaternion.identity);
                    Instantiate(BabyChickPrefeb, new Vector3(8f, babyChickmakeDelay, 0), Quaternion.identity);
                    Instantiate(BabyChickPrefeb, new Vector3(8f, babyChickmakeDelay - 1, 0), Quaternion.identity);
                }

                //게
                if (crabwatiTime >= crabmakeDelay && monsterMakeTimer < 30.0f)
                {
                    crabwatiTime -= crabmakeDelay;
                    crabmakeDelay = 5f;
                    if (crabLimit % 2 == 0)
                        Instantiate(CrabPrefeb, new Vector3(8f, 3.6f, 0), Quaternion.identity);
                    else if (crabLimit % 2 == 1)
                        Instantiate(CrabPrefeb, new Vector3(8f, -3.6f, 0), Quaternion.identity);
                    crabLimit++;
                }

                //닭
                if (adultwaitTime >= adultChickmakeDelay && monsterMakeTimer < 30.0f)
                {
                    adultwaitTime -= adultChickmakeDelay;
                    adultchickRandY = Random.Range(-2f, 2f);
                    Instantiate(AdultChickPrefeb, new Vector3(8f, adultchickRandY - 1, 0), Quaternion.identity);
                    Instantiate(AdultChickPrefeb, new Vector3(8f, adultchickRandY + 1, 0), Quaternion.identity);
                }
            }
            else if(monsterMakeTimer>=30.0f)
            { minuteLoop2 = true; }
        }

    */
    }

