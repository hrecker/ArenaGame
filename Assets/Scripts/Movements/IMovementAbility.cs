using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementAbility
{
    // Whether this ability is currently being used by the player
    bool IsActive();

    // Set the player's movement force for the frame, called every frame
    Vector2 GetPlayerMovementForce();

    // Set the player's max speed for the frame, called every frame
    float GetMaxSpeed();

    // Change the normal duration of this movement ability
    void SetMovementDuration(float newDuration);
}
