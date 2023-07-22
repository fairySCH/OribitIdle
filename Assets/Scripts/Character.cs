using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform center;    // 원운동 중심점
    public float radius = 2.0f; // 반지름
    public float speed = 2.0f;  // 속도
    private float angle = Mathf.PI*3/2;
    [SerializeField]
    public GameObject weapon;
    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.05f;
    private float lastShotTime = 0f;

    // Update is called once per frame
    void Update()
    {
        angle += speed * Time.deltaTime;
        transform.position = center.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
        Shoot();
    }

    void Shoot() {
        if (Time.time - lastShotTime > shootInterval) {
            Instantiate(weapon, shootTransform.position, Quaternion.identity);
            lastShotTime= Time.time;
        }
    }
    public void Upgrade() {

    }
}
