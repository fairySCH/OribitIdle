using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;

    public void SetDamage(float value)
    {
        damage = value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Character character = other.gameObject.GetComponent<Character>();
            if (character != null)
            {
                character.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
