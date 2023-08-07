using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public static float characterHpLevel;
    public float damage;

    public float CharacterHp() {
        float hp = 100 + 10*characterHpLevel;
        return hp;
    }
}
