using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponControl : MonoBehaviour 
{
    [Range(0, 1.0f)]
    public float deadzone = 0.5f;

    void Update ()
    {
        // Control main weapon
        WeaponBase activeWeapon = GetActiveWeapon();
        float h = Input.GetAxis("Horizontal2");
        float v = Input.GetAxis("Vertical2");
        Vector2 weaponAxis = new Vector2(h, v);
        if (weaponAxis.magnitude > deadzone)
        {
            activeWeapon.Fire(weaponAxis);
        }
    }

    private WeaponBase GetActiveWeapon()
    {
        WeaponBase[] weapons = GetComponents<WeaponBase>();
        foreach (WeaponBase weapon in weapons)
        {
            if (weapon.enabled)
            {
                return weapon;
            }
        }
        return null;
    }
}
