using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneMessenger : MonoBehaviour, IMessenger
{
    public static SceneMessenger Instance { get; private set; }
    private Dictionary<Message, List<Delegate>> callbacks;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        if (callbacks == null)
        {
            callbacks = new Dictionary<Message, List<Delegate>>();
        }

    }

    public void Invoke(Message msg, object[] args)
    {
        if (callbacks.ContainsKey(msg))
        {
            foreach (Delegate callback in callbacks[msg])
            {
                callback.DynamicInvoke();
            }
        }
    }

    public void AddListener(Message msg, Delegate callback)
    {
        if (callbacks == null)
        {
            callbacks = new Dictionary<Message, List<Delegate>>();
        }
        if (!callbacks.ContainsKey(msg))
        {
            callbacks.Add(msg, new List<Delegate>());
        }
        callbacks[msg].Add(callback);
    }
}
