using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public SoundArray[] sfxSoundArray;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Sound not found or is null");
        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }
    
    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Sound not found or is null");
        }
        else
        {
           sfxSource.PlayOneShot(sound.clip);
        }
    }
    
    public void PlaySFXArrayRandom(string name)
    {
        SoundArray soundArray = Array.Find(sfxSoundArray, x => x.name == name);

        if (soundArray == null)
        {
            Debug.Log("Sound not found or is null");
        }
        else
        {
            int i;
            i = Random.Range(0, soundArray.clip.Length);

            sfxSource.PlayOneShot(soundArray.clip[i]);
        }
    }
    
    
   /* 
    public void PlaySFXVariance(string name, float variance)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Sound not found or is null");
        }
        else
        {
            float _pitchVariance = Random.Range(-variance, variance);
            sfxSource.pitch = _pitchVariance;
             sfxSource.PlayOneShot(sound.clip);
        }
    }
    
    */
    
    
    
}
