using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;     //플레이어 이동 속도
    public float h, v;          //플레이어 상하좌우 이동
    bool isHitPlayer;    //플레이어 피격 가능 여부

    public GameObject speedUpEffect;

    
    Animator anim;

    // StageManager stageMgr;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        speedUpEffect = this.gameObject.GetComponent<PlayerMove>().speedUpEffect;

    }

    private void OnEnable()
    {
        this.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        moveSpeed = 5f;     //플레이어 이동 속도 5f로 초기화
        isHitPlayer = true; //게임 시작 시 플레이어 피격 가능하도록 isHitPlayer 활성화
    }

    private void Update()
    {
        if (this.gameObject != null) //플레이어 오브젝트가 화면에 존재할 경우에만 조작 가능
        {
            MovingPlayer(); //플레이어 조작 함수
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            MoveSpeedUp();       //스피드업 적용 함수 (실행 = 영문자 'H');
        }
    }

    void MovingPlayer()  //플레이어 조작
    {
        h = Input.GetAxisRaw("Horizontal");   //좌우 조작 (A, D, <-, ->)
        v = Input.GetAxisRaw("Vertical");     //상하 조작 (W, S, ↑, ↓)

        Vector3 movement = new Vector3(h, v, 0);
        this.transform.position += movement * moveSpeed * Time.deltaTime;

        if(Input.GetButtonDown("Vertical") || Input.GetButtonUp("Vertical"))
        {
            anim.SetInteger("Input0", (int)v);
        }

    }

    public void MoveSpeedUp()     //스피드업 적용 함수 (실행 = 영문자 'H' / F 키 입력 시 속도 초기화)
    {

        if (moveSpeed < 7.5f)
        {
            Instantiate(speedUpEffect, this.transform.position, Quaternion.identity);
            moveSpeed += 0.5f;
        }


        if (Input.GetKeyDown(KeyCode.F))  //플레이어 이동 속도 초기화
        {
            moveSpeed = 5f;     //플레이어 이동 속도 5f로 초기화
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //IDamage damage = collision.GetComponent<IDamage>();

        if (collision.tag == "ENEMY" || collision.tag == "ENEMYBULLET" || collision.tag == "BGGuard")
        {
            moveSpeed = 0;
            isHitPlayer = false;
        }
    }


}
