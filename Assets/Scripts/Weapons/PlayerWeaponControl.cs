using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponControl : MonoBehaviour 
{
    [Range(0, 1.0f)]
    public float deadzone = 0.5f;
    private BombWeapon bombControl;

    private void Start()
    {
        bombControl = GetComponent<BombWeapon>();
    }

    void Update ()
    {
        // Control main weapon
        WeaponBase weapon = GetComponent<WeaponBase>();
        float h = Input.GetAxis("Horizontal2");
        float v = Input.GetAxis("Vertical2");
        Vector2 weaponAxis = new Vector2(h, v);
        if (weaponAxis.magnitude > deadzone)
        {
            weapon.Fire(weaponAxis);
        }

        // Control bombs
        if (Input.GetButtonDown("Fire2"))
        {
            bombControl.PlaceBomb();
        }
    }
}
