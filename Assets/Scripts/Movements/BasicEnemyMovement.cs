using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour 
{
    public GameObject player;
    public float maxSpeed = 7.0f;
    public float accelerationScalar = 3.0f;
    [Range(0, 1.0f)]
    public float movementRandomization = 0.1f;
    private Vector2 currentVelocity = Vector2.zero;
    private Collider2D movementCollider;

    void Start ()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        movementCollider = GetComponent<Collider2D>();
    }
	
	void Update () 
	{
        if (player != null)
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
}
