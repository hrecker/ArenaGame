//types of messages that can be sent
public enum Message
{
    PLAYER_HEALTH_LOST,
    PLAYER_HEALTH_GAINED, // message should include updated health and amount gained
    HEALTH_LOST, // message should include remaining health and damage done
    NO_HEALTH_REMAINING, // message should include damage done
    ENEMY_DEFEATED,
    //STOP_PLAYER_MOVEMENT,
    //FREE_PLAYER_MOVEMENT,
    READY_TO_START_LEVEL,
    LEVEL_STARTED,
    LEVEL_COMPLETED
}