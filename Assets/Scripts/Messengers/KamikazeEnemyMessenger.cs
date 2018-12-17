using UnityEngine;

public class KamikazeEnemyMessenger : MonoBehaviour, IMessenger
{
    public EnemyType type = EnemyType.KAMIKAZE;
    public GameObject explosion;

    public void Invoke(Message msg, object[] args)
    {
        switch (msg)
        {
            case Message.NO_HEALTH_REMAINING:
                Explode();
                break;
        }
    }

    // Explode when touched by player
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Explode();
        }
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        SceneMessenger.Instance.Invoke(Message.ENEMY_DEFEATED, new object[] { type });
        Destroy(gameObject);
    }
}