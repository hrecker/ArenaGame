using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : MonoBehaviour 
{
    public GameObject bombPrefab;
    public float minFireInterval;
    private float timeSinceLastFire;

    void Start()
    {
        timeSinceLastFire = minFireInterval;
    }

    void Update()
    {
        timeSinceLastFire += Time.deltaTime;
    }

    public void PlaceBomb()
    {
        if (timeSinceLastFire >= minFireInterval)
        {
            Instantiate(bombPrefab, transform.position, transform.rotation);
            timeSinceLastFire = 0;
        }
    }
}
