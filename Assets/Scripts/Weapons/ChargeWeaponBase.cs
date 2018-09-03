using UnityEngine;

public abstract class ChargeWeaponBase : WeaponBase
{
    public float levelChargeTime = 1.0f;

    public int numLevels = 2;

    public abstract bool Fire(float timeSinceLastFire, Vector2 direction, Transform currentTransform, float chargeTime);
}
