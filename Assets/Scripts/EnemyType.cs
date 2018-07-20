﻿public enum EnemyType
{
    SQUARE
}

// extension methods
// TODO find a cleaner way to store/modify this data
static class EnemyTypeMethods
{
    public static int GetPointValue(this EnemyType type)
    {
        switch (type)
        {
            case EnemyType.SQUARE:
                return 10;
            default:
                return 0;
        }
    }
}