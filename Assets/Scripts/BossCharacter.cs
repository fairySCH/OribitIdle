using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCharacter : MonoBehaviour
{
    [SerializeField]
    public GameObject bulletPrefab;
    public Transform shootTransform;
    public float bulletSpeed = 10f;
    public float attackInterval = 3f; // 총알 발사 주기
    private float attackTimer = 0f;

    private Transform playerTransform; // 플레이어(캐릭터)의 위치를 추적하기 위한 변수
    private Vector3 lastKnownPlayerPosition; // 최근에 확인한 플레이어(캐릭터)의 위치

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // 태그 "Player"를 가진 오브젝트의 Transform을 가져옴
        lastKnownPlayerPosition = playerTransform.position;
    }

    void Update()
    {
        // 캐릭터를 향해 총알을 발사하는 로직
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackInterval)
        {
            AttackPlayer();
            attackTimer = 0f;
        }
    }

    // 캐릭터를 향해 총알을 발사하는 함수
    void AttackPlayer()
    {
        Vector3 playerPosition = playerTransform.position; // 플레이어(캐릭터)의 위치를 가져옴

        // 캐릭터의 현재 위치와 플레이어의 위치를 기준으로 방향을 계산
        Vector3 direction = (playerPosition - transform.position).normalized;

        // 총알 오브젝트를 생성하고 발사
        GameObject bullet = Instantiate(bulletPrefab, shootTransform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
