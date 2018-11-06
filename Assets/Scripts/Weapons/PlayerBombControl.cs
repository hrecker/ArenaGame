using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombControl : MonoBehaviour, IPauseable
{
    private BombWeapon bombControl;

    private bool paused = false;

    private void Start()
    {
        bombControl = GetComponent<BombWeapon>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (paused)
        {
            return;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            bombControl.PlaceBomb();
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
