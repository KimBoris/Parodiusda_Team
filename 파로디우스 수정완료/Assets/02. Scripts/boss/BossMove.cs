using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossMove : MonoBehaviour, IDamage
{
    public int bossHp;   //보스 체력
    float bossSpeed;     //보스 이동속도
    float hitDelay;      //보스 피격 후 딜레이

    float yUp;           //위로 움직이는 끝부분
    float yDown;         //아래로 움직이는 끝부분

    bool isBossUp;       // 보스의 이동방향 (0 : Up, 1 : Down)
    bool isHitBoss;      //보스 피격 가능 여부(0 : 불가능, 1: 가능)
    public bool isBossLive;       //보스 생사 여부
    Vector3 bossDir = Vector3.up; //보스 기본 방향

    Animator bossAnim; //보스 (Idle, Damaged, Die)애니메이션
    Animator navelAnim;//배꼽 애니메이션
    Rigidbody2D rigid; //보스 사망시 죽는 모션에 필요한 리지드바디
    private void OnEnable()
    {
        bossHp = 15;
        bossSpeed = 3f;
        yUp = 2f;
        yDown = -2f;
        isHitBoss = true;
        isBossUp = true;
        isBossLive = true;
        isHitBoss = false;
        bossAnim = GetComponentInChildren<Animator>();
        navelAnim = GetComponentInChildren<Animator>();
        navelAnim.name = "navel";
    }
    void Update()
    {
        if (isBossLive)
        {
            if (this.transform.position.y > yUp)
            {
                isBossUp = false;
            }
            else if (this.transform.position.y < yDown)
            {
                isBossUp = true;
            }
            switch (isBossUp)
            {
                case false:
                    downMovingBoss();
                    break;
                case true:
                    UpMovingBoss();
                    break;
            }
        }
        else if (isBossLive == false)
        {
            StartCoroutine(BossDie());
            if (this.transform.position.y < -13)
            {   //현재 점수를 레지스트리내에 저장시킨다
                PlayerPrefs.SetInt("HiScore", GameManager.instance.score);
                this.gameObject.SetActive(false);
                GameManager.instance.MoveToGameClearScene();
            }
        }
        //보스 타격
        if (isHitBoss == true) //보스에게 연속적인 데미지를 주지 못하게 제어
        {
            hitDelay += Time.deltaTime;
            if (hitDelay > 1)
            {
                isHitBoss = false;
                hitDelay = 0;
            }
        }
    }

    void UpMovingBoss() //보스 위로 이동
    {
        this.transform.position += bossDir * bossSpeed * Time.deltaTime;
    }
    void downMovingBoss()//보스 아래로 이동
    {
        this.transform.position -= bossDir * bossSpeed * Time.deltaTime;
    }

    IEnumerator BossDie()
    {//보스 죽었을때 애니메이션
        navelAnim.GetBool("BossDie");
        //현재 위치에서 잠시 대기
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        yield return new WaitForSeconds(4f);
        BossDown();

    }
    void BossDown()
    {
        this.transform.position += Vector3.down * 5 * Time.deltaTime;
        //웨이브 이펙트
        //if (this.transform.position.y < -4.44f)
        //{
        //    bossAnim.SetTrigger("WaveEff");
        //}
        //this.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 2, ForceMode2D.Impulse); 
        //보스죽었을때 살짝 올라갔다가 떨어지게 만드는 코드
    }

    void IDamage.Damage(int damage)
    {
        if (isHitBoss == false)
        {
            bossHp -= damage;
            bossAnim.SetInteger("BossHp", bossHp);
            bossAnim.SetTrigger("BossDamaged");
            isHitBoss = true;
            if (bossHp <= 0)//보스 죽었을 시에
            {   //게임매니저에 점수 적용
                GameManager.instance.ScoreAdd(10000);
                isBossLive = false;
            }
        }
    }
}
