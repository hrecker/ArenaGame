using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombControl : MonoBehaviour
{
    private BombWeapon bombControl;

    private void Start()
    {
        bombControl = GetComponent<BombWeapon>();
    }

    // Update is called once per frame
    void Update () 
	{
        if (Input.GetButtonDown("Fire2"))
        {
            bombControl.PlaceBomb();
        }
    }
}
