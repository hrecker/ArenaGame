using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour 
{
    public GameObject player;
    public float maxSpeed = 7.0f;
    public float accelerationScalar = 3.0f;
    private Vector2 currentVelocity = Vector2.zero;

    void Start ()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
	
	void Update () 
	{
        if (player != null)
        {
            Vector2 direction = player.transform.position - transform.position;
            Vector2 acceleration = direction.normalized * accelerationScalar;
            currentVelocity += (acceleration * Time.deltaTime);

            float velMagnitude = currentVelocity.magnitude;
            if (velMagnitude > maxSpeed)
            {
                currentVelocity = currentVelocity.normalized * maxSpeed;
            }

            transform.Translate(currentVelocity * Time.deltaTime);
        }
        else
        {
            this.enabled = false;
        }
    }
}
