// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Stats : MonoBehaviour
// {
//     public static int characterHpLevel = 0;
//     public static int characterDamageLevel = 0;
//     public static int characterFireRateLevel = 0;
//     public static int characterCriticalRateLevel = 0;
//     public static int characterCriticalDamageLevel = 0;
//     public static int characterDefenceLevel = 0;
//     public static int characterSpinSpeedLevel = 0;

//     //===================================================================

//     public static float hp = 100f;
//     public static float damage = 10f;
//     public static float fireRate = 1f;
//     public static float criticalRate = 0.01f;
//     public static float criticalDamage = 2f;
//     public static float defence = 10f;
//     public static float spinSpeed = 1f;

//     //===================================================================

//     public static int hpUpgradeCost = 50;
//     public static int damageUpgradeCost = 50;
//     public static int fireRateUpgradeCost = 50;
//     public static int criticalRateUpgradeCost = 50;
//     public static int criticalDamageUpgradeCost = 50;
//     public static int defenceUpgradeCost = 50;
//     public static int spinSpeedUpgradeCost = 50;

//     //==================================================================

//     public void UpdateHp() {
//         hp = 100f + 10f*characterHpLevel;
//     }
//     public void UpdateDamage() {
//         damage = 10f + 10f*characterDamageLevel;
//     }
//     public void UpdateFireRate() {
//         fireRate = 1f - 0.05f*characterFireRateLevel;
//     }
//     public void UpdateCriticalRate() {
//         criticalRate = 0.01f*characterCriticalRateLevel;
//     }
//     public void UpdateCriticalDamage() {
//         criticalDamage = (2f + 0.5f*characterCriticalDamageLevel)*damage;
//     }
//     public void UpdateDefence() {
//         defence = 10f + 15f*characterDefenceLevel;
//     }
//     public void UpdateSpinSpeed() {
//         spinSpeed = 1f + 0.5f*characterSpinSpeedLevel;
//     }
    

//     //=================================================================
//     public void UpdateHpUpgradeCost() {
//         hpUpgradeCost = 50 + 10*characterHpLevel;
//     }
//     public void UpdateDamageUpgradeCost() {
//         damageUpgradeCost = 50 + 10*characterDamageLevel;
//     }
//     public void UpdateFireRateUpgradeCost() {
//         fireRateUpgradeCost = 50 + 10*characterFireRateLevel;
//     }
//     public void UpdateCriticalRateUpgradeCost() {
//         criticalRateUpgradeCost = 50 + 10*characterCriticalRateLevel;
//     }
//     public void UpdateCriticalDamageUpgradeCost() {
//         criticalDamageUpgradeCost = 50 + 10*characterCriticalDamageLevel;
//     }
//     public void UpdateDefenceUpgradeCost() {
//         defenceUpgradeCost = 50 + 10*characterDefenceLevel;
//     }
//     public void UpdateSpinSpeedUpgradeCost() {
//         spinSpeedUpgradeCost = 50 + 10*characterSpinSpeedLevel;
//     }
// }
