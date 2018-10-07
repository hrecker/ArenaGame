using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotDamage : MonoBehaviour 
{
    public bool isPlayerOwned = true;

    public bool destroyOnBlockerHit = true;

    public float damage = 1;
    // If max hits before destroyed is 0, this shot can hit any number of times and will not be destroyed
    public int maxHitsBeforeDestroyed = 1;
    private int hits;

    public bool bouncy = false;
    public int maxBouncesBeforeDestroyed = 5;
    private int bounces;

    private StraightConstantMovement movement;
    private CircleCollider2D circleCollider;

    public virtual void Awake()
    {
        //TODO allow other movements?
        movement = GetComponent<StraightConstantMovement>();
        //TODO allow other shot shapes?
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        // If a blocker (wall, shield, obstacle) is hit, destroy this shot
        if (destroyOnBlockerHit && WasBlockerHit(collider))
        {
            hits++;
            Destroy(gameObject);
            return;
        }

        // If anything other than an enemy blocker or shot is hit, bounce
        if (ShouldBounce(collider))
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

        DealDamage(collider);

        if (hits >= maxHitsBeforeDestroyed && maxHitsBeforeDestroyed != 0)
        {
            Destroy(gameObject);
        }
        else if (bouncy && bounces > maxBouncesBeforeDestroyed)
        {
            Destroy(gameObject);
        }
    }

    private bool WasBlockerHit(Collider2D collider)
    {
        // Check for blockers based on whether this is a player controlled shot or not
        return ((isPlayerOwned && (collider.gameObject.CompareTag("EnemyBlocker") || collider.gameObject.CompareTag("Blocker"))) ||
            (!isPlayerOwned && (collider.gameObject.CompareTag("PlayerBlocker") || collider.gameObject.CompareTag("Blocker"))));
    }

    private bool ShouldBounce(Collider2D collider)
    {
        return bouncy && bounces <= maxBouncesBeforeDestroyed &&
            // Checks for player controlled
            ((isPlayerOwned && !collider.gameObject.CompareTag("PlayerBlocker") && !collider.gameObject.CompareTag("Player") &&
            !collider.gameObject.CompareTag("PlayerShot")) ||
            // Checks for enemy controlled
            (!isPlayerOwned && !collider.gameObject.CompareTag("EnemyBlocker") && !collider.gameObject.CompareTag("EnemyShot")));
    }

    private void DealDamage(Collider2D collider)
    {
        if (isPlayerOwned)
        {
            EnemyHealth enemy = collider.GetComponent<EnemyHealth>();
            if (enemy != null && (hits < maxHitsBeforeDestroyed || maxHitsBeforeDestroyed == 0))
            {
                hits++;
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
        else
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
        }
    }
}
