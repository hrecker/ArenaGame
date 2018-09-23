using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
    public Transform target;
    private float zLevel;
    public float smoothTime = 0.3F;
    public float maxSmoothDistance = 30.0f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        zLevel = transform.position.z;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, zLevel);
            if ((targetPosition - transform.position).magnitude > maxSmoothDistance)
            {
                transform.position = targetPosition;
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            }
        }
    }
}
