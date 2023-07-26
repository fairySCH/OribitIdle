using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform center;    // 원운동 중심점
    public float radius = 2.0f; // 반지름
    public float speed = 2.0f;  // 속도
    private float angle = Mathf.PI*3/2; // 각속도 구현용, 각도를 정의

    [SerializeField]
    public GameObject weapon; // 무기 선언
    [SerializeField]
    private Transform shootTransform; // 총알 발사 위치 == 캐릭터 위치

    [SerializeField]
    private float shootInterval = 0.05f; // 발사하는 간격 조절
    private float lastShotTime = 0f; // 현재 시간을 계산해 일정하게 총알 발사하도록 함

    // Update is called once per frame
    void Update()
    {
        angle += speed * Time.deltaTime; // 원운동 속도에 비례하게 각도가 커짐. (각속도)
        transform.position = center.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius; // 원의 중심으로부터 반지름만큼의 거리를 유지한채 회전.
        Shoot();
    }
    // 총알 발사 구현
    void Shoot() {
        if (Time.time - lastShotTime > shootInterval) {
            Instantiate(weapon, shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }
    
}
