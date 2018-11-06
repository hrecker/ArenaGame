using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls both movement and weapons for the octoshot enemy
public class OctoshotEnemyController : MonoBehaviour, IPauseable 
{
    public GameObject player;
    public float maxSpeed = 3.0f;
    public float accelerationScalar = 3.0f;
    [Range(0, 1.0f)]
    public float movementRandomization = 0.25f;
    private Vector2 currentVelocity = Vector2.zero;

    public float movementTime = 2.5f;
    public float stopTime = 2.0f;
    private bool moving;
    private float currentStageTime;
    private WeaponMods mods;
    private WeaponBase weapon;
    private Collider2D movementCollider;

    private bool paused = false;

    void Start()
    {
        mods = GetComponent<WeaponMods>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        weapon = new OctoShot(mods, false);
        movementCollider = GetComponent<CircleCollider2D>();
        moving = true;
    }

    void Update()
    {
        if (paused)
        {
            return;
        }

        if (player != null)
        {
            currentStageTime += Time.deltaTime;
            if (moving)
            {
                Vector2 direction = player.transform.position - transform.position;
                // Add some randomization to enemy movement
                direction = RandomizeDirection(direction);
                Vector2 acceleration = direction.normalized * accelerationScalar;
                currentVelocity += (acceleration * Time.deltaTime);

                float velMagnitude = currentVelocity.magnitude;
                if (velMagnitude > maxSpeed)
                {
                    currentVelocity = currentVelocity.normalized * maxSpeed;
                }

                currentVelocity = MovementUtilities.ResolveObstacles(movementCollider, currentVelocity, transform.position);
                transform.Translate(currentVelocity * Time.deltaTime);

                if (currentStageTime >= movementTime)
                {
                    currentStageTime = 0;
                    moving = false;
                }
            }
            else
            {
                if (currentStageTime >= stopTime)
                {
                    currentStageTime = 0;
                    moving = true;
                    // fire interval and direction don't matter
                    //TODO this could be bad...
                    weapon.Fire(weapon.minFireInterval + 1, Vector2.zero, transform);
                }
            }
        }
        else
        {
            this.enabled = false;
        }
    }

    private Vector2 RandomizeDirection(Vector2 startingDirection)
    {
        float angleRandomization = ((360 * Random.value) - 180) * movementRandomization;
        return startingDirection.Rotate(angleRandomization);
    }

    public void OnPause()
    {
        paused = true;
    }

    public void OnResume()
    {
        paused = false;
    }
}
