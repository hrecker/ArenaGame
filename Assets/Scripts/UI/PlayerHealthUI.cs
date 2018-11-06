using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    private SceneMessenger sceneMessenger;

	// Use this for initialization
	void Start () 
	{
        sceneMessenger = SceneMessenger.Instance;
        sceneMessenger.AddListener(Message.PLAYER_HEALTH_LOST, new SceneMessenger.HealthChangeCallback(UpdateHearts));
        sceneMessenger.AddListener(Message.PLAYER_HEALTH_GAINED, new SceneMessenger.HealthChangeCallback(UpdateHearts));
    }

    void UpdateHearts(int currentHealth, int change)
    {
        int iter = 0;
        foreach (Transform child in transform)
        {
            if (iter < currentHealth)
            {
                child.GetComponent<Image>().enabled = true;
            }
            else
            {
                child.GetComponent<Image>().enabled = false;
            }
            iter++;
        }
    }
}
