using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnemySpawner : MonoBehaviour, IPauseable
{
    // Enemy prefabs to be added in editor
    public GameObject squareEnemy;
    public GameObject kamikazeEnemy;
    public GameObject shooterEnemy;
    public GameObject octoshotEnemy;
    public GameObject dashEnemy;
    public GameObject cloudbossEnemy;
    public GameObject whirlerEnemy;
    public GameObject motherEnemy;

    public ArenaLocator arenaLocator;
    private Vector2 baseArenaSpawnLocation;

    public GameObject player;

    public int startLevelIndexDebug = 0;

    private List<Level> levels;
    private int currentLevelIndex;
    private float currentStageTimePassed;
    private int currentStageIndex;
    private int currentStageEnemyNumber;
    private int enemiesDefeatedThisStage;
    private const float enemyZLevel = 1;
    private bool allLevelsComplete;
    private bool levelActive;
    private string currentArena = "";

    private bool paused = false;

    void Start ()
    {
        SceneMessenger.Instance.AddListener(Message.ENEMY_DEFEATED, new SceneMessenger.EnemyCallback(EnemyDefeated));
        SceneMessenger.Instance.AddListener(Message.LEVEL_STARTED, new SceneMessenger.VoidCallback(StartNextLevel));
        SceneMessenger.Instance.AddListener(Message.READY_TO_START_LEVEL, new SceneMessenger.VoidCallback(MoveToArena));
        levels = LevelLoader.LoadAllLevels();
        currentLevelIndex = startLevelIndexDebug - 1;
        allLevelsComplete = false;
        levelActive = false;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        currentArena = levels[0].ArenaName;

        if (startLevelIndexDebug != 0)
        {
            MoveToArena();
        }
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

    public void MoveToArena()
    {
        string nextArena = levels[currentLevelIndex + 1].ArenaName;
        if (nextArena != currentArena)
        {
            baseArenaSpawnLocation = arenaLocator.GetArenaSpawnLocation(nextArena);
            player.transform.position = new Vector3(baseArenaSpawnLocation.x, baseArenaSpawnLocation.y,
                player.transform.position.z);
            currentArena = nextArena;
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
        if (paused)
        {
            return;
        }

        if (levelActive && !allLevelsComplete && currentLevelIndex < levels.Count && currentStageIndex < levels[currentLevelIndex].Stages.Count)
        {
            currentStageTimePassed += Time.deltaTime;
            LevelStage currentStage = levels[currentLevelIndex].Stages[currentStageIndex];
            for (int i = currentStageEnemyNumber; i < currentStage.Enemies.Count; i++)
            {
                if (currentStage.SpawnTimings[i] <= currentStageTimePassed)
                {
                    SpawnEnemy(currentStage.Enemies[i], currentStage.SpawnLocations[i] + baseArenaSpawnLocation);
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
        if (type != EnemyType.SPAWN) // enemies spawned from other enemies don't count
        {
            enemiesDefeatedThisStage++;
            if (enemiesDefeatedThisStage >= levels[currentLevelIndex].Stages[currentStageIndex].Enemies.Count)
            {
                StartNextStage();
            }
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
            case EnemyType.OCTOSHOT:
                return octoshotEnemy;
            case EnemyType.DASH:
                return dashEnemy;
            case EnemyType.CLOUDBOSS:
                return cloudbossEnemy;
            case EnemyType.WHIRLER:
                return whirlerEnemy;
            case EnemyType.MOTHER:
                return motherEnemy;
            default:
                return squareEnemy;
        }
    }

    public void OnPause() //TODO make this work right
    {
        paused = true;
    }

    public void OnResume()
    {
        paused = false;
    }
}
