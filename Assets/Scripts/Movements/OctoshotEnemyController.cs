using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls both movement and weapons for the octoshot enemy
public class OctoshotEnemyController : MonoBehaviour, IPauseable 
{
    public GameObject player;
    public float maxSpeed = 3.0f;
    public float forceScalar = 7500.0f;
    [Range(0, 1.0f)]
    public float movementRandomization = 0.25f;

    public float movementTime = 2.5f;
    public float stopTime = 2.0f;
    private bool moving;
    private float currentStageTime;
    private WeaponMods mods;
    private WeaponBase weapon;
    private Rigidbody2D body;

    private bool paused = false;
    private Vector2 pausedVelocity = Vector2.zero;

    void Start()
    {
        mods = GetComponent<WeaponMods>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        weapon = new OctoShot(mods, false);
        body = GetComponent<Rigidbody2D>();
        moving = true;
    }

    void FixedUpdate()
    {
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxSpeed);
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
                body.AddForce(direction.normalized * forceScalar * Time.deltaTime);

                if (currentStageTime >= movementTime)
                {
                    currentStageTime = 0;
                    moving = false;
                    body.velocity = Vector2.zero;
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
        pausedVelocity = body.velocity;
        body.velocity = Vector2.zero;
        body.isKinematic = true;
    }

    public void OnResume()
    {
        paused = false;
        body.velocity = pausedVelocity;
        body.isKinematic = false;
    }
}
