using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGun : WeaponBase 
{
    public GameObject bulletPrefab;
    public float minFireInterval = 0.25f;
    public float bulletSpeed = 15;
    private float timeSinceLastFire;

    void Start()
    {
        timeSinceLastFire = minFireInterval;
        if (gameObject.tag == "Player")
        {
            bulletPrefab = Resources.Load<GameObject>("Prefabs/PlayerBullet");
        }
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
            ShotDamageBase damage = bullet.GetComponent<ShotDamageBase>();
            damage.damage = baseWeaponDamage;
            timeSinceLastFire = 0;
        }
    }
}
