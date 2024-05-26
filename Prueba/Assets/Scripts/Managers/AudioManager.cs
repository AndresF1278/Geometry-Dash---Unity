using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class AudioParams
{
    public int ID;
    public AudioClip AudioClip;
    public String Description;
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private List <AudioParams> audioParams = new List<AudioParams>();
    private Dictionary<int, AudioParams> audioParamsDict = new Dictionary<int, AudioParams>();

    AudioSource audioSource;

    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        foreach(var param in audioParams)
        {
            audioParamsDict[param.ID] = param;
        }

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private AudioClip GetAudioByID(int id)
    {
        if(audioParamsDict.TryGetValue(id, out var audioParams))
        {
            return audioParams.AudioClip;

        }else {
            UnityEngine.Debug.LogWarning($"NO SE ENCONTRO AUDIO! REVISAR ID {id}");
            return null;
        }
    }

    public void PlayAudioWithSource(int ID, AudioSource source, bool isLoop = false) {

        if (source == null) { 
            UnityEngine.Debug.LogWarning("AudioSource no asignado."); 
            return;
        } 

        AudioClip audio = GetAudioByID(ID);
        if (audio == null) return;

        source.loop = isLoop;
        source.clip = audio;
        source.Play();

    }

    public void PlayAudio(int ID) {

        if (audioSource.isPlaying) { 
           audioSource.Stop();
        }

        AudioClip audio = GetAudioByID(ID);
        if (audio == null) return;

        audioSource.clip = audio;
        audioSource.Play();
    }

    public void StopAudio(){
        if (!audioSource.isPlaying) return;
        audioSource.Stop();
    }

}

