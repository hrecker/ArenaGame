using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
    public GameObject enemyPrefab;
    public float spawnDelay;
    private float currentDelayPassed;
    bool spawned = false;

    private void Update()
    {
        currentDelayPassed += Time.deltaTime;
        if (!spawned && currentDelayPassed > spawnDelay)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
