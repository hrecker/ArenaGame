using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUtilities 
{
    // Resolve potential collisions this frame by altering velocity if necessary
    public static Vector2 ResolveObstacles(Collider2D collider, Vector2 currentVelocity, Vector3 position)
    {
        if (collider.GetType() == typeof(BoxCollider2D))
        {
            BoxCollider2D boxCollider = (BoxCollider2D)collider;
            return Box2DPreResolveObstacles(currentVelocity, position, boxCollider.size);
        }
        else if (collider.GetType() == typeof(CircleCollider2D))
        {
            CircleCollider2D circleCollider = (CircleCollider2D)collider;
            return Circle2DPreResolveObstacles(currentVelocity, position, circleCollider.radius);
        }
        // When an unsupported collider is used, just don't even try
        return currentVelocity;
    }

    private static Vector2 Box2DPreResolveObstacles(Vector2 currentVelocity, Vector3 position, Vector2 size)
    {
        Vector2 frameVelocity = currentVelocity * Time.deltaTime;
        bool resolved = false;
        // Check above and below
        if (frameVelocity.y > 0)
        {
            resolved = Box2DResolveObstacle(position, Vector2.up, size, (size.y / 2) + frameVelocity.y, true, ref currentVelocity);
            if (!resolved) resolved = Box2DResolveObstacle(new Vector2(position.x - (7 * size.x / 16), position.y), Vector2.up, 
                size, (size.y / 2) + frameVelocity.y, true, ref currentVelocity);
            if (!resolved) resolved = Box2DResolveObstacle(new Vector2(position.x + (7 * size.x / 16), position.y), Vector2.up, 
                size, (size.y / 2) + frameVelocity.y, true, ref currentVelocity);
        }
        else if (frameVelocity.y < 0)
        {
            resolved = Box2DResolveObstacle(position, Vector2.down, size, (size.y / 2) - frameVelocity.y, true, ref currentVelocity);
            if (!resolved) resolved = Box2DResolveObstacle(new Vector2(position.x - (7 * size.x / 16), position.y), Vector2.down, 
                size, (size.y / 2) - frameVelocity.y, true, ref currentVelocity);
            if (!resolved) resolved = Box2DResolveObstacle(new Vector2(position.x + (7 * size.x / 16), position.y), Vector2.down, 
                size, (size.y / 2) - frameVelocity.y, true, ref currentVelocity);
        }

        // Check right and left
        if (frameVelocity.x > 0)
        {
            resolved = Box2DResolveObstacle(position, Vector2.right, size, (size.x / 2) + frameVelocity.x, false, ref currentVelocity);
            if (!resolved) resolved = Box2DResolveObstacle(new Vector2(position.x, position.y + (7 * size.y / 16)), Vector2.right, 
                size, (size.x / 2) + frameVelocity.x, false, ref currentVelocity);
            if (!resolved) resolved = Box2DResolveObstacle(new Vector2(position.x, position.y - (7 * size.y / 16)), Vector2.right, 
                size, (size.x / 2) + frameVelocity.x, false, ref currentVelocity);
        }
        else if (frameVelocity.x < 0)
        {
            resolved = Box2DResolveObstacle(position, Vector2.left, size, (size.x / 2) - frameVelocity.x, false, ref currentVelocity);
            if (!resolved) resolved = Box2DResolveObstacle(new Vector2(position.x, position.y + (7 * size.y / 16)), Vector2.left, 
                size, (size.x / 2) - frameVelocity.x, false, ref currentVelocity);
            if (!resolved) resolved = Box2DResolveObstacle(new Vector2(position.x, position.y - (7 * size.y / 16)), Vector2.left, 
                size, (size.x / 2) - frameVelocity.x, false, ref currentVelocity);
        }

        return currentVelocity;
    }

    private static bool Box2DResolveObstacle(Vector2 origin, Vector2 direction, Vector2 size, float distance, bool vertical, ref Vector2 currentVelocity)
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

    public static bool Box2DObstaclePresent(Vector2 origin, Vector2 direction, float distance)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, Physics.DefaultRaycastLayers, 5);
        return hit.collider != null;
    }

    private static Vector2 Circle2DPreResolveObstacles(Vector2 currentVelocity, Vector3 position, float radius)
    {
        Vector2 frameVelocity = currentVelocity * Time.deltaTime;
        // Check above and below
        if (frameVelocity.y > 0)
        {
            Circle2DResolveObstacle(position, Vector2.up, radius, radius + frameVelocity.y, true, ref currentVelocity);
        }
        else if (frameVelocity.y < 0)
        {
            Circle2DResolveObstacle(position, Vector2.down, radius, radius - frameVelocity.y, true, ref currentVelocity);
        }

        // Check right and left
        if (frameVelocity.x > 0)
        {
            Circle2DResolveObstacle(position, Vector2.right, radius, radius + frameVelocity.x, false, ref currentVelocity);
        }
        else if (frameVelocity.x < 0)
        {
            Circle2DResolveObstacle(position, Vector2.left, radius, radius - frameVelocity.x, false, ref currentVelocity);
        }

        return currentVelocity;
    }

    private static bool Circle2DResolveObstacle(Vector2 origin, Vector2 direction, float radius, float distance, bool vertical, ref Vector2 currentVelocity)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, Physics.DefaultRaycastLayers, 5);
        if (hit.collider != null)
        {
            float extraDistance = hit.distance - radius;
            if (!vertical)
            {
                extraDistance = hit.distance - radius;
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
}
