using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour 
{
    public int health = 3;
    public int maxHealth = 5;
    public float invincibilityTimeOnHit = 2.0f;
    private float invincibilityFlashSpeed = 0.15f;
    private float invincibilityRemaining;
    private float invincibilityFlashRemaining;
    private IMessenger messenger;
    private SpriteRenderer spriteRenderer;
    private bool invincible = false;

    void Start()
    {
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
        if (collider.gameObject.CompareTag("EnemyShot"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!invincible)
        {
            TakeDirectDamage(damage);
        }
    }

    // Take damage regardless of invincibility
    public void TakeDirectDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }

        if (damage > 0)
        {
            invincible = true;
            invincibilityRemaining = invincibilityTimeOnHit;
            invincibilityFlashRemaining = invincibilityFlashSpeed;
            spriteRenderer.enabled = false;
            // Send message to any other player scripts
            messenger.Invoke(Message.HEALTH_LOST, new object[] { health, -damage });
        }
        if (health == 0)
        {
            messenger.Invoke(Message.NO_HEALTH_REMAINING, new object[] { damage });
        }
    }

    public void GainHealth(int gained)
    {
        if (health >= maxHealth)
        {
            gained = 0;
        }
        health += gained;

        if (gained > 0)
        {
            SceneMessenger.Instance.Invoke(Message.PLAYER_HEALTH_GAINED, new object[] { health, gained });
        }
    }
}
