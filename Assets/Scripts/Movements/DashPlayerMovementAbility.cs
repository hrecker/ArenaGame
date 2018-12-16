using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPlayerMovementAbility : IMovementAbility
{
    private bool active = false;
    private float currentDurationPassed = 0;
    private float duration = 0.2f;

    private Vector2 dashDirection = Vector2.zero;
    private float dashForce = 15000.0f;
    private float maxVelocity = 25.0f;

    public bool IsActive()
    {
        if (!active)
        {
            if (Input.GetAxis("TriggerLeft") > 0 && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
            {
                active = true;
                float horizontal = Input.GetAxis("Horizontal");
                float vertical = Input.GetAxis("Vertical");
                dashDirection = new Vector2(horizontal, vertical);
                return true;
            }
        }
        return active;
    }

    public Vector2 GetPlayerMovementForce()
    {
        currentDurationPassed += Time.deltaTime;
        if (currentDurationPassed >= duration)
        {
            currentDurationPassed = 0;
            active = false;
        }

        return dashForce * dashDirection.normalized;
    }

    public float GetMaxSpeed()
    {
        return maxVelocity;
    }

    public void SetMovementDuration(float newDuration)
    {
        duration = newDuration;
    }
}
