using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotDamage : ShotDamageBase
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
        if (destroyOnBlockerHit && (collider.gameObject.CompareTag("PlayerBlocker") || collider.gameObject.CompareTag("Blocker")))
        {
            hits++;
            Destroy(gameObject);
            return;
        }

        // If anything other than an enemy blocker or shot is hit, bounce
        if (bouncy && !collider.gameObject.CompareTag("EnemyBlocker") && 
            !collider.gameObject.CompareTag("EnemyShot") && bounces <= maxBouncesBeforeDestroyed)
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
        else if (bouncy && bounces > maxBouncesBeforeDestroyed)
        {
            Destroy(gameObject);
        }
    }
}
