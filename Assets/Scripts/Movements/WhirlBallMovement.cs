using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlBallMovement : MonoBehaviour, IPauseable
{
    public float minRadius = 0.8f;
    public float maxRadius = 3.0f;
    private float currentRadius;

    public float expansionSpeed = 1.0f;
    public float angularSpeed = 0.3f;
    private int currentExpansionDirection;

    private float currentAngle = 0;

    private bool paused = false;


    // Use this for initialization
    void Start () 
	{
        currentRadius = minRadius;
        currentExpansionDirection = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (paused)
        {
            return;
        }

        currentAngle += angularSpeed * Time.deltaTime;
        currentRadius += (currentExpansionDirection * expansionSpeed);
        if (currentRadius >= maxRadius)
        {
            currentRadius = maxRadius;
            currentExpansionDirection = -1;
        }
        else if (currentRadius <= minRadius)
        {
            currentRadius = minRadius;
            currentExpansionDirection = 1;
        }

        if (transform.parent != null)
        {
            UpdatePosition();
        }
    }

    protected void UpdatePosition()
    {
        float dx = currentRadius * Mathf.Cos(currentAngle);
        float dy = currentRadius * Mathf.Sin(currentAngle);
        transform.position = transform.parent.position + new Vector3(dx, dy, 0);
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
