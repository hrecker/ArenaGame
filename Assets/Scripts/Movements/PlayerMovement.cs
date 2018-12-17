using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPauseable
{
    public float movementForce = 7500.0f;
    public float maxSpeed = 15.0f;
    private bool movementEnabled;
    private Rigidbody2D body;

    private IMovementAbility movementAbility;

    private bool paused = false;
    private Vector2 pausedVelocity;

    void Start ()
    {
        //SceneMessenger.Instance.AddListener(Message.STOP_PLAYER_MOVEMENT, new SceneMessenger.VoidCallback(FreezeMovement));
        //SceneMessenger.Instance.AddListener(Message.FREE_PLAYER_MOVEMENT, new SceneMessenger.VoidCallback(EnableMovement));
        movementAbility = new DashPlayerMovementAbility();
        body = GetComponent<Rigidbody2D>();
        //movementEnabled = true;
	}

    public void SetMovementAbility(IMovementAbility newAbility)
    {
        movementAbility = newAbility;
    }

    void FixedUpdate()
    {
        float max = maxSpeed;
        if (movementAbility != null && movementAbility.IsActive())
        {
            max = movementAbility.GetMaxSpeed();
        }
        body.velocity = Vector2.ClampMagnitude(body.velocity, max);
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

        Vector2 currentForce = Vector2.zero;
        if (movementAbility != null && movementAbility.IsActive())
        {
            currentForce = movementAbility.GetPlayerMovementForce();
        }
        else
        {
            currentForce = NormalMovement();
        }

        body.AddForce(currentForce * Time.deltaTime);
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

    /*public void FreezeMovement()
    {
        movementEnabled = false;
    }

    public void EnableMovement()
    {
        movementEnabled = true;
    }*/

    private Vector2 NormalMovement()
    {
        // Get input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate acceleration
        return (new Vector2(horizontal, vertical)) * movementForce;
    }
}
