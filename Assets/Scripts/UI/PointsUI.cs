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
        SceneMessenger.Instance.AddListener(Message.LEVEL_COMPLETED, new SceneMessenger.LevelCallback(AddLevelCompletionPoints));
	}
	
	public void AddEnemyPoints(EnemyType type)
    {
        AddPoints(type.GetPointValue());
    }

    public void AddLevelCompletionPoints(Level level, bool isLastLevel)
    {
        AddPoints(level.CompletionPoints);
    }

    private void AddPoints(int toAdd)
    {
        currentPoints += toAdd;
        pointsText.text = currentPoints.ToString();
    }
}
