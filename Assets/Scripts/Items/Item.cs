using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Item
{
    public string name;
    public Sprite sprite;
    public ItemEffectType type;
    public int itemEffectQuantity;
}

public enum ItemEffectType
{
    BUFFDAMAGE,
    HEAL,
    WEAPON,
    PET,
    SPECIAL
}
