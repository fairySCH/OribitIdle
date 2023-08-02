using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI text;
    private int money = 0;
    public static int stageCounter = 0;
    private bool bossSpawned = false;
    private bool isBossAlive = false;
    private float bossTimeLimit = 30f;
    private float currentBossTime = 0f;

    public Enemy enemyPrefab;
    public Enemy bossPrefab;
    public Transform spawnPoint;

    public Weapon weapon;
    private int enemiesDefeated = 0;
    private int enemiesToRespawn = 1; // 한 번에 하나의 적만 소환하도록 변경
    private int enemyCounter = 0; // 이번 스테이지에서 처치한 적의 수

    public Character character; // 캐릭터 객체
    void Awake() {
        if (instance == null) 
        {
            instance = this;
        }
    }
    void Start()
    {
        SpawnEnemies();
    }
    void Update()
    {
        if (isBossAlive)
        {
            currentBossTime += Time.deltaTime;
            if (currentBossTime >= bossTimeLimit)
            {
                // 보스를 제한 시간 내에 처치하지 못했을 때
                RestartStage();
            }
        }
    }
    void SpawnEnemies()
    {
        
        if (bossSpawned)
        {
            return;
        }

        if (!isBossAlive)
        {
            for (int i = 0; i < enemiesToRespawn; i++)
            {
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    }
    void SpawnBoss() 
    {
        isBossAlive = true;
        bossSpawned = true;
        Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
    }
    public void EnemyDefeated()
    {
        if (!isBossAlive)
        {
            enemiesDefeated++;
            enemyCounter++;

            if (enemyCounter == 25)
            {
                SpawnBoss();
                enemyCounter = 0; // 보스가 소환되면 적 처치 수를 초기화
            }
            else if (enemiesDefeated >= enemiesToRespawn)
            {
                enemiesDefeated = 0;
                SpawnEnemies();
            }
        }
    }
    public void BossDefeated()
    {
        isBossAlive = false;
        bossSpawned = false;
        currentBossTime = 0f;
        IncreaseMoney(100); // 보스 처치 시 추가 보상 등을 지급할 수 있습니다.
        stageCounter++;
        SpawnEnemies();
    }

    public void RestartStage()
    {
        // 보스를 처치하지 못했을 때 스테이지 재시작
        isBossAlive = false;
        bossSpawned = false;
        currentBossTime = 0f;
        enemiesDefeated = 0;

        // 보스가 존재하는 경우 보스를 제거
        if (bossPrefab != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Boss"));
        }

        SpawnEnemies();
    }

    public void IncreaseMoney(int reward) {
        money += reward;
        text.SetText(money.ToString());
    }

    public void UpgradeBulletDamage() {
        int upgradecost = 50;

        if (money >= upgradecost) {
            weapon.damage += 10;
            money -= upgradecost;
            text.SetText(money.ToString());
        }
    }
    
    public void CharacterDied()
    {
        // 캐릭터 사망 시 호출되는 함수
        // 여기에 게임 오버 처리 등을 추가할 수 있습니다.
        Debug.Log("게임 오버");
        // 예를 들어 게임 오버 화면을 표시하거나 게임을 재시작하는 로직을 추가할 수 있습니다.
    }
}
