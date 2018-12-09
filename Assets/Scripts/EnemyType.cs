public enum EnemyType
{
    SQUARE,
    KAMIKAZE,
    SHOOTER,
    OCTOSHOT,
    DASH,
    WHIRLER,
    MOTHER,
    SPAWN,
    CLOUDBOSS
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
                return 40;
            case EnemyType.OCTOSHOT:
                return 100;
            case EnemyType.DASH:
                return 25;
            case EnemyType.WHIRLER:
                return 60;
            case EnemyType.MOTHER:
                return 200;
            case EnemyType.SPAWN:
                return 5;
            default:
                return 0;
        }
    }
}