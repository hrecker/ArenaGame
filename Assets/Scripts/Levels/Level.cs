using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level 
{
    public string ArenaName { get; set; }
    public List<LevelStage> Stages { get; set; }
    public int CompletionPoints { get; set; }
    public int LevelNumber { get; set; }
    public string[] ItemSelection { get; set; }
}

public class LevelStage
{
    public List<EnemyType> Enemies { get; set; }
    public List<Vector2> SpawnLocations { get; set; }
    public List<float> SpawnTimings { get; set; }
}
