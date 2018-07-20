using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour 
{
    public GameObject explosion;
    public float explosionDelay = 3.0f;
    private float currentExplosionDelay;
	
	void FixedUpdate () 
	{
		currentExplosionDelay += Time.deltaTime;
        if (currentExplosionDelay >= explosionDelay)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            currentExplosionDelay *= -1;
        }
	}
}
