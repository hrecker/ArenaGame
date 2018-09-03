using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoShot : WeaponBase
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    public OctoShot(WeaponMods weaponMods, bool isPlayerControlled)
    {
        minFireInterval = 0.25f;
        mods = weaponMods;
        if (isPlayerControlled)
        {
            bulletPrefab = Resources.Load<GameObject>("Prefabs/PlayerBullet");
        }
        else
        {
            //TODO put this prefab in
            bulletPrefab = Resources.Load<GameObject>("Prefabs/EnemyBullet");
        }
    }

    // Fire in 8 directions
    public override bool Fire(float timeSinceLastFire, Vector2 direction, Transform currentTransform)
    {
        return FireSingleBullet(timeSinceLastFire, Vector2.up, currentTransform) &&
            FireSingleBullet(timeSinceLastFire, Vector2.up + Vector2.right, currentTransform) &&
            FireSingleBullet(timeSinceLastFire, Vector2.right, currentTransform) &&
            FireSingleBullet(timeSinceLastFire, Vector2.down + Vector2.right, currentTransform) &&
            FireSingleBullet(timeSinceLastFire, Vector2.down, currentTransform) &&
            FireSingleBullet(timeSinceLastFire, Vector2.down + Vector2.left, currentTransform) &&
            FireSingleBullet(timeSinceLastFire, Vector2.left, currentTransform) &&
            FireSingleBullet(timeSinceLastFire, Vector2.up + Vector2.left, currentTransform);
    }

    private bool FireSingleBullet(float timeSinceLastFire, Vector2 direction, Transform currentTransform)
    {
        return mods.FireSimpleProjectile(bulletPrefab, direction, currentTransform,
            timeSinceLastFire, minFireInterval, baseWeaponDamage, bulletSpeed) != null;
    }
}
