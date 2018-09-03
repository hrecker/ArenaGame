using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGun : WeaponBase 
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 15;

    public SimpleGun (WeaponMods weaponMods, bool isPlayerControlled)
    {
        minFireInterval = 0.25f;
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

    public override bool Fire (float timeSinceLastFire, Vector2 direction, Transform transform)
    {
        return mods.FireSimpleProjectile(bulletPrefab, direction, transform,
            timeSinceLastFire, minFireInterval, baseWeaponDamage, bulletSpeed) != null;
    }
}
