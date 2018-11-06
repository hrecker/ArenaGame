using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponControl : MonoBehaviour, IPauseable
{
    private float currentFireDelay;
    public GameObject player;
    private WeaponBase weapon;
    private WeaponMods mods;

    private bool paused = false;

    private void Start()
    {
        mods = GetComponent<WeaponMods>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        weapon = new SimpleGun(mods, false);
        weapon.minFireInterval = 0.5f;
    }

    void Update()
    {
        if (paused)
        {
            return;
        }

        if (player != null)
        {
            currentFireDelay += Time.deltaTime;
            if (currentFireDelay >= weapon.minFireInterval)
            {
                Vector2 direction = player.transform.position - transform.position;
                weapon.Fire(currentFireDelay, direction, transform);
                currentFireDelay = 0;
            }
        }
        else
        {
            this.enabled = false;
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