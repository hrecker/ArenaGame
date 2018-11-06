using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pet that orbits the player
public class RotatingPet : PetBase, IPauseable
{
    public float angularSpeed = 0.01f;
    public float orbitRadius = 1;
    private float currentAngle = 0;

    private bool paused = false;

    void Update()
    {
        if (paused)
        {
            return;
        }

        currentAngle += angularSpeed * Time.deltaTime;
        if (player != null)
        {
            UpdatePosition();
        }
    }

    protected override void UpdatePosition()
    {
        float dx = orbitRadius * Mathf.Cos(currentAngle);
        float dy = orbitRadius * Mathf.Sin(currentAngle);
        transform.position = player.position + new Vector3(dx, dy, transform.position.z);
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

