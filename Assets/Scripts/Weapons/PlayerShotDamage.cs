﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotDamage : ShotDamageBase
{
    public override void OnTriggerEnter2D(Collider2D collider)
    {
        EnemyHealth enemy = collider.GetComponent<EnemyHealth>();
        if (enemy != null && hits < maxHitsBeforeDestroyed)
        {
            hits++;
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
