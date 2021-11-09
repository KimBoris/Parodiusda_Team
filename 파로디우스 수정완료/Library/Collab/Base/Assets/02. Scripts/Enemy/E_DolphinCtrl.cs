using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_DolphinCtrl : MonoBehaviour, IDamage
{
    /*
        public Rigidbody2D _rigidbody;
        public float dolphinSpeed;
        public float dolphinDegree;

        Vector3 curPos;
        Vector3 targetPos;
        Vector3 beforePos;

        void Start()
        {
            enemyHp = 1;
            dolphinSpeed = 10.0f;
            targetPos = new Vector3(3.9f, -3.6f, 0);

            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.velocity = targetPos * dolphinSpeed * Time.deltaTime;

            float radian= dolphinDegree * Mathf.PI / 180;   //각도를 라디안으로 변환
            Vector2 radianToVector = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

            _rigidbody.AddForce(radianToVector * dolphinSpeed * 100);
            beforePos = transform.position;
        }

        private void FixedUpdate()
        {

            float angle = Mathf.Atan2(_rigidbody.position.y, _rigidbody.position.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
            _rigidbody.AddForce(transform.forward);

        }

        void Update()
        {
            if (beforePos != curPos) // 전프레임과 현재프레임 좌표가 다른지 확인합니다.
            {
                Vector3 directionVec = curPos - beforePos;  //현재 프레임과 전프레임의 차이로 방향벡터를 구합니다.

                float radian = Mathf.Atan2(directionVec.y, directionVec.x);     // 방향벡터 -> 라디안 -> 각도(degree) 순으로 변환합니다.
                float degree = radian * 180 / Mathf.PI;

                transform.eulerAngles = new Vector3(0, 0, degree);  // 각도를 변경합니다.

                beforePos = curPos;     //전프레임좌표를 갱신합니다.
            }
        }

        void dolphinMove()
        { }


    Rigidbody2D rb2d;
    public float launchPower = 5f;
    public float launchDegree = 30f;
    Vector3 beforePos;//전 프레임을 저장할 변수
    Vector3 pos;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        //각도(degree)를 라디안(radian)으로 변환합니다.
        float radian = launchDegree * Mathf.PI / 180;

        //라디안을 단위벡터로 변환합니다.
        Vector2 radianToVector2 = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

        //AddForce
        rb2d.AddForce(radianToVector2 * launchPower * 100);

        //전 프레임 좌표 초기화
        beforePos = transform.position;
    }

    void Update()
    {
        pos = gameObject.transform.position;

        if (beforePos != pos) // 전프레임과 현재프레임 좌표가 다른지 확인합니다.
        {
            //현재 프레임과 전프레임의 차이로 방향벡터를 구합니다.
            Vector3 directionVec = pos - beforePos;

            // 방향벡터 -> 라디안 -> 각도(degree) 순으로 변환합니다.
            float radian = Mathf.Atan2(directionVec.y, directionVec.x);
            float degree = radian * 180 / Mathf.PI;

            // 각도를 변경합니다.
            transform.eulerAngles = new Vector3(0, 0, degree);

            //전프레임좌표를 갱신합니다.
            beforePos = pos;
        }
    }
    */

    public int enemyHp;
    public float dolphinSpeed;

    public Vector3 targetPos;

    void Start()
    {
        targetPos = new Vector3(6.5f, -1f, 0);
        dolphinSpeed = 3f;
    }

    void Update()
    {
        DolphinMove();

        if (gameObject.transform.position.x < -9f)
            Destroy(gameObject);
    }

    void DolphinMove()
    { gameObject.transform.position = Vector3.Slerp(gameObject.transform.position, targetPos, Time.deltaTime * dolphinSpeed); }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Point100")
        { targetPos = new Vector3(3.9f, -6f, 0); }
        if (collision.name == "Point80")
        { targetPos = new Vector3(1.3f, -1f, 0); }
        if (collision.name == "Point60")
        { targetPos = new Vector3(-1.3f, -6f, 0); }
        if (collision.name == "Point40")
        { targetPos = new Vector3(-3.9f, -1f, 0); }
        if (collision.name == "Point20")
        { targetPos = new Vector3(-6.5f, -6f, 0); }
        if (collision.name == "Point0")
        { targetPos = new Vector3(-9.1f, -1f, 0); }
    }

    public void Damage(int playerAtkDamage)
    {
        enemyHp -= playerAtkDamage;

        if (enemyHp <= 0)
        {
            GameManager.instance.ScoreAdd(100);
            this.gameObject.SetActive(false);
        }
    }
}
