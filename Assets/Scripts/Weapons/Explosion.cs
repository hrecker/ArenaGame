using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionSpriteDuration = 0.5f;
    public float explosionDuration = 0.5f;
    public int damage = 2;
    private Collider2D explosionCollider;
    private float currentTimePassed;
    
    void Start ()
    {
        explosionCollider = GetComponent<Collider2D>();
        //explosionCollider.enabled = false;
    }
	
	void Update () 
	{
        currentTimePassed += Time.deltaTime;
        if (currentTimePassed >= explosionDuration)
        {
            explosionCollider.enabled = false;
        }
        if (currentTimePassed >= explosionSpriteDuration)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
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
