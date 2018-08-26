public enum EnemyType
{
    SQUARE,
    KAMIKAZE,
    SHOOTER,
    OCTOSHOT
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
            case EnemyType.KAMIKAZE:
                return 15;
            case EnemyType.SHOOTER:
                return 20;
            case EnemyType.OCTOSHOT:
                return 100;
            default:
                return 0;
        }
    }
}