using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponControl : MonoBehaviour 
{
    [Range(0, 1.0f)]
    public float deadzone = 0.5f;
    private float timeSinceLastFire;
    private WeaponBase weapon;
    private WeaponMods weaponMods;

    void OnEnable()
    {
        weaponMods = GetComponent<WeaponMods>();
        if (weapon == null)
        {
            weapon = WeaponType.GetWeapon(weaponMods, "SimpleGun", true);
        }
        timeSinceLastFire = weapon.minFireInterval;
    }

    void Update ()
    {
        timeSinceLastFire += Time.deltaTime;
        // Control main weapon
        float h = Input.GetAxis("Horizontal2");
        float v = Input.GetAxis("Vertical2");
        Vector2 weaponAxis = new Vector2(h, v);
        if (weaponAxis.magnitude > deadzone)
        {
            weapon.SetFireHeld(true);
            if (weapon.Fire(timeSinceLastFire, weaponAxis, transform))
            {
                timeSinceLastFire = 0;
            }
        }
        else
        {
            weapon.SetFireHeld(false);
        }
    }

    public void SetWeapon(string newWeaponName)
    {
        weapon = WeaponType.GetWeapon(weaponMods, newWeaponName, true);
    }
}
