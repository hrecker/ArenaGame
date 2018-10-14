using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PetBase : MonoBehaviour
{
    protected Transform player;

    void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        UpdatePosition();
    }

    abstract protected void UpdatePosition();
}
