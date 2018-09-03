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
            WeaponMods mods = player.GetComponent<WeaponMods>();
            mods.damageBuffs += 1;
        }

        if (item.type == ItemEffectType.WEAPON)
        {
            PlayerChargeWeaponControl chargeControl = player.GetComponent<PlayerChargeWeaponControl>();
            PlayerWeaponControl weaponControl = player.GetComponent<PlayerWeaponControl>();

            // add new weapon
            Type weaponType = WeaponParser.GetWeaponScriptFromName(item.name);
            if (weaponType.IsSubclassOf(typeof(ChargeWeaponBase))) // charge based weapon
            {
                chargeControl.enabled = true;
                weaponControl.enabled = false;
                chargeControl.SetWeapon(item.name);
            }
            else // normal weapon
            {
                chargeControl.enabled = false;
                weaponControl.enabled = true;
                weaponControl.SetWeapon(item.name);
            }
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
        else if (name == "GrenadeLauncher")
        {
            return typeof(GrenadeLauncher);
        }
        else // Default to simplegun
        {
            return typeof(SimpleGun);
        }
    }
}
