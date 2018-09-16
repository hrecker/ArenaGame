using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotDamage : ShotDamageBase
{
    public override void OnTriggerEnter2D(Collider2D collider)
    {
        // If a blocker (wall, shield, obstacle) is hit, destroy this shot
        if (destroyOnBlockerHit && (collider.gameObject.CompareTag("EnemyBlocker") || collider.gameObject.CompareTag("Blocker")))
        {
            hits++;
            Destroy(gameObject);
            return;
        }

        EnemyHealth enemy = collider.GetComponent<EnemyHealth>();
        if (enemy != null && (hits < maxHitsBeforeDestroyed || maxHitsBeforeDestroyed == 0))
        {
            hits++;
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        if (hits >= maxHitsBeforeDestroyed && maxHitsBeforeDestroyed != 0)
        {
            Destroy(gameObject);
        }
    }
}
