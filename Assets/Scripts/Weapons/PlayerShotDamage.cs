using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotDamage : MonoBehaviour 
{
    public int damage = 1;
    public int maxHitsBeforeDestroyed = 1;
    private int hits;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("Player") && hits < maxHitsBeforeDestroyed)
        {
            hits++;
            EnemyHealth enemy = collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        if (hits >= maxHitsBeforeDestroyed)
        {
            Destroy(gameObject);
        }
    }
}
