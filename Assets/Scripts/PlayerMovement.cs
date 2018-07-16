using System.Collections;
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
        PreResolveObstacles();
        // Move
        transform.Translate(currentVelocity * Time.deltaTime);
    }

    void PreResolveObstacles()
    {
        Vector2 frameVelocity = currentVelocity * Time.deltaTime;
        bool resolved = false;
        // Check above and below
        if (frameVelocity.y > 0)
        {
            resolved = ResolveObstacle(transform.position, Vector2.up, (size.y / 2) + frameVelocity.y, true);
            if (!resolved) resolved = ResolveObstacle(new Vector2(transform.position.x - (7 * size.x / 16), transform.position.y), Vector2.up, (size.y / 2) + frameVelocity.y, true);
            if (!resolved) resolved = ResolveObstacle(new Vector2(transform.position.x + (7 * size.x / 16), transform.position.y), Vector2.up, (size.y / 2) + frameVelocity.y, true);
        }
        else if (frameVelocity.y < 0)
        {
            resolved = ResolveObstacle(transform.position, Vector2.down, (size.y / 2) - frameVelocity.y, true);
            if (!resolved) resolved = ResolveObstacle(new Vector2(transform.position.x - (7 * size.x / 16), transform.position.y), Vector2.down, (size.y / 2) - frameVelocity.y, true);
            if (!resolved) resolved = ResolveObstacle(new Vector2(transform.position.x + (7 * size.x / 16), transform.position.y), Vector2.down, (size.y / 2) - frameVelocity.y, true);
        }

        // Check right and left
        if (frameVelocity.x > 0)
        {
            resolved = ResolveObstacle(transform.position, Vector2.right, (size.x / 2) + frameVelocity.x, false);
            if (!resolved) resolved = ResolveObstacle(new Vector2(transform.position.x, transform.position.y + (7 * size.y / 16)), Vector2.right, (size.x / 2) + frameVelocity.x, false);
            if (!resolved) resolved = ResolveObstacle(new Vector2(transform.position.x, transform.position.y - (7 * size.y / 16)), Vector2.right, (size.x / 2) + frameVelocity.x, false);
        }
        else if (frameVelocity.x < 0)
        {
            resolved = ResolveObstacle(transform.position, Vector2.left, (size.x / 2) - frameVelocity.x, false);
            if (!resolved) resolved = ResolveObstacle(new Vector2(transform.position.x, transform.position.y + (7 * size.y / 16)), Vector2.left, (size.x / 2) - frameVelocity.x, false);
            if (!resolved) resolved = ResolveObstacle(new Vector2(transform.position.x, transform.position.y - (7 * size.y / 16)), Vector2.left, (size.x / 2) - frameVelocity.x, false);
        }
    }

    bool ResolveObstacle(Vector2 origin, Vector2 direction, float distance, bool vertical)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, Physics.DefaultRaycastLayers, 5);
        if (hit.collider != null)
        {
            float extraDistance = hit.distance - (size.y / 2);
            if (!vertical)
            {
                extraDistance = hit.distance - (size.x / 2);
            }

            if (vertical)
            {
                currentVelocity.y = extraDistance / Time.deltaTime * direction.y;
            }
            else
            {
                currentVelocity.x = extraDistance / Time.deltaTime * direction.x;
            }
            return true;
        }
        return false;
    }

    public Vector2 GetCurrentVelocity()
    {
        return currentVelocity;
    }
}
