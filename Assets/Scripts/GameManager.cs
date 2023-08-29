using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    Stats stat = new Stats();
    public static GameManager instance;
    [SerializeField]
    private TextMeshProUGUI text; // 돈 보여주기?

    //=============================================================
    [SerializeField]
    private TextMeshProUGUI DamageLevelText;
    [SerializeField]
    private TextMeshProUGUI HpLevelText;
    [SerializeField]
    private TextMeshProUGUI FireRateLevelText;
    [SerializeField]
    private TextMeshProUGUI CriticalRateLevelText;
    [SerializeField]
    private TextMeshProUGUI CriticalDamageLevelText;
    [SerializeField]
    private TextMeshProUGUI DefenceLevelText;
    [SerializeField]
    private TextMeshProUGUI SpinSpeedLevelText;

    //============================================================
    [SerializeField]
    private TextMeshProUGUI DamageUpgradeCostText;
    [SerializeField]
    private TextMeshProUGUI HpUpgradeCostText;
    [SerializeField]
    private TextMeshProUGUI FireRateUpgradeCostText;
    [SerializeField]
    private TextMeshProUGUI CriticalRateUpgradeCostText;
    [SerializeField]
    private TextMeshProUGUI DefenceUpgradeCostText;
    [SerializeField]
    private TextMeshProUGUI SpinSpeedUpgradeCostText;
    [SerializeField]
    private TextMeshProUGUI CriticalDamageUpgradeCostText;

    private int money = 99999; // 돈 선언 및 0 초기화
    public static int stageCounter = 0; // 스테이지 수 변수
    private bool bossSpawned = false; // 보스 소환 상태 저장
    private bool isBossAlive = false; // 보스 생존여부 확인
    private float bossTimeLimit = 30f; // 보스전 제한시간
    private float currentBossTime = 0f; // 현재 보스전 진행 시간

    public Enemy enemyPrefab; // 적 prefab
    public Enemy bossPrefab; // 보스 prefab
    public Transform spawnPoint; // 적 소환위치 transform
    public Weapon weapon; // 무기 weapon 선언
    public Character character; // 캐릭터 객체
    
    private int enemiesDefeated = 0; // 
    private int enemiesToRespawn = 1; // 한 번에 하나의 적만 소환하도록 변경
    private int enemyCounter = 0; // 이번 스테이지에서 처치한 적의 수
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // This ensures the object persists across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Ensures that any additional instances are destroyed
        }
    }
    void Start()
    {
        SpawnEnemies(); // 게임 시작 시 적 소환으로 게임을 시작한다.
    }
    void Update()
    {
        if (isBossAlive) {
            currentBossTime += Time.deltaTime;
            if (currentBossTime >= bossTimeLimit) {
                // 보스를 제한 시간 내에 처치하지 못했을 때
                RestartStage();
            }
        }
    }
    void SpawnEnemies() {
        if (bossSpawned) // 보스가 소환되어있으면 보스를 소환 / 아니면 넘어감.
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
    public void EnemyDefeated() {
        if (!isBossAlive) {
            enemiesDefeated++;
            enemyCounter++;

            if (enemyCounter == 25) {
                SpawnBoss();
                enemyCounter = 0; // 보스가 소환되면 적 처치 수를 초기화
            } else if (enemiesDefeated >= enemiesToRespawn) {
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
    // damage upgrade
    public void UpgradeCharacterDamage() {
        if (CanAfford(Stats.damageUpgradeCost)) {
            DeductMoney(Stats.damageUpgradeCost);
            Stats.characterDamageLevel += 1;
            stat.UpdateDamageUpgradeCost();
            stat.UpdateDamage();
            UpdateDamageUI();
        }
    }
    // hp upgrade
    public void UpgradeCharacterHp() { // 캐릭터 최대 체력 업그레이드
        if (CanAfford(Stats.hpUpgradeCost)) {
            DeductMoney(Stats.hpUpgradeCost);
            Stats.characterHpLevel += 1;
            stat.UpdateHpUpgradeCost();
            stat.UpdateHp();
            UpdateHpUI();
        }
    }
    // fire rate upgrade
    public void UpgradeCharacterFireRate() { // 총알 발사속도 업그레이드
        if (CanAfford(Stats.fireRateUpgradeCost)) {
            DeductMoney(Stats.fireRateUpgradeCost);
            Stats.characterFireRateLevel += 1;
            stat.UpdateFireRateUpgradeCost();
            stat.UpdateFireRate();
            UpdateFireRateUI();
        }
    }    
    // critical rate upgrade
    public void UpgradeCharacterCriticalRate() { // 캐릭터 치명타율 업그레이드
        if (CanAfford(Stats.criticalRateUpgradeCost)) {
            DeductMoney(Stats.criticalRateUpgradeCost);
            Stats.characterCriticalRateLevel += 1;
            stat.UpdateCriticalRateUpgradeCost();
            stat.UpdateCriticalRate();
            UpdateCriticalRateUI();
        }
    }
    // critical damage upgrade
    public void UpgradeCharacterCriticalDamage() { // 캐릭터 치명타 데미지% 업그레이드
        if (CanAfford(Stats.criticalDamageUpgradeCost)) {
            DeductMoney(Stats.criticalDamageUpgradeCost);
            Stats.characterCriticalDamageLevel += 1;
            stat.UpdateCriticalDamageUpgradeCost();
            stat.UpdateCriticalDamage();
            UpdateCriticalDamageUI();
        }
    }        
    // defence upgrade
    public void UpgradeCharacterDefence() { // 캐릭터 방어력 업그레이드
        if (CanAfford(Stats.defenceUpgradeCost)) {
            DeductMoney(Stats.defenceUpgradeCost);
            Stats.characterDefenceLevel += 1;
            stat.UpdateDefenceUpgradeCost();
            stat.UpdateDefence();
            UpdateDefenceUI();
        }
    }    
    // spinspeed upgrade
    public void UpgradeSpinSpeed() { // 캐릭터 방어력 업그레이드
        if (CanAfford(Stats.spinSpeedUpgradeCost)) {
            DeductMoney(Stats.spinSpeedUpgradeCost);
            Stats.characterSpinSpeedLevel += 1;
            stat.UpdateSpinSpeedUpgradeCost();
            stat.UpdateSpinSpeed();
            UpdateSpinSpeedUI();
        }
    }    
    private bool CanAfford(int cost)
    {
        return money >= cost;
    }
    private void DeductMoney(int amount)
    {
        money -= amount;
        text.SetText(money.ToString());
    }

        // UI 업데이트 메서드
    private void UpdateDamageUI()
    {
        DamageUpgradeCostText.SetText(Stats.damageUpgradeCost.ToString());
        DamageLevelText.SetText("Lv." + Stats.characterDamageLevel.ToString());
    }
    private void UpdateHpUI() {
        HpUpgradeCostText.SetText(Stats.damageUpgradeCost.ToString());
        HpLevelText.SetText("Lv." + Stats.characterHpLevel.ToString());
    }
    private void UpdateFireRateUI() {
        FireRateUpgradeCostText.SetText(Stats.damageUpgradeCost.ToString());
        FireRateLevelText.SetText("Lv." + Stats.characterFireRateLevel.ToString());
    }
    private void UpdateCriticalRateUI() {
        CriticalRateUpgradeCostText.SetText(Stats.damageUpgradeCost.ToString());
        CriticalRateLevelText.SetText("Lv." + Stats.characterCriticalRateLevel.ToString());
    }
    private void UpdateCriticalDamageUI() {
        CriticalDamageUpgradeCostText.SetText(Stats.damageUpgradeCost.ToString());
        CriticalDamageLevelText.SetText("Lv." + Stats.characterCriticalDamageLevel.ToString());
    }
    private void UpdateDefenceUI() {
        DefenceUpgradeCostText.SetText(Stats.damageUpgradeCost.ToString());
        DefenceLevelText.SetText("Lv." + Stats.characterDefenceLevel.ToString());
    }
    private void UpdateSpinSpeedUI() {
        SpinSpeedUpgradeCostText.SetText(Stats.damageUpgradeCost.ToString());
        SpinSpeedLevelText.SetText("Lv." + Stats.characterSpinSpeedLevel.ToString());
    }

}
