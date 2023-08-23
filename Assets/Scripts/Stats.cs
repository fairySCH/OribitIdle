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
    public static float criticalDamage;
    public static float damage = 0;
    public static float hp = 0;
    public static float fireRate = 0;
    public static float criticalRate = 0;
    public static float defence = 0;
    public static float spinSpeed = 0;
    
    void Start() {
    // Initialize level&stats
        characterHpLevel = 0;
        characterDamageLevel = 0;
        characterFireRateLevel = 0;
        characterCriticalRateLevel = 0;
        characterCriticalDamageLevel = 0;
        characterDefenceLevel = 0;
        characterSpinSpeedLevel = 0;
        criticalDamage = 0;
        damage = 0;
        hp = 0;
        fireRate = 0;
        criticalRate = 0;
        defence = 0;
        spinSpeed = 0;
    }
    public float Hp() {
        float hp = 100 + 10*characterHpLevel;
        return hp;
    }
    public float Damage() {
        float damage = 10 + 10*characterDamageLevel;
        return damage;
    }
    public float FireRate() {
        float fireRate = 1 - 0.05f*characterFireRateLevel;
        return fireRate;
    }
    public float CriticalRate() {
        float criticalRate = 0.01f*characterCriticalRateLevel;
        return criticalRate;
    }
    public float Defence() {
        float defence = 10 + 15f*characterDefenceLevel;
        return defence;
    }
    public float SpinSpeed() {
        float spinSpeed = 1f + 0.5f*characterSpinSpeedLevel;
        return spinSpeed;
    }
        public float CriticalDamage() {
        float criticalDamage = (2 + 0.5f*characterCriticalDamageLevel)*damage;
        return criticalDamage;
    }
}
