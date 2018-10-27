using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelLoader 
{
    private static Dictionary<int, Level> levelsByLevelNum;

	public static List<Level> LoadAllLevels()
    {
        if (levelsByLevelNum == null)
        {
            levelsByLevelNum = new Dictionary<int, Level>();
            TextAsset[] levelXMLObjects = Resources.LoadAll<TextAsset>("LevelXML");
            foreach (TextAsset levelText in levelXMLObjects)
            {
                Level level = LevelXMLParser.ParseLevel(levelText.text);
                levelsByLevelNum.Add(level.LevelNumber, level);
            }
        }
        List<Level> result = new List<Level>();
        List<int> sortedLevelNums = levelsByLevelNum.Keys.ToList();
        sortedLevelNums.Sort();
        foreach (int levelNum in sortedLevelNums)
        {
            result.Add(levelsByLevelNum[levelNum]);
        }
        return result;
    }

    public static Level GetLevel(int levelNum)
    {
        return levelsByLevelNum[levelNum];
    }
}
