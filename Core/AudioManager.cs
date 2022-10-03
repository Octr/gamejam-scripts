using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Public static instance of the AudioManager for other classes to acccess.
    public static AudioManager instance;
    public Sound[] sounds;

    // Public Enum References for Audio Files in AudioManager.
    public enum AudioList
    {
        BGM_MENU,
        BGM_LEVEL1,
        AMB_WATER,
        SFX_LAUNCH,
        SFX_JUMP,
        SFX_SPLASH,
    };

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.awake;
            s.source.spatialBlend = s.spatialBlend;
            s.source.bypassListenerEffects = s.bypassListenerEffects;

            s.source.outputAudioMixerGroup = s.mixerGroup;
        }

        if (!SceneManager.GetSceneByName("Audio").isLoaded)
        {
            StartCoroutine("WaitForSceneLoad");
        }

    }

    // Custom internal method for handling Play() requests.
    private void PlayAudio(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
    }

    private void PauseAudio(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Pause();
    }

    private void StopAudio(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

    private void PlayAudioPitch(string sound, float pitch)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = pitch;

        s.source.Play();
    }

    private void WaitForAudio(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        StartCoroutine(WaitForAudioFinish(s.source));
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Custom public method for playing audio from AudioManager using AudioList enums.
    public void Play(AudioList audioList)
    {
        PlayAudio(audioList.ToString());
        Debug.Log("Playing Audio: " + audioList.ToString());
    }

    public void PlayPitch(AudioList audioList, float pitch)
    {
        PlayAudioPitch(audioList.ToString(), pitch);
        Debug.Log("Playing Audio: " + audioList.ToString());
    }

    public void Stop(AudioList audioList)
    {
        StopAudio(audioList.ToString());
        Debug.Log("Stopping Audio: " + audioList.ToString());
    }

    public void Pause(AudioList audioList)
    {
        PauseAudio(audioList.ToString());
        Debug.Log("Pausing Audio: " + audioList.ToString());
    }

    public void Wait(AudioList audioList)
    {
        WaitForAudio(audioList.ToString());
        Debug.Log("Waiting For Audio: " + audioList.ToString());
    }

    IEnumerator WaitForAudioFinish(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
    }

    IEnumerator WaitForSceneLoad()
    {
        while (!SceneManager.GetSceneByName("Audio").isLoaded)
        {
            yield return null;
        }

        // Do anything after proper scene has been loaded
        if (SceneManager.GetSceneByName("Audio").isLoaded)
        {
            instance.Play(AudioList.BGM_LEVEL1);
        }
    }

}