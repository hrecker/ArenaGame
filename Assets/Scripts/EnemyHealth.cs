using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour 
{
    public int health = 3;
    public int maxHealth = 3;

    void Start ()
    {
        health = maxHealth;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Hit by player bullet
        // TODO make this better
        if (collision.gameObject.name.StartsWith("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }
}
