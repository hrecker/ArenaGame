using UnityEngine;

public class PlayerMessenger : MonoBehaviour, IMessenger
{
    SceneMessenger sceneMessenger;

    void Start()
    {
        sceneMessenger = SceneMessenger.Instance;
    }

    public void Invoke(Message msg, object[] args)
    {
        switch (msg)
        {
            case Message.HEALTH_LOST:
                sceneMessenger.Invoke(Message.PLAYER_HEALTH_LOST, args);
                break;
            case Message.NO_HEALTH_REMAINING:
                Destroy(gameObject);
                break;
        }
    }
}