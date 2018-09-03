using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGun : WeaponBase
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 15;
    private float fireIntervalReductionPerShot = 0.05f;
    private float floorFireInterval = 0.1f;
    private float baseFireInterval = 0.6f;

    public MiniGun(WeaponMods weaponMods, bool isPlayerControlled)
    {
        minFireInterval = baseFireInterval;
        mods = weaponMods;
        if (isPlayerControlled)
        {
            bulletPrefab = Resources.Load<GameObject>("Prefabs/PlayerBullet");
        }
        else
        {
            bulletPrefab = Resources.Load<GameObject>("Prefabs/EnemyBullet");
        }
    }

    public override void SetFireHeld(bool fireHeld)
    {
        this.fireHeld = fireHeld;
        if (!fireHeld)
        {
            minFireInterval = baseFireInterval;
        }
    }

    public override bool Fire(float timeSinceLastFire, Vector2 direction, Transform transform)
    {
        GameObject projectile = mods.FireSimpleProjectile(bulletPrefab, direction, transform,
            timeSinceLastFire, minFireInterval, baseWeaponDamage, bulletSpeed);
        if (projectile != null)
        {
            if (fireHeld) // reduce fire interval as fire is held down
            {
                minFireInterval -= fireIntervalReductionPerShot;
                if (minFireInterval < floorFireInterval)
                {
                    minFireInterval = floorFireInterval;
                }
            }
            return true;
        }
        return false;
    }
}