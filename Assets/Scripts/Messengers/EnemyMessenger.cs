using UnityEngine;

public class EnemyMessenger : MonoBehaviour, IMessenger
{
    void Start()
    {
    }

    public void Invoke(Message msg, object[] args)
    {
        switch (msg)
        {
            case Message.NO_HEALTH_REMAINING:
                Destroy(gameObject);
                break;
        }
    }
}