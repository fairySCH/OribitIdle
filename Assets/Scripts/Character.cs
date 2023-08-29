using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
    Stats stat = new Stats();
    public Transform center;
    public float radius = 2.0f;
    private float angle; // Removed the property for starting angle

    [SerializeField]
    public GameObject weapon;
    [SerializeField]
    private Transform shootTransform;
    
    private float lastShotTime = 0f;

    private Vector2 velocity;
    private float angularSpeed;
    
    public Vector2 Velocity
    {
        get { return velocity; }
    }

    public float AngularSpeed
    {
        get { return angularSpeed; }
    }

    private float currentHp; // 현재 체력

    //==================================================================

    [SerializeField]
    public int skillCount1 = 0;
    public int skillCount2 = 0;
    public int skillCount3 = 0;
    public int skillCount4 = 0;
    public int skillCount5 = 0;
    public int skillCount6 = 0;

    //==================================================================

    // Start is called before the first frame update
    public void Start()
    {
        angle = Mathf.PI * 3 / 2; // 시작 위치 초기화
        currentHp = Stats.hp; // 체력 초기화
    }


    // Update is called once per frame
    void Update()
    {
        angle += Stats.spinSpeed * Time.deltaTime;
        transform.position = center.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

        velocity = new Vector2(-radius * Stats.spinSpeed * Mathf.Sin(angle), radius * Stats.spinSpeed * Mathf.Cos(angle));
        angularSpeed = Stats.spinSpeed;
        Shoot(Stats.fireRate);
    }

    public void Shoot(float shootTime)
    {
        float firerate = shootTime;
        if (Time.time - lastShotTime > firerate)
        {
            Instantiate(weapon, shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        // 스킬 1 checkpoint 지날 때 skillCount1 1씩 증가
        if (other.gameObject.tag == "Checkpoint1") {
            skillCount1++;
            // skillCount1이 5가 되면 스킬1 발동
            if (skillCount1 >= 5) {
                StartCoroutine(Skill1());
                skillCount1 = 0;
            }
        }
        // 스킬 2 checkpoint 지날 때 skillCount2 1씩 증가
        if (other.gameObject.tag == "Checkpoint2") {
            skillCount2++;
            // skillCount2가 10가 되면 스킬2 발동
            if (skillCount2 >= 10) {
                StartCoroutine(Skill2());
                skillCount2 = 0;
            }
        }
        // 스킬 3 checkpoint 지날 때 skillCount3 1씩 증가
        if (other.gameObject.tag == "Checkpoint3") {
            skillCount3++;
            // skillCount3가 n이(가) 되면 스킬3 발동
            if (skillCount3 >= 10) {
                StartCoroutine(Skill3());
                skillCount3 = 0;
            }
        }
        // 스킬 4 checkpoint 지날 때 skillCount4 1씩 증가
        if (other.gameObject.tag == "Checkpoint4") {
            skillCount4++;
            // skillCount2가 n가 되면 스킬2 발동
            if (skillCount4 >= 10) {
                StartCoroutine(Skill4());
                skillCount4 = 0;
            }
        }
    }
    // 스킬1 : 5초간 공격속도 2배 증가
    public IEnumerator Skill1() {
        Stats.fireRate = Stats.fireRate/2;
        yield return new WaitForSeconds(5);
        Stats.fireRate = Stats.fireRate*2;
    }
    // 스킬2 : 5초간 공격력 2배 증가
    public IEnumerator Skill2() {
        Stats.damage = Stats.damage*2;
        yield return new WaitForSeconds(5);
        Stats.damage = Stats.damage/2;
    }
    // 스킬3 : 미정
    public IEnumerator Skill3() {
        Stats.damage = Stats.damage*2;
        yield return new WaitForSeconds(5);
        Stats.damage = Stats.damage/2;
    }
    // 스킬4 : 미정
    public IEnumerator Skill4() {
        Stats.damage = Stats.damage*2;
        yield return new WaitForSeconds(5);
        Stats.damage = Stats.damage/2;
    }
    public void TakeDamage(float damage) // 캐릭터 데미지 받는 함수.
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            GameManager.instance.RestartStage(); // GameManager의 RestartStage
            // "Weapon" 태그를 가진 모든 오브젝트를 찾습니다.
            GameObject[] leftoverWeapons = GameObject.FindGameObjectsWithTag("Weapon");

            // 모든 "Weapon" 오브젝트를 파괴합니다.
            foreach (GameObject bullets in leftoverWeapons)
            {
                Destroy(bullets);
            }
            Start();
        }
    }
}
