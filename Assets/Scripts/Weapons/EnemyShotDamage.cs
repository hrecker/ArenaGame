using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotDamage : ShotDamageBase
{
    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && hits < maxHitsBeforeDestroyed)
        {
            hits++;
            PlayerHealth player = collider.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
        if (hits >= maxHitsBeforeDestroyed)
        {
            Destroy(gameObject);
        }
    }
}
