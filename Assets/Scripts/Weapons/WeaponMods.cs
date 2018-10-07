using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMods : MonoBehaviour 
{
    public float damageBuffs = 0; //Additive buffs and debuffs, positive for increased damage, negative for decreased
    public float damageMultiplier = 1;
    public float shotSpeedMultiplier = 1;
    public float levelChargeDelayMultiplier = 1;
    public float fireDelayMultiplier = 1;
    public int maxBounces;

    // Instantiates a simple projectile and returns the created object
    // If not enough time has passed based on fire delay, returns null
    public GameObject FireSimpleProjectile(GameObject projectilePrefab, Vector2 direction, Transform transform, 
        float timeSinceLastFire, float baseFireDelay,
        float baseDamage, float baseSpeed)
    {
        if (timeSinceLastFire < (baseFireDelay * fireDelayMultiplier))
        {
            return null;
        }

        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        //TODO eventually make movement more generic
        StraightConstantMovement bulletMovement = projectile.GetComponent<StraightConstantMovement>();
        bulletMovement.velocity = direction.normalized * (baseSpeed * shotSpeedMultiplier);
        ShotDamage damage = projectile.GetComponent<ShotDamage>();
        damage.damage = (baseDamage + damageBuffs) * damageMultiplier;
        if (maxBounces >= 1)
        {
            damage.bouncy = true;
            damage.maxBouncesBeforeDestroyed = maxBounces;
            damage.destroyOnBlockerHit = false;
        }
        //TODO more multipliers
        return projectile;
    }


    // Instantiates a charge projectile and returns the created object
    // If not enough time has passed based on fire delay, returns null
    public GameObject FireChargedSimpleProjectile(GameObject[] projectilePrefabs, Vector2 direction, Transform transform,
        float timeSinceLastFire, float baseFireDelay,
        float baseLevelChargeDelay, float chargeHoldTime,
        float baseSpeed)
    {
        if (timeSinceLastFire < (baseFireDelay * fireDelayMultiplier))
        {
            return null;
        }
        
        GameObject chargedPrefab = projectilePrefabs[GetChargeLevel(baseLevelChargeDelay, chargeHoldTime, projectilePrefabs.Length) - 1];

        GameObject projectile = Instantiate(chargedPrefab, transform.position, transform.rotation);
        //TODO eventually make movement more generic
        StraightConstantMovement bulletMovement = projectile.GetComponent<StraightConstantMovement>();
        bulletMovement.velocity = direction.normalized * (baseSpeed * shotSpeedMultiplier);
        ShotDamage damage = projectile.GetComponent<ShotDamage>();
        damage.damage = (damage.damage + damageBuffs) * damageMultiplier;
        //TODO more multipliers
        return projectile;
    }

    // 1 is base level, and so on up
    // TODO allow different charge times for different charge levels
    public int GetChargeLevel(float baseLevelChargeDelay, float chargeHoldTime, int totalChargeLevels)
    {
        float chargeDelay = baseLevelChargeDelay * levelChargeDelayMultiplier;
        for (int i = 1; i <= totalChargeLevels; i++)
        {
            if (chargeHoldTime < (i * chargeDelay))
            {
                return i;
            }
        }
        return totalChargeLevels;
    }
}
