using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour 
{
    [Serializable]
    public struct MessageAudioClip
    {
        public Message message;
        public AudioClip clip;
    }
    public MessageAudioClip[] clips;
    private Dictionary<Message, List<AudioClip>> messageClips;
    
    public AudioSource source;

    void Start () 
	{
        messageClips = new Dictionary<Message, List<AudioClip>>();
        foreach (MessageAudioClip clip in clips)
        {
            if (!messageClips.ContainsKey(clip.message))
            {
                messageClips.Add(clip.message, new List<AudioClip>());
            }
            messageClips[clip.message].Add(clip.clip);
        }
        SceneMessenger.Instance.AddGenericListener(new SceneMessenger.GenericCallback(PlayMessageSound));
	}
	
	public void PlayMessageSound(Message message)
    {
        if (messageClips.ContainsKey(message))
        {
            foreach (AudioClip clip in messageClips[message])
            {
                source.PlayOneShot(clip);
            }
        }
    }
}
