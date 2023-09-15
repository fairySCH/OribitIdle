using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
    //===================================================================

        
    public static int characterHpLevel;
    public static int characterDamageLevel;
    public static int characterFireRateLevel;
    public static int characterCriticalRateLevel;
    public static int characterCriticalDamageLevel;
    public static int characterDefenceLevel;
    public static int characterSpinSpeedLevel;

    //===================================================================

    public static float hp;
    public static float damage;
    public static float fireRate;
    public static float criticalRate;
    public static float criticalDamage;
    public static float defence;
    public static float spinSpeed;

    //===================================================================

    public static int hpUpgradeCost;
    public static int damageUpgradeCost;
    public static int fireRateUpgradeCost;
    public static int criticalRateUpgradeCost;
    public static int criticalDamageUpgradeCost;
    public static int defenceUpgradeCost;
    public static int spinSpeedUpgradeCost;


    //==================================================================

    public void UpdateHp() {
        hp = 100f + 10f*characterHpLevel;
    }
    public void UpdateDamage() {
        damage = 10f + 10f*characterDamageLevel;
    }
    public void UpdateFireRate() {
        fireRate = 1f - 0.05f*characterFireRateLevel;
    }
    public void UpdateCriticalRate() {
        criticalRate = 0.01f*characterCriticalRateLevel;
    }
    public void UpdateCriticalDamage() {
        criticalDamage = (2f + 0.5f*characterCriticalDamageLevel)*damage;
    }
    public void UpdateDefence() {
        defence = 10f + 15f*characterDefenceLevel;
    }
    public void UpdateSpinSpeed() {
        spinSpeed = 1f + 0.5f*characterSpinSpeedLevel;
    }
    

    //=================================================================
    public void UpdateHpUpgradeCost() {
        hpUpgradeCost = 50 + 10*characterHpLevel;
    }
    public void UpdateDamageUpgradeCost() {
        damageUpgradeCost = 50 + 10*characterDamageLevel;
    }
    public void UpdateFireRateUpgradeCost() {
        fireRateUpgradeCost = 50 + 10*characterFireRateLevel;
    }
    public void UpdateCriticalRateUpgradeCost() {
        criticalRateUpgradeCost = 50 + 10*characterCriticalRateLevel;
    }
    public void UpdateCriticalDamageUpgradeCost() {
        criticalDamageUpgradeCost = 50 + 10*characterCriticalDamageLevel;
    }
    public void UpdateDefenceUpgradeCost() {
        defenceUpgradeCost = 50 + 10*characterDefenceLevel;
    }
    public void UpdateSpinSpeedUpgradeCost() {
        spinSpeedUpgradeCost = 50 + 10*characterSpinSpeedLevel;
    }
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

        characterHpLevel = 0;
        characterDamageLevel = 0;
        characterFireRateLevel = 0;
        characterCriticalRateLevel = 0;
        characterCriticalDamageLevel = 0;
        characterDefenceLevel = 0;
        characterSpinSpeedLevel = 0;

        hp = 100f;
        damage = 10f;
        fireRate = 1f;
        criticalRate = 0.01f;
        criticalDamage = 2f;
        defence = 10f;
        spinSpeed = 1f;

        hpUpgradeCost = 50;
        damageUpgradeCost = 50;
        fireRateUpgradeCost = 50;
        criticalRateUpgradeCost = 50;
        criticalDamageUpgradeCost = 50;
        defenceUpgradeCost = 50;
        spinSpeedUpgradeCost = 50;

        currentHp = hp; // 체력 초기화
        Debug.Log("현재 체력은" + currentHp + "이고, 레벨은" + characterHpLevel + "이고, 업그래이드 비용은" + hpUpgradeCost + "입니다.");
        Debug.Log("현재 공격력은" + damage + "입니다. ");

    }


    // Update is called once per frame
    void Update()
    {
        angle += spinSpeed * Time.deltaTime;
        transform.position = center.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

        velocity = new Vector2(-radius * spinSpeed * Mathf.Sin(angle), radius * spinSpeed * Mathf.Cos(angle));
        angularSpeed = spinSpeed;
        Shoot(fireRate);
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
        fireRate = fireRate/2;
        yield return new WaitForSeconds(5);
        fireRate = fireRate*2;
    }
    // 스킬2 : 5초간 공격력 2배 증가
    public IEnumerator Skill2() {
        damage = damage*2;
        yield return new WaitForSeconds(5);
        damage = damage/2;
    }
    // 스킬3 : 미정
    public IEnumerator Skill3() {
        damage = damage*2;
        yield return new WaitForSeconds(5);
        damage = damage/2;
    }
    // 스킬4 : 미정
    public IEnumerator Skill4() {
        damage = damage*2;
        yield return new WaitForSeconds(5);
        damage = damage/2;
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
