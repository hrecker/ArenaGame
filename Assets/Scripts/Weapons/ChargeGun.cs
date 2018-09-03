using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeGun : ChargeWeaponBase
{
    public GameObject bulletLevel1;
    public GameObject bulletLevel2;
    public GameObject bulletLevel3;
    private GameObject[] allBullets;
    public float bulletSpeed = 15;
    
    public ChargeGun (WeaponMods weaponMods, bool isPlayerControlled)
    {
        minFireInterval = 0;
        mods = weaponMods;
        numLevels = 3;
        bulletLevel1 = Resources.Load<GameObject>("Prefabs/PlayerBullet");
        bulletLevel2 = Resources.Load<GameObject>("Prefabs/PlayerBulletLevel2");
        bulletLevel3 = Resources.Load<GameObject>("Prefabs/PlayerBulletLevel3");
        allBullets = new GameObject[] { bulletLevel1, bulletLevel2, bulletLevel3 };
    }

    public override bool Fire(float timeSinceLastFire, Vector2 direction, Transform transform, float chargeTime)
    {
        return mods.FireChargedSimpleProjectile(allBullets, direction, transform, 
            timeSinceLastFire, minFireInterval, 
            levelChargeTime, chargeTime,
            bulletSpeed) != null;
    }

    public override bool Fire(float timeSinceLastFire, Vector2 direction, Transform transform)
    {
        return Fire(timeSinceLastFire, direction, transform, 0);
    }
}
