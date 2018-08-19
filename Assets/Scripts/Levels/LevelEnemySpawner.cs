using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnemySpawner : MonoBehaviour 
{
    // Enemy prefabs to be added in editor
    public GameObject squareEnemy;
    public GameObject kamikazeEnemy;
    public GameObject shooterEnemy;

    private List<Level> levels;
    private int currentLevelIndex;
    private float currentStageTimePassed;
    private int currentStageIndex;
    private int currentStageEnemyNumber;
    private int enemiesDefeatedThisStage;
    private const float enemyZLevel = 1;
    private bool allLevelsComplete;
    private bool levelActive;
    
	void Start () 
	{
        SceneMessenger.Instance.AddListener(Message.ENEMY_DEFEATED, new SceneMessenger.EnemyCallback(EnemyDefeated));
        SceneMessenger.Instance.AddListener(Message.LEVEL_STARTED, new SceneMessenger.VoidCallback(StartNextLevel));
        levels = LevelLoader.LoadAllLevels();
        currentLevelIndex = -1;
        allLevelsComplete = false;
        levelActive = false;
        //StartNextLevel();
	}

    public void StartNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex < levels.Count)
        {
            levelActive = true;
            currentStageIndex = -1;
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
        currentStageIndex++;
        if (currentStageIndex >= levels[currentLevelIndex].Stages.Count)
        {
            SceneMessenger.Instance.Invoke(Message.LEVEL_COMPLETED, new object[] { levels[currentLevelIndex], currentLevelIndex >= levels.Count - 1 });
            levelActive = false;
            return;
        }

        currentStageTimePassed = 0;
        currentStageEnemyNumber = 0;
        enemiesDefeatedThisStage = 0;
    }
	
	void Update () 
	{
        if (levelActive && !allLevelsComplete && currentLevelIndex < levels.Count && currentStageIndex < levels[currentLevelIndex].Stages.Count)
        {
            currentStageTimePassed += Time.deltaTime;
            LevelStage currentStage = levels[currentLevelIndex].Stages[currentStageIndex];
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
        if (enemiesDefeatedThisStage >= levels[currentLevelIndex].Stages[currentStageIndex].Enemies.Count)
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
            case EnemyType.KAMIKAZE:
                return kamikazeEnemy;
            case EnemyType.SHOOTER:
                return shooterEnemy;
            default:
                return squareEnemy;
        }
    }
}
