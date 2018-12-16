using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoiderEnemyMovement : MonoBehaviour, IPauseable
{
    public GameObject player;
    public float maxSpeed = 8.0f;
    public float forceScalar = 7500.0f;
    public float safeDistance = 8.0f;
    [Range(0, 1.0f)]
    public float movementRandomization = 0.1f;
    private Rigidbody2D body;

    private bool paused = false;
    private Vector2 pausedVelocity = Vector2.zero;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        body = GetComponent<Rigidbody2D>();
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
            float distanceBetween = Vector2.Distance(player.transform.position, transform.position);
            if (distanceBetween < safeDistance) // If enemy is not a safe distance away, move away from player
            {
                Vector2 direction = transform.position - player.transform.position;
                // Add some randomization to enemy movement
                direction = RandomizeDirection(direction);
                body.AddForce(direction.normalized * forceScalar * Time.deltaTime);
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
