using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Character character;
    public static GameManager instance;
    // public Text로 모두 바꿀 예정
    public TextMeshProUGUI moneyText; // 돈 보여주기?

    //=============================================================
    public TextMeshProUGUI DamageLevelText;
    public TextMeshProUGUI HpLevelText;
    public TextMeshProUGUI FireRateLevelText;
    public TextMeshProUGUI CriticalRateLevelText;
    public TextMeshProUGUI CriticalDamageLevelText;
    public TextMeshProUGUI DefenceLevelText;
    public TextMeshProUGUI SpinSpeedLevelText;

    //============================================================
    public TextMeshProUGUI DamageUpgradeCostText;
    public TextMeshProUGUI HpUpgradeCostText;
    public TextMeshProUGUI FireRateUpgradeCostText;
    public TextMeshProUGUI CriticalRateUpgradeCostText;
    public TextMeshProUGUI DefenceUpgradeCostText;
    public TextMeshProUGUI SpinSpeedUpgradeCostText;
    public TextMeshProUGUI CriticalDamageUpgradeCostText;

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
    // public Character character; // 캐릭터 객체
    
    private int enemiesDefeated = 0; // 
    private int enemiesToRespawn = 1; // 한 번에 하나의 적만 소환하도록 변경
    public static int enemyCounter = 0; // 이번 스테이지에서 처치한 적의 수
    
    private void Awake()
    {
        if (character == null) {
            character = gameObject.AddComponent<Character>();
            Debug.Log("Character을 불러왔습니다.");
        }
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // This ensures the object persists across scenes
            Debug.Log("instance == null");
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Ensures that any additional instances are destroyed
            Debug.Log("instance != this");
        }
    }
    void Start()
    {
        SpawnEnemies(); // 게임 시작 시 적 소환으로 게임을 시작한다.
        Debug.Log("일반 몹을 소환합니다.");
    }
    void Update()
    {
        if (isBossAlive) {
            currentBossTime += Time.deltaTime;
            if (currentBossTime >= bossTimeLimit) {
                // 보스를 제한 시간 내에 처치하지 못했을 때 무한 스테이지로 변경
                RestartStage();
                Debug.Log("주어진 시간은" + bossTimeLimit + "초였습니다.");
            }
        }
        //#. UI moeny update
        moneyText.text = string.Format("{0:n0}", money);
    }
    void SpawnEnemies() {
        if (bossSpawned) // 보스가 소환되어있으면 보스를 소환 / 아니면 넘어감.
        {
            return;
        }

        if (!isBossAlive) {
            for (int i = 0; i < enemiesToRespawn; i++)
            {
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }
        }
    }
    void SpawnBoss() 
    {
        Debug.Log("보스가 소환됩니다.");
        isBossAlive = true;
        bossSpawned = true;
        Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
    }
    public void EnemyDefeated() {
        if (!isBossAlive) {
            enemiesDefeated++;
            enemyCounter++;
            // 무한 모드가 아닐 때
            if (enemyCounter == 25) {
                Debug.Log("몹을 " + enemyCounter + "마리 처치하여 보스몹이 소환됩니다.");
                SpawnBoss();
                enemyCounter = 0; // 보스가 소환되면 적 처치 수를 초기화
            } else if (enemiesDefeated >= enemiesToRespawn) {
                Debug.Log("처치한 적 수 : " + enemyCounter);
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
        Debug.Log("제한시간 내에 보스를 처치하지 못하여 무한모드로 전환하였습니다.");
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
        Debug.Log("돈을 " + reward + "만큼 얻었습니다.");
        // text.SetText(money.ToString());
    }
    // damage upgrade
    public void UpgradeCharacterDamage() {
        if (CanAfford(Character.damageUpgradeCost)) {
            DeductMoney(Character.damageUpgradeCost);
            Character.characterDamageLevel += 1;
            character.UpdateDamageUpgradeCost();
            character.UpdateDamage();
            UpdateDamageUI();
            Debug.Log("업그레이드 성공, 현재 업글비용: " + Character.damageUpgradeCost);
        }
    }
    // hp upgrade
    public void UpgradeCharacterHp() { // 캐릭터 최대 체력 업그레이드
        if (CanAfford(Character.hpUpgradeCost)) {
            DeductMoney(Character.hpUpgradeCost);
            Character.characterHpLevel += 1;
            character.UpdateHpUpgradeCost();
            character.UpdateHp();
            UpdateHpUI();
            Debug.Log("업그레이드 성공, 현재 업글비용: " + Character.hpUpgradeCost);
        }
    }
    // fire rate upgrade
    public void UpgradeCharacterFireRate() { // 총알 발사속도 업그레이드
        if (CanAfford(Character.fireRateUpgradeCost)) {
            DeductMoney(Character.fireRateUpgradeCost);
            Character.characterFireRateLevel += 1;
            character.UpdateFireRateUpgradeCost();
            character.UpdateFireRate();
            UpdateFireRateUI();
        }
    }    
    // critical rate upgrade
    public void UpgradeCharacterCriticalRate() { // 캐릭터 치명타율 업그레이드
        if (CanAfford(Character.criticalRateUpgradeCost)) {
            DeductMoney(Character.criticalRateUpgradeCost);
            Character.characterCriticalRateLevel += 1;
            character.UpdateCriticalRateUpgradeCost();
            character.UpdateCriticalRate();
            UpdateCriticalRateUI();
        }
    }
    // critical damage upgrade
    public void UpgradeCharacterCriticalDamage() { // 캐릭터 치명타 데미지% 업그레이드
        if (CanAfford(Character.criticalDamageUpgradeCost)) {
            DeductMoney(Character.criticalDamageUpgradeCost);
            Character.characterCriticalDamageLevel += 1;
            character.UpdateCriticalDamageUpgradeCost();
            character.UpdateCriticalDamage();
            UpdateCriticalDamageUI();
        }
    }        
    // defence upgrade
    public void UpgradeCharacterDefence() { // 캐릭터 방어력 업그레이드
        if (CanAfford(Character.defenceUpgradeCost)) {
            DeductMoney(Character.defenceUpgradeCost);
            Character.characterDefenceLevel += 1;
            character.UpdateDefenceUpgradeCost();
            character.UpdateDefence();
            UpdateDefenceUI();
        }
    }    
    // spinspeed upgrade
    public void UpgradeSpinSpeed() { // 캐릭터 방어력 업그레이드
        if (CanAfford(Character.spinSpeedUpgradeCost)) {
            DeductMoney(Character.spinSpeedUpgradeCost);
            Character.characterSpinSpeedLevel += 1;
            character.UpdateSpinSpeedUpgradeCost();
            character.UpdateSpinSpeed();
            UpdateSpinSpeedUI();
        }
    }    
    private bool CanAfford(int cost) {
        return money >= cost;
    }
    private void DeductMoney(int amount) {
        money -= amount;
        // text.SetText(money.ToString());
    }

        // UI 업데이트 메서드
    private void UpdateDamageUI() {
        DamageUpgradeCostText.SetText(Character.damageUpgradeCost.ToString());
        DamageLevelText.SetText("Lv." + Character.characterDamageLevel.ToString());
    }
    private void UpdateHpUI() {
        HpUpgradeCostText.SetText(Character.damageUpgradeCost.ToString());
        HpLevelText.SetText("Lv." + Character.characterHpLevel.ToString());
    }
    private void UpdateFireRateUI() {
        FireRateUpgradeCostText.SetText(Character.damageUpgradeCost.ToString());
        FireRateLevelText.SetText("Lv." + Character.characterFireRateLevel.ToString());
    }
    private void UpdateCriticalRateUI() {
        CriticalRateUpgradeCostText.SetText(Character.damageUpgradeCost.ToString());
        CriticalRateLevelText.SetText("Lv." + Character.characterCriticalRateLevel.ToString());
    }
    private void UpdateCriticalDamageUI() {
        CriticalDamageUpgradeCostText.SetText(Character.damageUpgradeCost.ToString());
        CriticalDamageLevelText.SetText("Lv." + Character.characterCriticalDamageLevel.ToString());
    }
    private void UpdateDefenceUI() {
        DefenceUpgradeCostText.SetText(Character.damageUpgradeCost.ToString());
        DefenceLevelText.SetText("Lv." + Character.characterDefenceLevel.ToString());
    }
    private void UpdateSpinSpeedUI() {
        SpinSpeedUpgradeCostText.SetText(Character.damageUpgradeCost.ToString());
        SpinSpeedLevelText.SetText("Lv." + Character.characterSpinSpeedLevel.ToString());
    }

    public static int GetEnemyCounter() {
        return enemyCounter;
    }
    //무한 스테이지 모드 들어가는 함수() {
        //
    //}
    //보스 스테이지 엔터 함수() {
        // 모든 적 없애기 (이때 카운터는 안올라감)
        // 
    //}
}
