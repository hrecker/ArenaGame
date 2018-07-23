using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionDuration = 0.5f;
    public int damage = 2;
    public float explosionRadius = 0.47f;
    
    void Start ()
    {
        Explode();
        Destroy(gameObject, explosionDuration);
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            DamageEntity(hit);
        }
    }

    private void DamageEntity(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player")) // player hit
        {
            PlayerHealth player = collider.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
        else if (collider.gameObject.CompareTag("EnemyShot")) // enemy hit
        {
            EnemyHealth enemy = collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
