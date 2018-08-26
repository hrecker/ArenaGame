using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExecutor 
{
    public static void ExecuteItem(Item item)
    {
        GameObject player = GameObject.Find("Player");

        if (item.type == ItemEffectType.HEAL)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.GainHealth(item.itemEffectQuantity);
        }

        if (item.type == ItemEffectType.BUFFDAMAGE)
        {
            WeaponBase[] playerWeapons = player.GetComponents<WeaponBase>();
            foreach (WeaponBase playerWeapon in playerWeapons)
            {
                playerWeapon.baseWeaponDamage += item.itemEffectQuantity;
            }
        }

        if (item.type == ItemEffectType.WEAPON)
        {
            PlayerChargeWeaponControl chargeControl = player.GetComponent<PlayerChargeWeaponControl>();
            PlayerWeaponControl weaponControl = player.GetComponent<PlayerWeaponControl>();

            // destroy current weapon
            WeaponBase[] currentWeapons = player.GetComponents<WeaponBase>();
            foreach (WeaponBase weapon in currentWeapons)
            {
                UnityEngine.Object.Destroy(weapon);
            }

            // add new weapon
            Type weaponType = WeaponParser.GetWeaponScriptFromName(item.name);
            if (weaponType.IsSubclassOf(typeof(ChargeWeaponBase))) // charge based weapon
            {
                chargeControl.enabled = true;
                weaponControl.enabled = false;
            }
            else // normal weapon
            {
                chargeControl.enabled = false;
                weaponControl.enabled = true;
            }
            player.AddComponent(weaponType);
        }

        // TODO other item types
    }
}

class WeaponParser
{
    public static Type GetWeaponScriptFromName(string name)
    {
        if (name == "ChargeGun")
        {
            return typeof(ChargeGun);
        }
        else if (name == "SimpleGun")
        {
            return typeof(SimpleGun);
        }
        else // Default to simplegun
        {
            return typeof(SimpleGun);
        }
    }
}
