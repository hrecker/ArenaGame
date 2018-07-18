using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsUI : MonoBehaviour 
{
    private Text pointsText;
    private int currentPoints = 0;
    
	void Start () 
	{
        pointsText = GetComponent<Text>();
        SceneMessenger.Instance.AddListener(Message.ENEMY_DEFEATED, new SceneMessenger.EnemyCallback(AddEnemyPoints));
	}
	
	public void AddEnemyPoints(EnemyType type)
    {
        currentPoints += type.GetPointValue();
        pointsText.text = currentPoints.ToString();
    }
}
