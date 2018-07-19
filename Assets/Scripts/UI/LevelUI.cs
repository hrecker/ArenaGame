using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour 
{
    public Text currentLevelText;
    public Text levelCompleteText;
    public float betweenLevelDelay = 3.0f;
    public float levelStartingDelay = 4.0f;
    private float currentBetweenLevelDelay;
    private float currentLevelStartingDelay;
    private bool betweenLevel;
    private bool startingLevel;
    private bool allLevelsComplete;
    private int currentLevelNum = 1;
    
	void Start () 
	{
        levelCompleteText.enabled = false;
        SceneMessenger.Instance.AddListener(Message.LEVEL_COMPLETED, new SceneMessenger.LevelCallback(CompleteLevel));
        currentLevelNum = 0;
        StartNextLevel();
	}
	
	void Update () 
	{
		if (betweenLevel)
        {
            currentBetweenLevelDelay += Time.deltaTime;
            if (currentBetweenLevelDelay >= betweenLevelDelay)
            {
                StartNextLevel();
            }
        }
        else if (startingLevel)
        {
            currentLevelStartingDelay += Time.deltaTime;
            if (currentLevelStartingDelay >= levelStartingDelay)
            {
                FinishStartingLevel();
            }
        }
	}

    public void CompleteLevel(Level level, bool isLastLevel)
    {
        currentBetweenLevelDelay = 0;
        currentLevelStartingDelay = 0;
        betweenLevel = true;
        startingLevel = false;
        levelCompleteText.text = "Level " + currentLevelNum + " Complete!";
        levelCompleteText.enabled = true;
        allLevelsComplete = isLastLevel;
    }

    private void StartNextLevel()
    {
        betweenLevel = false;
        if (!allLevelsComplete)
        {
            startingLevel = true;
            currentLevelNum++;
            currentLevelText.text = "Level " + currentLevelNum;
            levelCompleteText.text = "Starting Level " + currentLevelNum + "...";
            levelCompleteText.enabled = true;
        }
    }

    private void FinishStartingLevel()
    {
        startingLevel = false;
        levelCompleteText.enabled = false;
        SceneMessenger.Instance.Invoke(Message.LEVEL_STARTED, null);
    }
}
