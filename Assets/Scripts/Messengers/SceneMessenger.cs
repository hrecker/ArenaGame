﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneMessenger : MonoBehaviour, IMessenger
{
    public static SceneMessenger Instance { get; private set; }
    private Dictionary<Message, List<Delegate>> callbacks;

    public delegate void HealthChangeCallback(int currentHealth, int change);
    public delegate void EnemyCallback(EnemyType type);
    public delegate void LevelCallback(Level level, bool isLastLevel);
    public delegate void VoidCallback();

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
            switch (msg)
            {
                case Message.PLAYER_HEALTH_LOST:
                    foreach (Delegate callback in callbacks[msg])
                    {
                        callback.DynamicInvoke(args[0], args[1]);
                    }
                    break;
                case Message.PLAYER_HEALTH_GAINED:
                    foreach (Delegate callback in callbacks[msg])
                    {
                        callback.DynamicInvoke(args[0], args[1]);
                    }
                    break;
                case Message.ENEMY_DEFEATED:
                    foreach (Delegate callback in callbacks[msg])
                    {
                        callback.DynamicInvoke(args[0]);
                    }
                    break;
                case Message.LEVEL_COMPLETED:
                    foreach (Delegate callback in callbacks[msg])
                    {
                        callback.DynamicInvoke(args[0], args[1]);
                    }
                    break;
                default:
                    foreach (Delegate callback in callbacks[msg])
                    {
                        callback.DynamicInvoke();
                    }
                    break;
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
