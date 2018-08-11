using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGun : WeaponBase 
{
    public GameObject bulletPrefab;
    public float minFireInterval;
    public float bulletSpeed = 15;
    private float timeSinceLastFire;
    private PlayerMovement movement;

    void Start()
    {
        timeSinceLastFire = minFireInterval;
    }

	void Update () 
	{
        timeSinceLastFire += Time.deltaTime;
	}

    public override void Fire (Vector2 direction)
    {
        if (timeSinceLastFire >= minFireInterval)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            StraightConstantMovement bulletMovement = bullet.GetComponent<StraightConstantMovement>();
            bulletMovement.velocity = direction.normalized * bulletSpeed;
            PlayerShotDamage damage = bullet.GetComponent<PlayerShotDamage>();
            damage.damage = weaponDamage;
            timeSinceLastFire = 0;
        }
    }
}
