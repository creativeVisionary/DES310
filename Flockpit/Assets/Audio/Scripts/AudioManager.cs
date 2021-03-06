﻿using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    //Sets ranges for volume and pitch
    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;
    
    //Sets random range for volume and pitch
    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    //Allows Looping
    public bool loop = false;

    private AudioSource source;

    public void SetSource (AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
    }

    public void Play()
    {
        //Finds and plays sounds in the array
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    }

    public void Stop()
    {
        //Function for stopping sounds that are playing
        source.Stop();
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    //Creates array for sounds
    [SerializeField]
    Sound[] sounds;

    void Awake()
    {
        //Detects Audio Manager
        if (instance != null)
        {
            Debug.LogError("More than one AM in scene");
        }
        else
        {
            instance = this;
        }
        
    }

    void Start()
    {
        //Creates sounds in the scene
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }

        PlaySound("Ambient");
    }

    public void PlaySound(string _name)
    {
        //Calls play
        for (int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        // no named sound
        Debug.LogWarning("Audio Manager: No Sound Found" + _name);
    }

    public void StopSound(string _name)
    {
        //Calls stop
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }

        // no named sound
        Debug.LogWarning("Audio Manager: No Sound Found" + _name);
    }
}
