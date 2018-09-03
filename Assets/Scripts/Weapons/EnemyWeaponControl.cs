using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponControl : MonoBehaviour
{
    private float currentFireDelay;
    public GameObject player;
    private WeaponBase weapon;
    private WeaponMods mods;

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
}