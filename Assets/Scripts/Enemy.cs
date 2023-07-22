using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public float maxhp = 100f;
    private float currentHp;
    [SerializeField]
    public int moneyReward = 10;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxhp;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Weapon") {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            currentHp -= weapon.damage;
            if (currentHp <= 0) {
                Destroy(gameObject);
                EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
                if (enemySpawner != null) {
                    enemySpawner.RespawnEnemy();
                    GameManager.instance.IncreaseMoney(moneyReward);
                }
            }
            Destroy(other.gameObject);
        }
    }
}
