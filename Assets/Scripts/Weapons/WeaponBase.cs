using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour 
{
    public int baseWeaponDamage = 1;

    public abstract void Fire(Vector2 direction);
}
