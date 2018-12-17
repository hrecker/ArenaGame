using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour, IPauseable 
{
    public GameObject player;
    public float maxSpeed = 7.0f;
    public float forceScalar = 7500.0f;
    [Range(0, 1.0f)]
    public float movementRandomization = 0.1f;
    private Vector2 pausedVelocity = Vector2.zero;
    private Rigidbody2D body;
    private bool paused = false;

    void Start ()
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

    void Update ()
    {
        if (paused)
        {
            return;
        }

        if (player != null)
        {
            Vector2 direction = player.transform.position - transform.position;
            // Add some randomization to enemy movement
            Vector2 randomDirection = RandomizeDirection(direction);
            body.AddForce(randomDirection.normalized * forceScalar * Time.deltaTime);
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
