using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomizer
{
    public static Item[] RandomizeItems(int levelNum)
    {
        Item item1, item2, item3;
        if (levelNum % 2 == 1)
        {
            item1.name = "ChargeGun";
            item1.sprite = LoadItemSprite("ChargeGun");
        }
        else
        {
            item1.name = "SimpleGun";
            item1.sprite = LoadItemSprite("SimpleGun");
        }
        item1.type = ItemEffectType.WEAPON;
        item1.itemEffectQuantity = 0;
        item2.name = "Heart";
        item2.sprite = LoadItemSprite("Heart");
        item2.type = ItemEffectType.HEAL;
        item2.itemEffectQuantity = 1;
        item3.name = "BuffDamage";
        item3.sprite = LoadItemSprite("BuffDamage");
        item3.type = ItemEffectType.BUFFDAMAGE;
        item3.itemEffectQuantity = 1;
        return new Item[] { item1, item2, item3 };
    }

    private static Sprite LoadItemSprite(string itemName)
    {
        return Resources.Load<Sprite>("ItemSprites/" + itemName);
    }
}
