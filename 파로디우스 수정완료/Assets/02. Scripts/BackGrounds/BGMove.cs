using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    public float moveSpeed;  //스테이지배경 이동 속도
    public float bgMovePosition;

    private void OnEnable()
    {
        moveSpeed = 2f * 0.8f;  //배경 이동 속도 초기화

    }
    private void Update()
    {
        BGMoving();  //배경 이동 함수
        PassMovePosition();  //게임매니저 스크립트에 배경 오브젝트의 X좌표 위치값 전달
    }

    void BGMoving()  //배경 이동
    {
        if (this.transform.position.x > -207)
        {
            this.transform.position += new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
        }
    }

    void PassMovePosition()
    {
        bgMovePosition = this.transform.position.x;
        GameManager.instance.bgMovePosition = bgMovePosition;
    }
}
