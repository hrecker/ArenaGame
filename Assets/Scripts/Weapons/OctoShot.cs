using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoShot : WeaponBase
{
    public GameObject bulletPrefab;
    public float minFireInterval = 0.25f;
    public float bulletSpeed = 10;
    private float timeSinceLastFire;

    void Start()
    {
        timeSinceLastFire = minFireInterval;
        if (gameObject.tag == "Player")
        {
            bulletPrefab = Resources.Load<GameObject>("Prefabs/PlayerBullet");
        }
    }

    void Update()
    {
        timeSinceLastFire += Time.deltaTime;
    }

    // Fire in 8 directions
    public override void Fire(Vector2 direction)
    {
        if (timeSinceLastFire >= minFireInterval)
        {
            FireSingleBullet(Vector2.up);
            FireSingleBullet(Vector2.up + Vector2.right);
            FireSingleBullet(Vector2.right);
            FireSingleBullet(Vector2.down + Vector2.right);
            FireSingleBullet(Vector2.down);
            FireSingleBullet(Vector2.down + Vector2.left);
            FireSingleBullet(Vector2.left);
            FireSingleBullet(Vector2.up + Vector2.left);
            timeSinceLastFire = 0;
        }
    }

    private void FireSingleBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        StraightConstantMovement bulletMovement = bullet.GetComponent<StraightConstantMovement>();
        bulletMovement.velocity = direction.normalized * bulletSpeed;
        ShotDamageBase damage = bullet.GetComponent<ShotDamageBase>();
        damage.damage = baseWeaponDamage;
    }
}
