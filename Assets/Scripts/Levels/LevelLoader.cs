using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader 
{
	public static List<Level> LoadAllLevels()
    {
        TextAsset[] levelXMLObjects = Resources.LoadAll<TextAsset>("LevelXML");
        List<Level> levels = new List<Level>();
        foreach (TextAsset levelText in levelXMLObjects)
        {
            levels.Add(LevelXMLParser.ParseLevel(levelText.text));
        }
        return levels;
    }
}
