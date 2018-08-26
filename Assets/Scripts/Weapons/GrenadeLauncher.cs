using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : WeaponBase
{
    public GameObject grenadePrefab;
    public float minFireInterval = 0.75f;
    public float bulletSpeed = 10;
    private float timeSinceLastFire;
    private bool playerOwned;

    void Start()
    {
        baseWeaponDamage = 2;
        timeSinceLastFire = minFireInterval;
        playerOwned = gameObject.tag == "Player";
        if (playerOwned)
        {
            grenadePrefab = Resources.Load<GameObject>("Prefabs/Grenade");
        }
    }

    void Update()
    {
        timeSinceLastFire += Time.deltaTime;
    }

    public override void Fire(Vector2 direction)
    {
        if (timeSinceLastFire >= minFireInterval)
        {
            GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
            StraightConstantMovement grenadeMovement = grenade.GetComponent<StraightConstantMovement>();
            grenadeMovement.velocity = direction.normalized * bulletSpeed;
            Grenade grenadeScript = grenade.GetComponent<Grenade>();
            grenadeScript.playerOwned = playerOwned;
            grenadeScript.damage = baseWeaponDamage;
            timeSinceLastFire = 0;
        }
    }
}
