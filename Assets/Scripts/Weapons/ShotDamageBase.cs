using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShotDamageBase : MonoBehaviour 
{
    public float damage = 1;
    public int maxHitsBeforeDestroyed = 1;
    protected int hits;

    public abstract void OnTriggerEnter2D(Collider2D collider);

}
