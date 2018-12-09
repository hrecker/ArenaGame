using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementAbility
{
    // Whether this ability is currently being used by the player
    bool IsActive();

    // Set the player's velocity for the frame, called every frame
    Vector2 GetPlayerVelocity(Vector2 previousVelocity);

    // Change the normal duration of this movement ability
    void SetMovementDuration(float newDuration);
}
