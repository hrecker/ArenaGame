using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightConstantMovement : MonoBehaviour, IPauseable
{
    public Vector2 velocity = Vector2.zero;

    private bool paused = false;

    // Update is called once per frame
    void Update ()
    {
        if (paused)
        {
            return;
        }

        transform.Translate(velocity * Time.deltaTime);
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
