using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnemySpawner : MonoBehaviour 
{
    // Enemy prefabs to be added in editor
    public GameObject squareEnemy;

    private List<Level> levels;
    private int currentLevelNumber;
    private float currentStageTimePassed;
    private int currentStageNumber;
    private int currentStageEnemyNumber;
    private int enemiesDefeatedThisStage;
    private const float enemyZLevel = 1;
    private bool allLevelsComplete;
    
	void Start () 
	{
        SceneMessenger.Instance.AddListener(Message.ENEMY_DEFEATED, new SceneMessenger.EnemyCallback(EnemyDefeated));
        levels = LevelLoader.LoadAllLevels();
        currentLevelNumber = -1;
        allLevelsComplete = false;
        StartNextLevel();
	}

    private void StartNextLevel()
    {
        currentLevelNumber++;
        if (currentLevelNumber < levels.Count)
        {
            currentStageNumber = -1;
            StartNextStage();
        }
        else
        {
            // TODO logic for completing all levels
            allLevelsComplete = true;
        }
    }

    private void StartNextStage()
    {
        currentStageNumber++;
        if (currentStageNumber >= levels[currentLevelNumber].Stages.Count)
        {
            StartNextLevel();
            return;
        }

        currentStageTimePassed = 0;
        currentStageEnemyNumber = 0;
        enemiesDefeatedThisStage = 0;
    }
	
	void Update () 
	{
        if (!allLevelsComplete && currentLevelNumber < levels.Count && currentStageNumber < levels[currentLevelNumber].Stages.Count)
        {
            currentStageTimePassed += Time.deltaTime;
            LevelStage currentStage = levels[currentLevelNumber].Stages[currentStageNumber];
            for (int i = currentStageEnemyNumber; i < currentStage.Enemies.Count; i++)
            {
                if (currentStage.SpawnTimings[i] <= currentStageTimePassed)
                {
                    SpawnEnemy(currentStage.Enemies[i], currentStage.SpawnLocations[i]);
                    currentStageEnemyNumber++;
                }
                else
                {
                    break;
                }
            }
        }
	}

    public void EnemyDefeated(EnemyType type)
    {
        enemiesDefeatedThisStage++;
        if (enemiesDefeatedThisStage >= levels[currentLevelNumber].Stages[currentStageNumber].Enemies.Count)
        {
            StartNextStage();
        }
    }

    private void SpawnEnemy(EnemyType type, Vector2 spawnLocation)
    {
        Instantiate(GetEnemyPrefab(type), new Vector3(spawnLocation.x, spawnLocation.y, enemyZLevel), Quaternion.identity);
    }

    private GameObject GetEnemyPrefab(EnemyType type)
    {
        switch(type)
        {
            case EnemyType.SQUARE:
                return squareEnemy;
            default:
                return squareEnemy;
        }
    }
}
