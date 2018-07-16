//types of messages that can be sent
public enum Message
{
    PLAYER_HEALTH_LOST,
    HEALTH_LOST, // message should include remaining health and damage done
    HEALTH_GAINED, 
    NO_HEALTH_REMAINING // message should include damage done
}