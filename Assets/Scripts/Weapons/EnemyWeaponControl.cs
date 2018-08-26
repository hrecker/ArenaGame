using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponControl : MonoBehaviour
{
    public float fireDelay = 0.5f;
    private float currentFireDelay;
    public GameObject player;
    private WeaponBase weapon;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        weapon = GetComponent<WeaponBase>();   
    }

    void Update()
    {
        if (player != null)
        {
            currentFireDelay += Time.deltaTime;
            if (currentFireDelay >= fireDelay)
            {
                Vector2 direction = player.transform.position - transform.position;
                weapon.Fire(direction);
                currentFireDelay = 0;
            }
        }
        else
        {
            this.enabled = false;
        }
    }
}