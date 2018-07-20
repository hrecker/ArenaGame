﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxAcceleration = 500.0f;
    public float maxDrag = 100f;
    public float maxVelocity = 10.0f;
    Vector2 currentVelocity = Vector2.zero;
    Vector2 size = Vector2.zero;

	void Start ()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        size = collider.size;
	}
	
	void Update ()
    {
        // Get input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        // Update velocity
        Vector2 acceleration = (new Vector2(horizontal, vertical)) * maxAcceleration;
        currentVelocity += (acceleration * Time.deltaTime);
        float velMagnitude = currentVelocity.magnitude;

        if (horizontal == 0 && vertical == 0)
        {
            Vector2 drag = -currentVelocity * (maxDrag / velMagnitude);
            if ((maxDrag * Time.deltaTime) > velMagnitude)
            {
                currentVelocity = Vector2.zero;
            }
            else
            {
                currentVelocity += (drag * Time.deltaTime);
            }
        }

        if (velMagnitude > maxVelocity)
        {
            currentVelocity = maxVelocity * currentVelocity.normalized;
        }

        // Check for walls and obstacles
        currentVelocity = MovementUtilities.Box2DPreResolveObstacles(currentVelocity, transform.position, size);
        // Move
        transform.Translate(currentVelocity * Time.deltaTime);
    }

    public Vector2 GetCurrentVelocity()
    {
        return currentVelocity;
    }
}