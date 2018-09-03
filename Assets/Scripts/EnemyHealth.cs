using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour 
{
    public int health = 3;
    public int maxHealth = 3;
    private IMessenger messenger;

    void Start ()
    {
        health = maxHealth;
        messenger = this.GetMessenger();
    }

    public void TakeDamage(float damage)
    {
        int intDamage = (int) damage;

        health -= intDamage;
        if (health < 0)
        {
            health = 0;
        }

        // Send message to any other enemy scripts
        if (intDamage > 0)
        {
            messenger.Invoke(Message.HEALTH_LOST, new object[] { health, intDamage });
        }
        if (health == 0)
        {
            messenger.Invoke(Message.NO_HEALTH_REMAINING, new object[] { intDamage });
        }
    }
}
