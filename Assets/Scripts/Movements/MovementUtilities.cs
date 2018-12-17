using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUtilities 
{
    public static bool Box2DObstaclePresent(Vector2 origin, Vector2 direction, float distance)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, Physics.DefaultRaycastLayers, 5);
        return hit.collider != null;
    }
}
