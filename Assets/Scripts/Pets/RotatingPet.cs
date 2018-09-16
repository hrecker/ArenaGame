using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pet that orbits the player
public class RotatingPet : MonoBehaviour
{
    private Transform player;
    public float angularSpeed = 0.01f;
    public float orbitRadius = 1;
    private float currentAngle = 0;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        currentAngle += angularSpeed * Time.deltaTime;
        if (player != null)
        {
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        float dx = orbitRadius * Mathf.Cos(currentAngle);
        float dy = orbitRadius * Mathf.Sin(currentAngle);
        transform.position = player.position + new Vector3(dx, dy, transform.position.z);
    }
}

