using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : WeaponBase
{
    public GameObject grenadePrefab;
    public float bulletSpeed = 10;
    private bool isPlayerOwned;

    public GrenadeLauncher(WeaponMods weaponMods, bool isPlayerControlled)
    {
        minFireInterval = 0.75f;
        baseWeaponDamage = 2;
        mods = weaponMods;
        isPlayerOwned = isPlayerControlled;
        if (isPlayerOwned)
        {
            grenadePrefab = Resources.Load<GameObject>("Prefabs/Grenade");
        }
        else
        {
            //TODO enemy grenades?
        }
    }

    public override bool Fire (float timeSinceLastFire, Vector2 direction, Transform currentTransform)
    {
        GameObject grenade = mods.FireSimpleProjectile(grenadePrefab, direction, currentTransform,
            timeSinceLastFire, minFireInterval, baseWeaponDamage, bulletSpeed);
        if (grenade != null)
        {
            Grenade grenadeScript = grenade.GetComponent<Grenade>();
            grenadeScript.isPlayerOwned = isPlayerOwned;
            return true;
        }
        return false;
    }
}
