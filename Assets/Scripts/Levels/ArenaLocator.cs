using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaLocator : MonoBehaviour 
{
    [Serializable]
    public struct ArenaSpawnLocation
    {
        public String arenaName;
        public Vector2 spawnLocation;
    }
    public ArenaSpawnLocation[] arenas;
    private Dictionary<String, Vector2> arenaSpawns;

    void Awake () 
	{
        arenaSpawns = new Dictionary<string, Vector2>();
        foreach (ArenaSpawnLocation arenaSpawnLocation in arenas)
        {
            arenaSpawns.Add(arenaSpawnLocation.arenaName, arenaSpawnLocation.spawnLocation);
        }
    }
	
	public Vector2 GetArenaSpawnLocation(String arenaName)
    {
        if (arenaSpawns.ContainsKey(arenaName))
        {
            return arenaSpawns[arenaName];
        }
        else
        {
            return Vector2.zero;
        }
    }
}
