using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IPauseable 
{
    public GameObject enemyPrefab;
    public float spawnDelay;
    private float currentDelayPassed;
    bool spawned = false;

    private bool paused = false;

    private void Update()
    {
        if (paused)
        {
            return;
        }

        currentDelayPassed += Time.deltaTime;
        if (!spawned && currentDelayPassed > spawnDelay)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
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
