using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMove : MonoBehaviour
{
    float yUp;      //위로 움직이는 끝부분
    float yDown;    //아래로 움직이는 끝부분
    float bossSpeed;//보스 이동속도
    bool isHitBoss; //보스 피격 가능 여부

    Vector3 bossDir = Vector3.up;

    Animator bossAnim;

    private void OnEnable()
    {
        bossSpeed = 5f;
        isHitBoss = true;
    }

    void Start()
    {

    }

    void Update()
    {
        MovingBoss();
        
    }

    void MovingBoss()
    {
        this.transform.position += bossDir * bossSpeed * Time.deltaTime;
             //보스 입장
        if(this.transform.position.y == moveY)
        {
            bossDir = Vector3.up;
            moveY = Random.Range(-2.5f, 2.5f);
        }
    }
}
