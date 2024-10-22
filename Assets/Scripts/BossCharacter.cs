using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharacter : MonoBehaviour
{
    [SerializeField]
    public GameObject bulletPrefab;
    public Transform shootTransform;
    public float bulletSpeed = 8f;
    public float attackInterval = 3f;
    private float attackTimer = 0f;
    private float bulletDamage = 10f; // 보스의 공격 데미지

    private Transform playerTransform;
    private Vector3 lastKnownPlayerPosition;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        lastKnownPlayerPosition = playerTransform.position;
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackInterval)
        {
            AttackPlayer();
            attackTimer = 0f;
        }
    }

    void AttackPlayer()
    {
        Vector3 playerPosition = playerTransform.position;
        Character character = playerTransform.GetComponent<Character>();

        Vector3 relativePosition = playerPosition - transform.position;
        float timeToReach = relativePosition.magnitude / bulletSpeed;

        Vector3 predictedPlayerPosition = playerPosition + (Vector3)(character.Velocity * timeToReach);

        Vector3 fireDirection = (predictedPlayerPosition - shootTransform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, shootTransform.position, Quaternion.identity);
        bulletDamage = bulletDamage+10*GameManager.stageCounter; // 스테이지에 비례하여 보스의 데미지 상승
        bullet.GetComponent<Bullet>().SetDamage(bulletDamage); 
        bullet.GetComponent<Rigidbody2D>().velocity = fireDirection * bulletSpeed;
    }
}
