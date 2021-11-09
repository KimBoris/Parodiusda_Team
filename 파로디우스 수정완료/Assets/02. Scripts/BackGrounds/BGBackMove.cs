using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGBackMove : MonoBehaviour
{
    public float moveSpeed;   //배경 이동 속도
    GameObject universeBG2;   //게임오브젝트 우주배경2 (BG_Universe2)
    GameObject universeBG3;   //게임오브젝트 우주배경3 (BG_Universe3)
    float removeDelay = 0.4f;
    float removeTime = 0;

    private void OnEnable()
    {
        moveSpeed = 0.75f;
        universeBG2 = GameObject.Find("BG_Universe2"); //우주배경2 찾기
        universeBG3 = GameObject.Find("BG_Universe3"); //우주배경3 찾기
    }
    private void Update()
    {
        BGBackMoving();   //우주 / 바다 배경 이동 함수
    }

    void BGBackMoving()  //우주/바다배경이동
    {
        if (this.transform.position.x > -98)
        {
            this.transform.position += new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
        }

        if (this.transform.position.x < -15)
        {
            BGBackRemove();  //우주 배경 삭제 함수
        }
    }

    void BGBackRemove()  //우주 배경 삭제 (0.8f 에 0.1f 씩 투명도 올림? 내림?)
    {

        removeTime += Time.deltaTime;
        if (removeTime > removeDelay)
        {
            universeBG2.GetComponent<SpriteRenderer>().color -= new Color(1, 1, 1,0.05f);
            universeBG3.GetComponent<SpriteRenderer>().color -= new Color(1, 1, 1,0.05f);
            removeTime = 0;
        }
    }

}
