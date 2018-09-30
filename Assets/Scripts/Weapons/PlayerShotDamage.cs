using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotDamage : ShotDamageBase
{
    private StraightConstantMovement movement;
    private CircleCollider2D circleCollider;

    private void Awake()
    {
        movement = GetComponent<StraightConstantMovement>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        // If a blocker (wall, shield, obstacle) is hit, destroy this shot
        if (destroyOnBlockerHit && (collider.gameObject.CompareTag("EnemyBlocker") || collider.gameObject.CompareTag("Blocker")))
        {
            hits++;
            Destroy(gameObject);
            return;
        }

        // If anything other than the player or a player blocker is hit, bounce
        if (bouncy && !collider.gameObject.CompareTag("PlayerBlocker") && !collider.gameObject.CompareTag("Player") &&
            !collider.gameObject.CompareTag("PlayerShot") && bounces <= maxBouncesBeforeDestroyed)
        {
            bounces++;
            if (movement != null && circleCollider != null)
            {
                //bounce
                RaycastHit2D hit = Physics2D.Raycast(transform.position, movement.velocity,
                    circleCollider.radius, Physics.DefaultRaycastLayers);
                if (hit)
                {
                    movement.velocity = Vector2.Reflect(movement.velocity, hit.normal);
                }
            }
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
        else if (bouncy && bounces > maxBouncesBeforeDestroyed)
        {
            Destroy(gameObject);
        }
    }
}
