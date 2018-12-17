using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemyMovement : MonoBehaviour, IPauseable
{
    public GameObject player;
    public float maxSpeed = 21.0f;
    public float forceScalar = 30000.0f;
    public float dashDuration = 1.0f;
    public float dashDelay = 2.0f;
    private Vector2 dashDirection;
    private Rigidbody2D body;
    private float currentDashDuration = 0;
    private float currentDashDelay = 0;
    private bool currentlyDashing = false;

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

        if (!currentlyDashing)
        {
            currentDashDelay += Time.deltaTime;
            if (currentDashDelay >= dashDelay)
            {
                // Start dash 
                currentDashDuration = 0;
                currentlyDashing = true;
                if (player == null)
                {
                    this.enabled = false;
                }
                else
                {
                    dashDirection = player.transform.position - transform.position;
                }
            }
        }
        else
        {
            currentDashDuration += Time.deltaTime;
            body.AddForce(dashDirection.normalized * forceScalar * Time.deltaTime);

            if (currentDashDuration >= dashDuration)
            {
                // End dash
                currentDashDelay = 0;
                currentlyDashing = false;
            }
        }
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
