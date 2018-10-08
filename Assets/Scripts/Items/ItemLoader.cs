using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoader
{
    public static Item[] GetVictoryItems(int levelNum)
    {
        string[] itemNames = LevelLoader.GetLevel(levelNum).ItemSelection;
        Item[] items = new Item[3];
        int i = 0;
        foreach (string itemName in itemNames)
        {
            items[i] = GetItemFromString(itemName);
            i++;
        }
        return items;
    }

    private static Item GetItemFromString(string itemName)
    {
        Item result = new Item();
        result.name = itemName;
        result.sprite = LoadItemSprite(itemName);
        switch(itemName)
        {
            case "ChargeGun":
            case "SimpleGun":
            case "GrenadeLauncher":
                result.type = ItemEffectType.WEAPON;
                result.itemEffectQuantity = 0;
                break;
            case "BuffDamage":
                result.type = ItemEffectType.BUFFDAMAGE;
                result.itemEffectQuantity = 1;
                break;
            case "Heart":
                result.type = ItemEffectType.HEAL;
                result.itemEffectQuantity = 1;
                break;
        }
        return result;
    }

    private static Sprite LoadItemSprite(string itemName)
    {
        return Resources.Load<Sprite>("ItemSprites/" + itemName);
    }
}
