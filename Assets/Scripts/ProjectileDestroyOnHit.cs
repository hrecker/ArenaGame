using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestroyOnHit : MonoBehaviour 
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
