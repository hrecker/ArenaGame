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
            WeaponBase playerWeapon = player.GetComponent<WeaponBase>();
            playerWeapon.weaponDamage += item.itemEffectQuantity;
        }

        // TODO other item types
    }
}
