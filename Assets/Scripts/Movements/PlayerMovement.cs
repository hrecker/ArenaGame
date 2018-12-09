using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPauseable
{
    public float maxAcceleration = 500.0f;
    public float maxDrag = 100f;
    public float maxVelocity = 10.0f;
    Vector2 currentVelocity = Vector2.zero;
    private bool movementEnabled;
    private Collider2D movementCollider;
    private bool paused = false;

    private IMovementAbility movementAbility;

    void Start ()
    {
        //SceneMessenger.Instance.AddListener(Message.STOP_PLAYER_MOVEMENT, new SceneMessenger.VoidCallback(FreezeMovement));
        //SceneMessenger.Instance.AddListener(Message.FREE_PLAYER_MOVEMENT, new SceneMessenger.VoidCallback(EnableMovement));
        movementCollider = GetComponent<Collider2D>();
        movementAbility = new DashPlayerMovementAbility();
        //movementEnabled = true;
	}

    public void SetMovementAbility(IMovementAbility newAbility)
    {
        movementAbility = newAbility;
    }
	
	void Update ()
    {
        if (paused)
        {
            return;
        }
        /*if (!movementEnabled)
        {
            return;
        }*/

        if (movementAbility != null && movementAbility.IsActive())
        {
            currentVelocity = movementAbility.GetPlayerVelocity(currentVelocity);
        }
        else
        {
            currentVelocity = NormalMovement(currentVelocity);
        }

        // Check for walls and obstacles
        currentVelocity = MovementUtilities.ResolveObstacles(movementCollider, currentVelocity, transform.position);
        // Move
        transform.Translate(currentVelocity * Time.deltaTime);
    }

    public Vector2 GetCurrentVelocity()
    {
        return currentVelocity;
    }

    public void OnPause()
    {
        paused = true;
    }

    public void OnResume()
    {
        paused = false;
    }

    /*public void FreezeMovement()
    {
        movementEnabled = false;
    }

    public void EnableMovement()
    {
        movementEnabled = true;
    }*/

    private Vector2 NormalMovement(Vector2 previousVelocity)
    {
        // Get input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Update velocity
        Vector2 acceleration = (new Vector2(horizontal, vertical)) * maxAcceleration;
        previousVelocity += (acceleration * Time.deltaTime);
        float velMagnitude = previousVelocity.magnitude;

        if (horizontal == 0 && vertical == 0)
        {
            Vector2 drag = -previousVelocity * (maxDrag / velMagnitude);
            if ((maxDrag * Time.deltaTime) > velMagnitude)
            {
                previousVelocity = Vector2.zero;
            }
            else
            {
                previousVelocity += (drag * Time.deltaTime);
            }
        }

        if (velMagnitude > maxVelocity)
        {
            previousVelocity = maxVelocity * previousVelocity.normalized;
        }

        return previousVelocity;
    }
}
