using UnityEngine;

public class EnemyMessenger : MonoBehaviour, IMessenger
{
    public EnemyType type;

    public void Invoke(Message msg, object[] args)
    {
        switch (msg)
        {
            case Message.NO_HEALTH_REMAINING:
                SceneMessenger.Instance.Invoke(Message.ENEMY_DEFEATED, new object[] { type });
                Destroy(gameObject);
                break;
        }
    }
}