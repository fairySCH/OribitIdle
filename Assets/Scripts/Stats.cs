using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public static int characterHpLevel = 0;
    public static int characterDamageLevel = 0;
    public static int characterFireRateLevel = 0;
    public static int characterCriticalRateLevel = 0;
    public static int characterCriticalDamageLevel = 0;
    public static int characterDefenceLevel = 0;
    public static int characterSpinSpeedLevel = 0;

    //===================================================================

    public static float criticalDamage;
    public static float damage;
    public static float hp;
    public static float fireRate;
    public static float criticalRate;
    public static float defence;
    public static float spinSpeed;

    //===================================================================

    public static int criticalDamageUpgradeCost;
    public static int damageUpgradeCost;
    public static int hpUpgradeCost;
    public static int fireRateUpgradeCost;
    public static int criticalRateUpgradeCost;
    public static int defenceUpgradeCost;
    public static int spinSpeedUpgradeCost;

    //==================================================================

    void Awake() {
        Hp();
        Damage();
        FireRate();
        CriticalRate();
        Defence();
        SpinSpeed();
        CriticalDamage();
    }
    public float Hp() {
        hp = 100f + 10f*characterHpLevel;
        return hp;
    }
    public float Damage() {
        damage = 10f + 10f*characterDamageLevel;
        return damage;
    }
    public float FireRate() {
        fireRate = 1f - 0.05f*characterFireRateLevel;
        return fireRate;
    }
    public float CriticalRate() {
        criticalRate = 0.01f*characterCriticalRateLevel;
        return criticalRate;
    }
    public float Defence() {
        defence = 10f + 15f*characterDefenceLevel;
        return defence;
    }
    public float SpinSpeed() {
        spinSpeed = 1f + 0.5f*characterSpinSpeedLevel;
        return spinSpeed;
    }
    public float CriticalDamage() {
        criticalDamage = (2f + 0.5f*characterCriticalDamageLevel)*damage;
        return criticalDamage;
    }

    //=================================================================
    public int HpUpgradeCost() {
        int hpUpgradeCost = 50 + 10*characterHpLevel;
        return hpUpgradeCost;
    }
    public int DamageUpgradeCost() {
        int damageUpgradeCost = 50 + 10*characterDamageLevel;
        return damageUpgradeCost;
    }
    public int FireRateUpgradeCost() {
        int fireRateUpgradeCost = 50 + 10*characterFireRateLevel;
        return fireRateUpgradeCost;
    }
    public int CriticalRateUpgradeCost() {
        int criticalRateUpgradeCost = 50 + 10*characterCriticalRateLevel;
        return criticalRateUpgradeCost;
    }
    public int DefenceUpgradeCost() {
        int defenceUpgradeCost = 50 + 10*characterDefenceLevel;
        return defenceUpgradeCost;
    }
    public int SpinSpeedUpgradeCost() {
        int spinSpeedUpgradeCost = 50 + 10*characterSpinSpeedLevel;
        return spinSpeedUpgradeCost;
    }
    public int CriticalDamageUpgradeCost() {
        int criticalDamageUpgradeCost = 50 + 10*characterCriticalDamageLevel;
        return criticalDamageUpgradeCost;
    }
}
