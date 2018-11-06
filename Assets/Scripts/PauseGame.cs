using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour 
{
    private bool paused = false;
	
	void Update () 
	{
		if (Input.GetButtonDown("Start"))
        {
            paused = !paused;
            SendPauseUpdateMessage();
        }
	}

    private void SendPauseUpdateMessage()
    {
        Object[] objects = FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objects)
        {
            foreach (IPauseable pauseable in go.GetComponents<IPauseable>())
            {
                if (paused)
                {
                    pauseable.OnPause();
                }
                else
                {
                    pauseable.OnResume();
                }
            }
        }
    }
}
