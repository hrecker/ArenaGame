using UnityEngine;

public class EnemyMessenger : MonoBehaviour, IMessenger
{
    public EnemyType type;
    private bool destroyed;

    public void Invoke(Message msg, object[] args)
    {
        if (!destroyed)
        {
            switch (msg)
            {
                case Message.NO_HEALTH_REMAINING:
                    SceneMessenger.Instance.Invoke(Message.ENEMY_DEFEATED, new object[] { type });
                    destroyed = true;
                    Destroy(gameObject);
                    break;
            }
        }
    }
}