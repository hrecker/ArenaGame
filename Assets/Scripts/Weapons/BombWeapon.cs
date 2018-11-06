using UnityEngine;

public class BombWeapon : MonoBehaviour, IPauseable
{
    public GameObject bombPrefab;
    public float minFireInterval;
    private float timeSinceLastFire;

    private bool paused = false;

    void Start()
    {
        timeSinceLastFire = minFireInterval;
    }

    void Update()
    {
        if (paused)
        {
            return;
        }

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

    public void OnPause()
    {
        paused = true;
    }

    public void OnResume()
    {
        paused = false;
    }
}
