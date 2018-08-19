using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeGun : ChargeWeaponBase
{
    public GameObject bulletLevel1;
    public GameObject bulletLevel2;
    public GameObject bulletLevel3;
    public float bulletSpeed = 15;

    void Start()
    {
        numLevels = 3;
    }

    public override void Fire(Vector2 direction, float chargeTime)
    {
        if (chargeTime >= 2 * levelChargeTime) // fire level 3 bullet
        {
            Fire(direction, bulletLevel3, baseWeaponDamage + 2);
        }
        else if (chargeTime >= levelChargeTime) // fire level 2 bullet
        {
            Fire(direction, bulletLevel2, baseWeaponDamage + 1);
        }
        else // fire basic bullet
        {
            Fire(direction, bulletLevel1, baseWeaponDamage);
        }
    }

    public override void Fire(Vector2 direction)
    {
        Fire(direction, 0);
    }

    private void Fire(Vector2 direction, GameObject bulletPrefab, int bulletDamage)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        StraightConstantMovement bulletMovement = bullet.GetComponent<StraightConstantMovement>();
        bulletMovement.velocity = direction.normalized * bulletSpeed;
        ShotDamageBase damage = bullet.GetComponent<ShotDamageBase>();
        damage.damage = bulletDamage;
    }
}
