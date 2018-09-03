using UnityEngine;

public abstract class WeaponBase 
{
    public int baseWeaponDamage = 1;
    public float minFireInterval = 0.25f;

    protected WeaponMods mods;

    protected bool fireHeld;

    // return true if fired
    public abstract bool Fire(float timeSinceLastFire, Vector2 direction, Transform currentTransform);

    public virtual void SetFireHeld(bool fireHeld)
    {
        this.fireHeld = fireHeld;
    }
}
