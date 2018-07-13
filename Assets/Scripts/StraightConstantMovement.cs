using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightConstantMovement : MonoBehaviour 
{
    public Vector2 velocity = Vector2.zero;
	
	// Update is called once per frame
	void Update () 
	{
        transform.Translate(velocity * Time.deltaTime);
	}
}
