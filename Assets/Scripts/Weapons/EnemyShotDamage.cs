using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotDamage : ShotDamageBase
{
    public override void OnTriggerEnter2D(Collider2D collider)
    {
        // If a blocker (wall, shield, obstacle) is hit, destroy this shot
        if (destroyOnBlockerHit && (collider.gameObject.CompareTag("PlayerBlocker") || collider.gameObject.CompareTag("Blocker")))
        {
            hits++;
            Destroy(gameObject);
            return;
        }

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
