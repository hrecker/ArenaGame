using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour 
{
    public int health = 3;
    public int maxHealth = 3;
    public float invincibilityTimeOnHit = 2.0f;
    private float invincibilityFlashSpeed = 0.15f;
    private float invincibilityRemaining;
    private float invincibilityFlashRemaining;
    private IMessenger messenger;
    private SpriteRenderer spriteRenderer;
    private bool invincible = false;

    void Start()
    {
        health = maxHealth;
        messenger = this.GetMessenger();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (invincible)
        {
            invincibilityRemaining -= Time.deltaTime;
            invincibilityFlashRemaining -= Time.deltaTime;
            if (invincibilityRemaining <= 0)
            {
                invincible = false;
                spriteRenderer.enabled = true;
            }
            else if (invincibilityFlashRemaining <= 0)
            {
                invincibilityFlashRemaining += invincibilityFlashSpeed;
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        TriggerCheck(collider);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        TriggerCheck(collider);
    }

    private void TriggerCheck(Collider2D collider)
    {
        if (!invincible)
        {
            if (collider.gameObject.CompareTag("EnemyShot"))
            {
                TakeDamage(1);
                invincible = true;
                invincibilityRemaining = invincibilityTimeOnHit;
                invincibilityFlashRemaining = invincibilityFlashSpeed;
                spriteRenderer.enabled = false;
            }
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }

        // Send message to any other player scripts
        if (damage > 0)
        {
            messenger.Invoke(Message.HEALTH_LOST, new object[] { health, damage });
        }
        if (health == 0)
        {
            messenger.Invoke(Message.NO_HEALTH_REMAINING, new object[] { damage });
        }
    }
}
