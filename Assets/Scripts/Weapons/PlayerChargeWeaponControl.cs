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
    private ChargeWeaponBase weapon;
    private WeaponMods weaponMods;

    void OnEnable()
    {
        weaponMods = GetComponent<WeaponMods>();
        if (weapon == null)
        {
            weapon = (ChargeWeaponBase) WeaponType.GetWeapon(weaponMods, "ChargeGun", true);
        }
        currentChargeLevel = 0;
        timeSinceLastFire = weapon.minFireInterval;
    }

    void Update()
    {
        timeSinceLastFire += Time.deltaTime;
        // Control main weapon
        float h = Input.GetAxis("Horizontal2");
        float v = Input.GetAxis("Vertical2");
        Vector2 weaponAxis = new Vector2(h, v);
        float heldButton = Input.GetAxis("TriggerRight");
        if (heldButton > 0)
        {
            currentChargeTime += Time.deltaTime;
            currentlyCharging = true;
            UpdateChargeLevel();
        }
        else if (weaponAxis.magnitude > deadzone)
        {
            if (currentlyCharging && weapon.Fire(timeSinceLastFire, weaponAxis, transform, currentChargeTime))
            {
                timeSinceLastFire = 0;
            }
            currentChargeTime = 0;
            currentChargeLevel = 0;
            currentlyCharging = false;
            chargeLevelRenderer.enabled = false;
        }
    }

    private void UpdateChargeLevel()
    {
        currentChargeLevel = weaponMods.GetChargeLevel(weapon.levelChargeTime, currentChargeTime, weapon.numLevels);
        chargeLevelRenderer.sprite = chargeLevelSprites[currentChargeLevel - 1];
        chargeLevelRenderer.enabled = true;
    }

    public void SetWeapon(string newWeaponName)
    {
        weapon = (ChargeWeaponBase) WeaponType.GetWeapon(weaponMods, newWeaponName, true);
    }
}
