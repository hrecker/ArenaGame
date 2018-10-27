using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour 
{
    public Text currentLevelText;
    public Text levelCompleteText;
    public float levelStartingDelay = 4.0f;
    private float currentLevelStartingDelay;
    private bool startingLevel;
    private bool allLevelsComplete;
    private int currentLevelNum = 1;
    
	void Start () 
	{
        levelCompleteText.enabled = false;
        SceneMessenger.Instance.AddListener(Message.LEVEL_COMPLETED, new SceneMessenger.LevelCallback(CompleteLevel));
        SceneMessenger.Instance.AddListener(Message.READY_TO_START_LEVEL, new SceneMessenger.VoidCallback(StartNextLevel));
        currentLevelNum = 0;
        StartNextLevel();
	}
	
	void Update () 
	{
        if (startingLevel)
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
        currentLevelNum = level.LevelNumber;
        currentLevelStartingDelay = 0;
        startingLevel = false;
        levelCompleteText.text = "Level " + currentLevelNum + " Complete!";
        levelCompleteText.enabled = true;
        allLevelsComplete = isLastLevel;
    }

    private void StartNextLevel()
    {
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
