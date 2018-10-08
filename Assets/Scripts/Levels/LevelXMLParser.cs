using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class LevelXMLParser
{
    public static Level ParseLevel(string xml)
    {
        return ParseLevel(XElement.Parse(xml));
    }

    public static Level ParseLevelFromFile(string xmlfilepath)
    {
        return ParseLevel(XElement.Load(xmlfilepath));
    }

    //TODO better error output when xml parsing fails
    private static Level ParseLevel(XElement root)
    {
        Level level = new Level();
        level.ArenaName = root.Element("Arena").Value;
        level.CompletionPoints = int.Parse(root.Element("CompletionPoints").Value);
        level.LevelNumber = int.Parse(root.Element("LevelNumber").Value);

        level.ItemSelection = new string[3];
        int i = 0;
        foreach(XElement itemElement in root.Element("VictoryItems").Elements("Item"))
        {
            level.ItemSelection[i] = itemElement.Value;
            i++;
        }

        level.Stages = new List<LevelStage>();

        IEnumerable<XElement> stageElements = root.Elements("Stage");
        if (stageElements != null && stageElements.Any())
        {
            foreach (XElement stage in stageElements)
            {
                LevelStage newStage = ParseLevelStage(stage);
                level.Stages.Add(newStage);
            }
        }

        return level;
    }

    private static LevelStage ParseLevelStage(XElement stageRoot)
    {
        LevelStage stage = new LevelStage();
        stage.Enemies = new List<EnemyType>();
        stage.SpawnLocations = new List<Vector2>();
        stage.SpawnTimings = new List<float>();

        IEnumerable<XElement> spawnElements = stageRoot.Elements("EnemySpawn");
        if (spawnElements != null && spawnElements.Any())
        {
            foreach (XElement spawn in spawnElements)
            {
                EnemyType type = (EnemyType)Enum.Parse(typeof(EnemyType), spawn.Element("EnemyType").Value);
                stage.Enemies.Add(type);
                float spawnX = float.Parse(spawn.Element("SpawnLocation").Element("X").Value);
                float spawnY = float.Parse(spawn.Element("SpawnLocation").Element("Y").Value);
                stage.SpawnLocations.Add(new Vector2(spawnX, spawnY));
                float spawnTime = float.Parse(spawn.Element("SpawnTiming").Value);
                stage.SpawnTimings.Add(spawnTime);
            }
        }

        return stage;
    }
}
