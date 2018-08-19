using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChargeWeaponBase : WeaponBase
{
    public float levelChargeTime = 1.0f;

    public float minFireInterval = 0.25f;

    public int numLevels = 2;

    public abstract void Fire(Vector2 direction, float chargeTime);
}
