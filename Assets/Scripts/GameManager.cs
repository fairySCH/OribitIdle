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

    public Weapon weapon;
    void Awake() {
        if (instance == null) {
            instance = this;
        }
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
}
