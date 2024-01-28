using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioName
{
    confirm,
    correct,
    incorrect,
    option,
    stage_start,
    talk1,
}

[System.Serializable]
public class AudioClipSet
{
    public AudioName audioName;
    public AudioClip audioClip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    public AudioSource bgm;
    public AudioSource sound;
    public List<AudioClipSet> AudioList;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;   
    }

    public void PlayBGM(AudioName name, bool loop = true)
    {
        AudioClip clip = GetAudio(name);
        if (clip != null)
        {
            bgm.clip = clip;
            bgm.loop = loop;
            bgm.Play(); 
        }
    }


    public void PlaySound(AudioName name)
    {
        AudioClip clip = GetAudio(name);
        if (clip != null)
        {
            sound.clip = clip;
            sound.Play();
        }
    }


    private AudioClip GetAudio(AudioName name)
    {
        AudioClipSet clipSet = AudioList.Find(c => c.audioName == name);
        if(clipSet != null)
        {
            return clipSet.audioClip;
        }
        return null;
    }
}
