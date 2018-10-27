using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryCloudBoss : MonoBehaviour 
{
    private enum Phase
    {
        REST,
        BULLET,
        BOUNCE
    }
    private Phase currentPhase;

    public float restPhaseLength;
    public float bulletPhaseLength;
    public float bouncePhaseLength;
    private float currentPhaseLength;

    private GameObject bulletPrefab;
    private float[] bulletFireAngles;
    public float bulletFireDelay;
    private float currentBulletFireDelay;
    public float bulletSpeed;

    public float speed;
    private Vector2 currentVelocity;
    private BoxCollider2D boxCollider;

    public Sprite restSprite;
    public Sprite bulletSprite;
    public Sprite bounceSprite;
    private SpriteRenderer spriteRenderer;

    void Awake ()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/EnemyBullet");
        currentPhase = Phase.REST;
        currentPhaseLength = 0;
        bulletFireAngles = new float[8];
        for (int i = 0; i < bulletFireAngles.Length; i++)
        {
            bulletFireAngles[i] = i * 45;
        }
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update () 
	{
        currentPhaseLength += Time.deltaTime;
        switch(currentPhase)
        {
            case Phase.REST:
                if (currentPhaseLength > restPhaseLength)
                {
                    currentPhase = Phase.BULLET;
                    currentPhaseLength = 0;
                    spriteRenderer.sprite = bulletSprite;
                }
                // do nothing during rest phase
                break;
            case Phase.BULLET:
                if (currentPhaseLength > bulletPhaseLength)
                {
                    currentPhase = Phase.BOUNCE;
                    currentPhaseLength = 0;
                    spriteRenderer.sprite = bounceSprite;
                    StartBouncePhase();
                }
                else
                {
                    BulletPhase();
                }
                break;
            case Phase.BOUNCE:
                if (currentPhaseLength > bouncePhaseLength)
                {
                    currentPhase = Phase.REST;
                    currentPhaseLength = 0;
                    spriteRenderer.sprite = restSprite;
                }
                else
                {
                    BouncePhase();
                }
                break;

        }
	}

    private void BulletPhase()
    {
        currentBulletFireDelay += Time.deltaTime;
        if (currentBulletFireDelay > bulletFireDelay)
        {
            for (int i = 0; i < bulletFireAngles.Length; i++)
            {
                Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * bulletFireAngles[i]), Mathf.Sin(Mathf.Deg2Rad * bulletFireAngles[i]));
                Vector3 spawnPosition = transform.position + Vector3.back; // spawn bullets in front so they are visible
                GameObject projectile = Instantiate(bulletPrefab, spawnPosition, transform.rotation);
                StraightConstantMovement bulletMovement = projectile.GetComponent<StraightConstantMovement>();
                bulletMovement.velocity = direction.normalized * bulletSpeed;
                bulletFireAngles[i] += 10;
            }
            currentBulletFireDelay = 0;
        }
    }

    private void StartBouncePhase()
    {
        Vector2 startDirection = Vector2.down + Vector2.right;
        currentVelocity = startDirection.normalized * speed;
    }

    private void BouncePhase()
    {
        Vector2 colliderCenter = new Vector2(transform.position.x, transform.position.y) + boxCollider.offset;
        Vector2 bottomLeftCorner = new Vector2(colliderCenter.x - (boxCollider.size.x / 2), colliderCenter.y - (boxCollider.size.y / 2));
        Vector2 bottomRightCorner = new Vector2(colliderCenter.x + (boxCollider.size.x / 2), colliderCenter.y - (boxCollider.size.y / 2));
        Vector2 topLeftCorner = new Vector2(colliderCenter.x - (boxCollider.size.x / 2), colliderCenter.y + (boxCollider.size.y / 2));
        Vector2 topRightCorner = new Vector2(colliderCenter.x + (boxCollider.size.x / 2), colliderCenter.y + (boxCollider.size.y / 2));
        float verticalDistanceCheck = boxCollider.size.y / 8;
        float horizontalDistanceCheck = boxCollider.size.x / 8;
        // check vertical bounces
        if (currentVelocity.y < 0)
        {
            if (MovementUtilities.Box2DObstaclePresent(bottomLeftCorner, Vector2.down, verticalDistanceCheck) ||
                MovementUtilities.Box2DObstaclePresent(colliderCenter + (Vector2.down * boxCollider.size.y / 2), Vector2.down, verticalDistanceCheck) ||
                MovementUtilities.Box2DObstaclePresent(bottomRightCorner, Vector2.down, verticalDistanceCheck))
            {
                currentVelocity.y = -currentVelocity.y;
            }
        }
        else if (currentVelocity.y > 0)
        {
            if (MovementUtilities.Box2DObstaclePresent(topLeftCorner, Vector2.up, verticalDistanceCheck) ||
                MovementUtilities.Box2DObstaclePresent(colliderCenter + (Vector2.up * boxCollider.size.y / 2), Vector2.up, verticalDistanceCheck) ||
                MovementUtilities.Box2DObstaclePresent(topRightCorner, Vector2.up, verticalDistanceCheck))
            {
                currentVelocity.y = -currentVelocity.y;
            }
        }

        // check horizontal bounces
        if (currentVelocity.x < 0)
        {
            if (MovementUtilities.Box2DObstaclePresent(topLeftCorner, Vector2.left, horizontalDistanceCheck) ||
                MovementUtilities.Box2DObstaclePresent(colliderCenter + (Vector2.left * boxCollider.size.x / 2), Vector2.left, horizontalDistanceCheck) ||
                MovementUtilities.Box2DObstaclePresent(bottomLeftCorner, Vector2.left, horizontalDistanceCheck))
            {
                currentVelocity.x = -currentVelocity.x;
            }
        }
        else if (currentVelocity.x > 0)
        {
            if (MovementUtilities.Box2DObstaclePresent(topRightCorner, Vector2.right, horizontalDistanceCheck) ||
                MovementUtilities.Box2DObstaclePresent(colliderCenter + (Vector2.right * boxCollider.size.x / 2), Vector2.right, horizontalDistanceCheck) ||
                MovementUtilities.Box2DObstaclePresent(bottomRightCorner, Vector2.right, horizontalDistanceCheck))
            {
                currentVelocity.x = -currentVelocity.x;
            }
        }

        this.transform.Translate(currentVelocity * Time.deltaTime);
    }
}
