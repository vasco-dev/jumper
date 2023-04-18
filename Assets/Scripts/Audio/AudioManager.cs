using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager Instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        /*if (instance == null) instance = this;         Persistent audio on change scene
        else
        {
            Destroy(gameObject); 
            return;
        }

        DontDestroyOnLoad(gameObject);*/

        foreach (Sound s in sounds) //grabs each sound and changes them accordingly on awake
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop; 
            
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, s => s.name == name);

        if (s == null)
        {
            print(s + " sound not found");
            return;
        }


        s.source.Play();

        
    }

    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, s => s.name == name);

        if (s == null)
        {
            print(s + " sound not found");
            return;
        }


        s.source.PlayOneShot(s.clip);
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, s => s.name == name);

        if (s == null)
        {
            print(s + " sound not found");
            return;
        }


        s.source.Stop();
    }


    public void ToggleMute(string name, bool pause)
    {
        Sound s = Array.Find(sounds, s => s.name == name);

        if (s == null)
        {
            print(s + " sound not found");
            return;
        }

        

        if (pause == true) s.source.volume = 0f;
        else s.source.volume = s.volume;

        
    }

}
