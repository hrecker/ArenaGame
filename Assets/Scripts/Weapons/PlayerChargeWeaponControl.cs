using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargeWeaponControl : MonoBehaviour
{
    [Range(0, 1.0f)]
    public float deadzone = 0.5f;
    public Sprite[] chargeLevelSprites;
    public SpriteRenderer chargeLevelRenderer;
    private float currentChargeTime;
    private bool currentlyCharging;
    private int currentChargeLevel;
    private float timeSinceLastFire;

    private void Start()
    {
        currentChargeLevel = 0;
    }

    void Update()
    {
        timeSinceLastFire += Time.deltaTime;
        // Control main weapon
        ChargeWeaponBase activeWeapon = GetActiveWeapon();
        float h = Input.GetAxis("Horizontal2");
        float v = Input.GetAxis("Vertical2");
        Vector2 weaponAxis = new Vector2(h, v);
        float heldButton = Input.GetAxis("TriggerRight");
        if (heldButton > 0)
        {
            currentChargeTime += Time.deltaTime;
            currentlyCharging = true;
            UpdateChargeLevel(activeWeapon);
        }
        else if (weaponAxis.magnitude > deadzone)
        {
            if (currentlyCharging)
            {
                activeWeapon.Fire(weaponAxis, currentChargeTime);
                timeSinceLastFire = 0;
            }
            currentChargeTime = 0;
            currentChargeLevel = 0;
            currentlyCharging = false;
            chargeLevelRenderer.enabled = false;
        }
    }

    private ChargeWeaponBase GetActiveWeapon()
    {
        ChargeWeaponBase[] weapons = GetComponents<ChargeWeaponBase>();
        foreach (ChargeWeaponBase weapon in weapons)
        {
            if (weapon.enabled)
            {
                return weapon;
            }
        }
        return null;
    }

    private void UpdateChargeLevel(ChargeWeaponBase weapon)
    {
        // Move up a charge level
        if (currentChargeLevel < weapon.numLevels && currentChargeTime >= (currentChargeLevel * weapon.levelChargeTime))
        {
            currentChargeLevel++;
            // update sprite
            chargeLevelRenderer.sprite = chargeLevelSprites[currentChargeLevel - 1];
            chargeLevelRenderer.enabled = true;
        }
    }
}
