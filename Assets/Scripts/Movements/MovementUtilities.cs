using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUtilities 
{
    public static Vector2 Box2DPreResolveObstacles(Vector2 currentVelocity, Vector3 position, Vector2 size)
    {
        Vector2 frameVelocity = currentVelocity * Time.deltaTime;
        bool resolved = false;
        // Check above and below
        if (frameVelocity.y > 0)
        {
            resolved = ResolveObstacle(position, Vector2.up, size, (size.y / 2) + frameVelocity.y, true, ref currentVelocity);
            if (!resolved) resolved = ResolveObstacle(new Vector2(position.x - (7 * size.x / 16), position.y), Vector2.up, 
                size, (size.y / 2) + frameVelocity.y, true, ref currentVelocity);
            if (!resolved) resolved = ResolveObstacle(new Vector2(position.x + (7 * size.x / 16), position.y), Vector2.up, 
                size, (size.y / 2) + frameVelocity.y, true, ref currentVelocity);
        }
        else if (frameVelocity.y < 0)
        {
            resolved = ResolveObstacle(position, Vector2.down, size, (size.y / 2) - frameVelocity.y, true, ref currentVelocity);
            if (!resolved) resolved = ResolveObstacle(new Vector2(position.x - (7 * size.x / 16), position.y), Vector2.down, 
                size, (size.y / 2) - frameVelocity.y, true, ref currentVelocity);
            if (!resolved) resolved = ResolveObstacle(new Vector2(position.x + (7 * size.x / 16), position.y), Vector2.down, 
                size, (size.y / 2) - frameVelocity.y, true, ref currentVelocity);
        }

        // Check right and left
        if (frameVelocity.x > 0)
        {
            resolved = ResolveObstacle(position, Vector2.right, size, (size.x / 2) + frameVelocity.x, false, ref currentVelocity);
            if (!resolved) resolved = ResolveObstacle(new Vector2(position.x, position.y + (7 * size.y / 16)), Vector2.right, 
                size, (size.x / 2) + frameVelocity.x, false, ref currentVelocity);
            if (!resolved) resolved = ResolveObstacle(new Vector2(position.x, position.y - (7 * size.y / 16)), Vector2.right, 
                size, (size.x / 2) + frameVelocity.x, false, ref currentVelocity);
        }
        else if (frameVelocity.x < 0)
        {
            resolved = ResolveObstacle(position, Vector2.left, size, (size.x / 2) - frameVelocity.x, false, ref currentVelocity);
            if (!resolved) resolved = ResolveObstacle(new Vector2(position.x, position.y + (7 * size.y / 16)), Vector2.left, 
                size, (size.x / 2) - frameVelocity.x, false, ref currentVelocity);
            if (!resolved) resolved = ResolveObstacle(new Vector2(position.x, position.y - (7 * size.y / 16)), Vector2.left, 
                size, (size.x / 2) - frameVelocity.x, false, ref currentVelocity);
        }

        return currentVelocity;
    }

    private static bool ResolveObstacle(Vector2 origin, Vector2 direction, Vector2 size, float distance, bool vertical, ref Vector2 currentVelocity)
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
}
