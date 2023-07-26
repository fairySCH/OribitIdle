using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public float maxhp = 100f; // 몬스터 기본 체력 = 100
    private float currentHp;
    [SerializeField]
    public int moneyReward = 10;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxhp * (GameManager.stageCounter + 1);
        if (gameObject.tag == "Boss")
        {
            currentHp = maxhp * (GameManager.stageCounter + 1) * 25;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Weapon") {
           Weapon weapon = other.gameObject.GetComponent<Weapon>();
            currentHp -= weapon.damage;
            if (currentHp <= 0)
            {
                Destroy(gameObject);
                GameManager.instance.IncreaseMoney(moneyReward);
                if (gameObject.tag == "Boss")
                {
                    GameManager.instance.BossDefeated();
                } 
                else 
                {
                    GameManager.instance.EnemyDefeated(); // 적 처치 시 GameManager의 함수 호출
                }

            }
            Destroy(other.gameObject);
        }
    }
}
