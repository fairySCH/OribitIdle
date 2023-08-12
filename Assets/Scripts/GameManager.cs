using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    Stats stat = new Stats();
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI text; // 돈 보여주기?
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
    
    void Awake() {
        if (instance == null) 
        {
            instance = this;
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

    public void UpgradeBulletDamage() { // 캐릭터 총알 데미지 업그레이드
        int upgradecost = 50;

        if (money >= upgradecost) {
            Stats.characterDamageLevel += 1;
            money -= upgradecost;
            UpdateDamageLevelUI(); // UI 업데이트 메서드 호출
            text.SetText(money.ToString());
        }
    }
    public void UpgradeCharacterHp() { // 캐릭터 최대 체력 업그레이드
        int hpUpgradeCost = 100;

        if (money >= hpUpgradeCost) {
            Stats.characterHpLevel += 1;
            money -= hpUpgradeCost;
            UpdateHpLevelUI(); // UI 업데이트 메서드 호출
            text.SetText(money.ToString());
        }
    }
    public void UpgradeCharacterFireRate() { // 총알 발사속도 업그레이드
        int fireRateUpgradeCost = 100;

        if (money >= fireRateUpgradeCost) {
            Stats.characterFireRateLevel += 1;
            money -= fireRateUpgradeCost;
            UpdateFireRateLevelUI(); // UI 업데이트 메서드 호출
            text.SetText(money.ToString());
        }
    }    
    public void UpgradeCharacterCriticalRate() { // 캐릭터 치명타율 업그레이드
        int criticalRateUpgradeCost = 100;

        if (money >= criticalRateUpgradeCost) {
            Stats.characterCriticalRateLevel += 1;
            money -= criticalRateUpgradeCost;
            UpdateCriticalRateLevelUI(); // UI 업데이트 메서드 호출
            text.SetText(money.ToString());
        }
    }
    public void UpgradeCharacterCriticalDamage() { // 캐릭터 치명타 데미지% 업그레이드
        int criticalDamageUpgradeCost = 100;

        if (money >= criticalDamageUpgradeCost) {
            Stats.characterCriticalDamageLevel += 1;
            money -= criticalDamageUpgradeCost;
            UpdateCriticalDamageLevelUI(); // UI 업데이트 메서드 호출
            text.SetText(money.ToString());
        }
    }        
    public void UpgradeCharacterDefence() { // 캐릭터 방어력 업그레이드
        int defenceUpgradeCost = 100;

        if (money >= defenceUpgradeCost) {
            Stats.characterDefenceLevel += 1;
            money -= defenceUpgradeCost;
            UpdateDefenceLevelUI(); // UI 업데이트 메서드 호출
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

        // UI 업데이트 메서드
    private void UpdateDamageLevelUI(){
        DamageLevelText.SetText("Lv." + Stats.characterDamageLevel.ToString());
    }
    private void UpdateHpLevelUI(){
        HpLevelText.SetText("Lv." + Stats.characterHpLevel.ToString());
    }
    private void UpdateFireRateLevelUI(){
        FireRateLevelText.SetText("Lv." + Stats.characterFireRateLevel.ToString());
    }
    private void UpdateCriticalRateLevelUI(){
        CriticalRateLevelText.SetText("Lv." + Stats.characterCriticalRateLevel.ToString());
    }
    private void UpdateCriticalDamageLevelUI(){
        CriticalDamageLevelText.SetText("Lv." + Stats.characterCriticalDamageLevel.ToString());
    }
    private void UpdateDefenceLevelUI(){
        DefenceLevelText.SetText("Lv." + Stats.characterDefenceLevel.ToString());
    }

}
