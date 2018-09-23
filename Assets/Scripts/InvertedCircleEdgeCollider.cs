using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertedCircleEdgeCollider : MonoBehaviour 
{
    public int edgeCount;
    public float radius;
    
    void Start()
    {
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[edgeCount];

        for (int i = 0; i < edgeCount; i++)
        {
            float angle = 2 * Mathf.PI * i / edgeCount;
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);

            points[i] = new Vector2(x, y);
        }
        edgeCollider.points = points;
    }
}
