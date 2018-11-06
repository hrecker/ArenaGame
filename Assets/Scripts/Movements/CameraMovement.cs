using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour, IPauseable 
{
    public Transform target;
    private float zLevel;
    public float smoothTime = 0.3F;
    public float maxSmoothDistance = 30.0f;
    private Vector3 velocity = Vector3.zero;

    private bool paused = false;

    void Start()
    {
        zLevel = transform.position.z;
    }

    void Update()
    {
        if (paused)
        {
            return;
        }

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

    public void OnPause()
    {
        paused = true;
    }

    public void OnResume()
    {
        paused = false;
    }
}
