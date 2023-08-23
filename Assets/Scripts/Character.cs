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

    // Start is called before the first frame update
    public void Start()
    {
        angle = Mathf.PI * 3 / 2; // 시작 위치 초기화
        currentHp = stat.Hp(); // 체력 초기화
    }


    // Update is called once per frame
    void Update()
    {
        angle += stat.SpinSpeed() * Time.deltaTime;
        transform.position = center.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

        velocity = new Vector2(-radius * stat.SpinSpeed() * Mathf.Sin(angle), radius * stat.SpinSpeed() * Mathf.Cos(angle));
        angularSpeed = stat.SpinSpeed();
        
        Shoot();
    }

    public void Shoot()
    {
        if (Time.time - lastShotTime > stat.FireRate())
        {
            Instantiate(weapon, shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }
    public void Shoot1()
    {
            Instantiate(weapon, shootTransform.position, Quaternion.identity);
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
