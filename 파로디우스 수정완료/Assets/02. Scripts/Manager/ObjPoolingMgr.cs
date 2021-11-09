using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolingMgr : MonoBehaviour
{
    public GameObject Boss;
    public GameObject Pirate;


    public GameObject bossSubPrefab;
    public GameObject PantarouBulletNormalPrefab;  //펭타로 기본 총알 프리펩
    public GameObject PantarouBulletDoublePrefab;  //펭타로 더블 총알 프리펩
    public GameObject PantarouBulletSpreadPrefab;  //펭타로 특수 총알 프리펩
    public GameObject PantarouBulletPotonPrefab;  //펭타로 서브웨폰 프리펩
    public GameObject PantarouBulletSpreadBombPrefab;  //펭타로 특수무기 폭발 프리펩
    public GameObject TacoBulletNormalPrefab;  //타코 기본 총알 프리펩
    public GameObject TacoBulletDoublePrefab;  //타코 더블 총알 프리펩
    public GameObject TacoBulletRipplePrefab;  //타코 특수 총알 프리펩
    public GameObject TacoBulletUpTwoWayPrefab;  //타코 서브웨폰1 프리펩
    public GameObject TacoBulletDownTwoWayPrefab;  //타코 서브웨폰2 프리펩

    public GameObject piratePenguinBulletPrefab;
    public GameObject piratePenguinPrefab;
    public GameObject bulletHitEffectPrefab;
    public GameObject bossMinimeBulletPrefab;
    public GameObject pirateBulletPrefab;

    public GameObject itemWeaponPrefab;  //무기 아이템 프리펩

    /*
    public GameObject BabyChickPrefeb;
    public GameObject AdultChickPrefeb;
    public GameObject MoaiPrefeb;

    GameObject[] EBabyChick;
    GameObject[] EAdultChick;
    GameObject[] EMoai;
    */

    GameObject[] minime;

    GameObject[] PantarouBulletNormal;   //펭타로 기본 총알을 넣을 게임오브젝트 배열
    GameObject[] PantarouBulletDouble;   //펭타로 더블 총알을 넣을 게임오브젝트 배열
    GameObject[] PantarouBulletSpread;   //펭타로 특수 총알을 넣을 게임오브젝트 배열
    GameObject[] PantarouBulletPoton;   //펭타로 서브웨폰을 넣을 게임오브젝트 배열
    GameObject[] PantarouBulletSpreadBomb;   //펭타로 특수 총알 폭발효과를 넣을 게임오브젝트 배열
    GameObject[] TacoBulletNormal;  //타코 기본 총알을 넣을 게임오브젝트 배열
    GameObject[] TacoBulletDouble;  //타코 더블 총알을 넣을 게임오브젝트 배열
    GameObject[] TacoBulletRipple;  //타코 특수 총알을 넣을 게임오브젝트 배열
    GameObject[] TacoBulletUpTwoWay;  //타코 서브웨폰1을 넣을 게임오브젝트 배열
    GameObject[] TacoBulletDownTwoWay;  //타코 서브웨폰2를 넣을 게임오브젝트 배열

    GameObject[] PiratePenguinBullet;
    GameObject[] bulletHitEffect;
    GameObject[] bossMinimeBullet;
    GameObject[] pirateBullet;
    GameObject[] piratePenguin;
    GameObject[] targetPool;

    GameObject[] itemWeapon;  //무기 아이템을 넣을 게임오브젝트 배열

    void Awake()
    {


        minime = new GameObject[16];  //보스가 소환하는 미니펭귄

        /*
        EBabyChick = new GameObject[10];  //병아리
        EAdultChick = new GameObject[5]; //닭
        EMoai = new GameObject[10]; //모아이
        */

        PantarouBulletNormal = new GameObject[50];   //펭타로 기본 무기
        PantarouBulletDouble = new GameObject[50];   //펭타로 더블 무기
        PantarouBulletSpread = new GameObject[50];   //펭타로 스프레드건 무기
        PantarouBulletPoton = new GameObject[50];    //펭타로 서브웨폰 포톤
        PantarouBulletSpreadBomb = new GameObject[50];  //펭타로 스프레드건 폭발효과
        TacoBulletNormal = new GameObject[50];     //타코 기본 무기
        TacoBulletDouble = new GameObject[50];     //타코 더블 무기
        TacoBulletRipple = new GameObject[50];     //타코 리플 무기
        TacoBulletUpTwoWay = new GameObject[50];   //타고 서브웨폰 상단
        TacoBulletDownTwoWay = new GameObject[50]; //타코 서브웨폰 하단

        bossMinimeBullet = new GameObject[50];
        bulletHitEffect = new GameObject[100];
        pirateBullet = new GameObject[40];
        PiratePenguinBullet = new GameObject[20];
        piratePenguin = new GameObject[10];
        itemWeapon = new GameObject[10];  //무기아이템
        Generate();
    }

    void Generate()
    {
        //#1. Enemy
        if (Boss != null)
        {
            for (int index = 0; index < minime.Length; index++)
            {
                minime[index] = Instantiate(bossSubPrefab);
                minime[index].SetActive(false);
            }
            for (int index = 0; index < bossMinimeBullet.Length; index++)
            {
                bossMinimeBullet[index] = Instantiate(bossMinimeBulletPrefab);
                bossMinimeBullet[index].SetActive(false);
            }
        }
        if (Pirate != null)
        {
            for (int index = 0; index < piratePenguin.Length; index++)
            {
                piratePenguin[index] = Instantiate(piratePenguinPrefab);
                piratePenguin[index].SetActive(false);
            }
            for (int index = 0; index < pirateBullet.Length; index++)
            {
                pirateBullet[index] = Instantiate(pirateBulletPrefab);
                pirateBullet[index].SetActive(false);
            }
            for (int index = 0; index < PiratePenguinBullet.Length; index++)
            {
                PiratePenguinBullet[index] = Instantiate(piratePenguinBulletPrefab);
                PiratePenguinBullet[index].SetActive(false);
            }
        }
        /*      
                for (int index = 0; index < EBabyChick.Length; index++)     //병아리
                {
                    EBabyChick[index] = Instantiate(BabyChickPrefeb);
                    EBabyChick[index].SetActive(false);
                }
                for (int index = 0; index < EAdultChick.Length; index++)    //닭
                {
                    EAdultChick[index] = Instantiate(AdultChickPrefeb);
                    EAdultChick[index].SetActive(false);
                }
                for (int index = 0; index < EMoai.Length; index++)          //모아이
                {
                    EMoai[index] = Instantiate(MoaiPrefeb);
                    EMoai[index].SetActive(false);
                }
        */

        //#3. Bullet
        for (int index = 0; index < PantarouBulletNormal.Length; index++)
        {
            PantarouBulletNormal[index] = Instantiate(PantarouBulletNormalPrefab);
            PantarouBulletNormal[index].SetActive(false);
        }
        for (int index = 0; index < PantarouBulletDouble.Length; index++)
        {
            PantarouBulletDouble[index] = Instantiate(PantarouBulletDoublePrefab);
            PantarouBulletDouble[index].SetActive(false);
        }
        for (int index = 0; index < PantarouBulletSpread.Length; index++)
        {
            PantarouBulletSpread[index] = Instantiate(PantarouBulletSpreadPrefab);
            PantarouBulletSpread[index].SetActive(false);
        }
        for (int index = 0; index < PantarouBulletPoton.Length; index++)
        {
            PantarouBulletPoton[index] = Instantiate(PantarouBulletPotonPrefab);
            PantarouBulletPoton[index].SetActive(false);
        }
        for (int index = 0; index < PantarouBulletSpreadBomb.Length; index++)
        {
            PantarouBulletSpreadBomb[index] = Instantiate(PantarouBulletSpreadBombPrefab);
            PantarouBulletSpreadBomb[index].SetActive(false);
        }
        for (int index = 0; index < TacoBulletNormal.Length; index++)
        {
            TacoBulletNormal[index] = Instantiate(TacoBulletNormalPrefab);
            TacoBulletNormal[index].SetActive(false);
        }
        for (int index = 0; index < TacoBulletDouble.Length; index++)
        {
            TacoBulletDouble[index] = Instantiate(TacoBulletDoublePrefab);
            TacoBulletDouble[index].SetActive(false);
        }
        for (int index = 0; index < TacoBulletRipple.Length; index++)
        {
            TacoBulletRipple[index] = Instantiate(TacoBulletRipplePrefab);
            TacoBulletRipple[index].SetActive(false);
        }
        for (int index = 0; index < TacoBulletUpTwoWay.Length; index++)
        {
            TacoBulletUpTwoWay[index] = Instantiate(TacoBulletUpTwoWayPrefab);
            TacoBulletUpTwoWay[index].SetActive(false);
        }
        for (int index = 0; index < TacoBulletDownTwoWay.Length; index++)
        {
            TacoBulletDownTwoWay[index] = Instantiate(TacoBulletDownTwoWayPrefab);
            TacoBulletDownTwoWay[index].SetActive(false);
        }
        for (int index = 0; index < bulletHitEffect.Length; index++)
        {
            bulletHitEffect[index] = Instantiate(bulletHitEffectPrefab);
            bulletHitEffect[index].SetActive(false);
        }


        for (int index = 0; index < itemWeapon.Length; index++)
        {
            itemWeapon[index] = Instantiate(itemWeaponPrefab);
            itemWeapon[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "BossMinime":
                targetPool = minime;
                break;
            /*
                        case "EBabyChick":
                            targetPool = EBabyChick;
                            break;
                        case "EAdultChick":
                            targetPool = EAdultChick;
                            break;
                        case "EMoai":
                            targetPool = EMoai;
                            break;
            */
            case "BulletNormalPantarou":
                targetPool = PantarouBulletNormal;
                break;
            case "BulletDoublePantarou":
                targetPool = PantarouBulletDouble;
                break;
            case "BulletSpreadPantarou":
                targetPool = PantarouBulletSpread;
                break;
            case "BulletPotonPantarou":
                targetPool = PantarouBulletPoton;
                break;
            case "PantarouBulletSpreadBomb":
                targetPool = PantarouBulletSpreadBomb;
                break;
            case "BulletNormalTaco":
                targetPool = TacoBulletNormal;
                break;
            case "BulletDoubleTaco":
                targetPool = TacoBulletDouble;
                break;
            case "BulletRippleTaco":
                targetPool = TacoBulletRipple;
                break;
            case "TacoBulletUpTwoWay":
                targetPool = TacoBulletUpTwoWay;
                break;
            case "TacoBulletDownTwoWay":
                targetPool = TacoBulletDownTwoWay;
                break;
            case "BulletHitEffect":
                targetPool = bulletHitEffect;
                break;
            case "BossMinimeBullet":
                targetPool = bossMinimeBullet;
                break;
            case "PirateBullet":
                targetPool = pirateBullet;
                break;
            case "PiratePenguin":
                targetPool = piratePenguin;
                break;
            case "PiratePenguinBullet":
                targetPool = PiratePenguinBullet;
                break;
            case "ItemWeapon":
                targetPool = itemWeapon;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }

}
