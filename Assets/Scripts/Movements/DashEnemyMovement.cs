using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemyMovement : MonoBehaviour
{
    public GameObject player;
    public float maxSpeed = 21.0f;
    public float dashDuration = 1.0f;
    public float dashDelay = 2.0f;
    private Vector2 currentVelocity = Vector2.zero;
    private Vector2 dashDirection;
    private Collider2D movementCollider;
    private float currentDashDuration = 0;
    private float currentDashDelay = 0;
    private bool currentlyDashing = false;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        movementCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (!currentlyDashing)
        {
            currentDashDelay += Time.deltaTime;
            if (currentDashDelay >= dashDelay)
            {
                // Start dash 
                currentDashDuration = 0;
                currentlyDashing = true;
                dashDirection = player.transform.position - transform.position;
            }
        }
        else
        {
            currentDashDuration += Time.deltaTime;

            currentVelocity = dashDirection.normalized * maxSpeed;

            currentVelocity = MovementUtilities.ResolveObstacles(movementCollider, currentVelocity, transform.position);
            transform.Translate(currentVelocity * Time.deltaTime);

            if (currentDashDuration >= dashDuration)
            {
                // End dash
                currentDashDelay = 0;
                currentlyDashing = false;
            }
        }
    }
}
