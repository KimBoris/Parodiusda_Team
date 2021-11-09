using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pirateMove : MonoBehaviour
{
    public int movePattern; //Pirate움직임 패턴

    float patternDelay;     //movePattern을 관리하는 외부 딜레이
    float pirateSpeed;      //Pirate속도값
    float delay;            //Pirate패턴 내부 딜레이

    bool isPirateLive;      //해적선 생사여부

    Vector3 pirateDir;      //Pirate방향

    private void OnEnable()
    {
        patternDelay = 0;
        movePattern = 0;
        pirateSpeed = 2f;
        pirateDir = Vector3.up;
        isPirateLive = true;
    }
    private void Update()
    {
        if (isPirateLive)
        {
            patternDelay += Time.deltaTime;
            switch (movePattern) //Pirate움직임 시간과 벡터방향값으로 제어
            {
                case 0:
                    if (patternDelay < 5)
                    {
                        Pattern0();
                    }
                    else
                    {
                        movePattern = 1;
                    }
                    break;
                case 1:
                    if (patternDelay < 6.5f)
                    {
                        Pattern1();
                    }
                    else
                    {
                        movePattern = 2;
                    }
                    break;
                case 2:
                    if (patternDelay < 7.5f)
                    {
                        Pattern2();
                    }
                    else
                    {
                        movePattern = 3;
                    }
                    break;
                case 3:
                    Pattern3();
                    break;
                case 4:
                    if (patternDelay < 10.5f)
                    {
                        Pattern4();
                    }
                    else
                    {
                        movePattern = 5;
                    }
                    break;
                case 5:
                    if (patternDelay < 12.5f)
                    {
                        Pattern5();
                    }
                    else
                    {
                        movePattern = 6;
                    }
                    break;
                case 6:
                    if (patternDelay < 14.5f)
                    {
                        Pattern6();
                    }
                    else
                    {
                        movePattern = 7;
                    }
                    break;
                case 7:
                    Pattern7();
                    break;
                case 8:
                    if (patternDelay < 16.5f)
                    {
                        Pattern8();
                    }
                    else
                    {
                        movePattern = 9;
                    }
                    break;
                case 9:
                    if (patternDelay < 18.5f)
                    {
                        Pattern9();
                    }
                    else
                    {
                        pattern10();
                    }
                    break;
                case 10:
                    pattern10();
                    break;
                case 11:
                    Pattern11();
                    if (this.transform.position.x < -12)
                    {
                        Destroy(this.gameObject);
                        isPirateLive = false;
                    }
                    break;
            }
        }
        else
        {
            return;
        }
    }



    //[해적선 이동 패턴]
    void Pattern0()
    {
        pirateDir = new Vector3(0.35f, 0.9f, 0);
        this.transform.position += pirateDir * pirateSpeed * Time.deltaTime;
    }
    void Pattern1()
    {
        pirateDir = new Vector3(0.25f, 0.325f, 0);
        this.transform.position += pirateDir * pirateSpeed * Time.deltaTime;
    }
    void Pattern2()
    {
        pirateDir = new Vector3(0.25f, 0, 0);
        this.transform.position += pirateDir * pirateSpeed * Time.deltaTime;
    }
    void Pattern3()
    {
        pirateDir = new Vector3(0.8f, 0, 0);
        this.transform.position -= pirateDir * pirateSpeed * Time.deltaTime;
        delay += Time.deltaTime;
        if (delay > 2)
        {
            movePattern = 4;
            delay = 0;
        }
    }
    void Pattern4()
    {
        pirateDir = new Vector3(0.95f, 0, 0);
        this.transform.position += pirateDir * pirateSpeed * Time.deltaTime;
    }
    void Pattern5()
    {
        pirateDir = new Vector3(0.5f, -0.8f, 0);
        this.transform.position += pirateDir * pirateSpeed * Time.deltaTime;
    }
    void Pattern6()
    {
        pirateDir = new Vector3(-0.8f, -0.4f, 0);
        this.transform.position += pirateDir * pirateSpeed * Time.deltaTime;
    }
    void Pattern7()
    {
        delay += Time.deltaTime;
        pirateDir = new Vector3(-0.8f, 0, 0);
        this.transform.position += pirateDir * pirateSpeed * Time.deltaTime;
        if (delay > 1f)
        {
            movePattern = 8;
            delay = 0;
        }
    }
    void Pattern8()
    {
        pirateDir = new Vector3(1.5f, 1.7f, 0);
        this.transform.position += pirateDir * pirateSpeed * Time.deltaTime;
    }
    void Pattern9()
    {
        pirateDir = new Vector3(0.75f, 0, 0);
        this.transform.position += pirateDir * pirateSpeed * Time.deltaTime;
    }
    void pattern10()
    {
        pirateDir = new Vector3(0f, 0, 0);
        this.transform.position += pirateDir * pirateSpeed * Time.deltaTime;
        delay += Time.deltaTime;
        if (delay > 4)
        {
            movePattern = 11;
        }
    }
    void Pattern11()
    {
        pirateDir = new Vector3(-2f, 0, 0);
        this.transform.position += pirateDir * pirateSpeed * Time.deltaTime;
    }
}