using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance = null;

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public static AudioManager Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType<AudioManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (this != Instance)
            Destroy(gameObject);
    }

    public void MusicStop()
    {
        audioSource.Stop();
    }

    public void PlayBGM(string musicName)
    {
        for(int i =0;i<audioClips.Length;i++) 
        {
            if (audioClips[i].name == musicName) 
            {
                audioSource.clip = audioClips[i];
                break;
            }
        }

        audioSource.Play();
    }
}
