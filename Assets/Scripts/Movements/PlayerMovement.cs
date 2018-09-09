using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxAcceleration = 500.0f;
    public float maxDrag = 100f;
    public float maxVelocity = 10.0f;
    Vector2 currentVelocity = Vector2.zero;
    private bool movementEnabled;
    private Collider2D movementCollider;

    void Start ()
    {
        //SceneMessenger.Instance.AddListener(Message.STOP_PLAYER_MOVEMENT, new SceneMessenger.VoidCallback(FreezeMovement));
        //SceneMessenger.Instance.AddListener(Message.FREE_PLAYER_MOVEMENT, new SceneMessenger.VoidCallback(EnableMovement));
        movementCollider = GetComponent<Collider2D>();
        //movementEnabled = true;
	}
	
	void Update ()
    {
        /*if (!movementEnabled)
        {
            return;
        }*/

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
        currentVelocity = MovementUtilities.ResolveObstacles(movementCollider, currentVelocity, transform.position);
        // Move
        transform.Translate(currentVelocity * Time.deltaTime);
    }

    public Vector2 GetCurrentVelocity()
    {
        return currentVelocity;
    }

    /*public void FreezeMovement()
    {
        movementEnabled = false;
    }

    public void EnableMovement()
    {
        movementEnabled = true;
    }*/
}
