using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoiderEnemyMovement : MonoBehaviour, IPauseable
{
    public GameObject player;
    public float maxSpeed = 5.0f;
    public float accelerationScalar = 3.0f;
    public float safeDistance = 8.0f;
    [Range(0, 1.0f)]
    public float movementRandomization = 0.1f;
    private Vector2 currentVelocity = Vector2.zero;
    private Collider2D movementCollider;

    private bool paused = false;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        movementCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (paused)
        {
            return;
        }

        if (player != null)
        {
            float distanceBetween = Vector2.Distance(player.transform.position, transform.position);
            if (distanceBetween < safeDistance) // If enemy is not a safe distance away, move away from player
            {
                Vector2 direction = transform.position - player.transform.position;
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
