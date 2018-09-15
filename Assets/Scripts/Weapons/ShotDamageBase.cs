﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShotDamageBase : MonoBehaviour 
{
    public float damage = 1;
    // If max hits before destroyed is 0, this shot can hit any number of times and will not be destroyed
    public int maxHitsBeforeDestroyed = 1;
    protected int hits;

    public abstract void OnTriggerEnter2D(Collider2D collider);

}
