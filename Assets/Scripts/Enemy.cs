using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float maxhp = 100f; // Monster's default health = 100
    private float currentHp;
    [SerializeField]
    public int moneyReward = 10; // 적 처치 보상금

    [SerializeField]
    private int bossHpMultiplier = 25; // 보스 체력 배수

    // Start is called before the first frame update
    void Start()
    {
        // Initialize current HP and health bar
        currentHp = maxhp * (GameManager.stageCounter + 1);

        // Adjust current HP for Boss enemies
        if (gameObject.tag == "Boss")
        {
            currentHp = maxhp * (GameManager.stageCounter + 1) * bossHpMultiplier;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the enemy's current HP has dropped to or below 0
        if (currentHp <= 0)
        {
            // Destroy the enemy gameObject
            Destroy(gameObject);

            // Increase player's money and handle defeated enemy
            GameManager.instance.IncreaseMoney(moneyReward);

            // Check if the defeated enemy is a Boss or regular enemy
            if (gameObject.tag == "Boss")
            {
                GameManager.instance.BossDefeated();
            }
            else
            {
                GameManager.instance.EnemyDefeated();
            }
        }
    }

    // Called when the enemy collides with a trigger collider (e.g., a weapon)
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            currentHp -= Stats.damage;

            // 적 사망처리
            if (currentHp <= 0)
            {
                // Destroy the enemy gameObject
                Destroy(gameObject);

                // Increase player's money and handle defeated enemy
                GameManager.instance.IncreaseMoney(moneyReward);

                // Check if the defeated enemy is a Boss or regular enemy
                if (gameObject.tag == "Boss")
                {
                    GameManager.instance.BossDefeated();
                }
                else
                {
                    GameManager.instance.EnemyDefeated(); // Call GameManager's function upon enemy defeat
                }
            }

            // Destroy the weapon gameObject after the attack
            Destroy(other.gameObject);
        }
    }
}
