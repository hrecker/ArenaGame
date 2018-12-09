using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherEnemyController : MonoBehaviour, IPauseable
{
    public float spawnDelay = 1.0f;
    public GameObject spawnEnemy;
    private float currentSpawnDelay;

    private bool paused = false;

    void Update()
    {
        if (paused)
        {
            return;
        }

        currentSpawnDelay += Time.deltaTime;
        if (currentSpawnDelay >= spawnDelay)
        {
            //spawn
            Instantiate(spawnEnemy, transform.position, transform.rotation);
            currentSpawnDelay = 0;
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
