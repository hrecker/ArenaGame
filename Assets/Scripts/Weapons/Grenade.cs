using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : ShotDamage
{
    public GameObject explosion;

    public override void Awake() { } 

    // Explode on any contact
    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isPlayerOwned || collider.gameObject.tag != "Player")
        {
            GameObject explosionInstance = Instantiate(explosion, transform.position, Quaternion.identity);
            explosionInstance.GetComponent<Explosion>().damage = damage;
            Destroy(gameObject);
        }
    }
}
