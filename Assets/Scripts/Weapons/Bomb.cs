using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IPauseable
{
    public GameObject explosion;
    public float explosionDelay = 3.0f;
    private float currentExplosionDelay;

    private bool paused = false;
	
	void FixedUpdate ()
    {
        if (paused)
        {
            return;
        }

        currentExplosionDelay += Time.deltaTime;
        if (currentExplosionDelay >= explosionDelay)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            currentExplosionDelay *= -1;
        }
    }

    public void OnPause()
    {
        paused = true;
    }

    public void OnResume()
    {
        paused = false;
    }
}
