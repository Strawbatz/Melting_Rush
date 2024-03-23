using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    public enum Sound {
        WallHit,
        Bounce, 
        Woosh, 
        RopeHit,
        Chiptune
    }
    [SerializeField] SoundAudioClip[] soundAudioClipArray;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("More than one sound manager!");
        } else {
            instance = this;
        }
    }

    private void Start() {
        PlaySound(Sound.Chiptune);
    }

    public void PlaySound(Sound sound) {
        GameObject soundObject = new GameObject("Sound");
        AudioSource source = soundObject.AddComponent<AudioSource>();
        source.PlayOneShot(GetAudioClip(sound));
    }
    private AudioClip GetAudioClip(Sound sound) {
        foreach(SoundAudioClip soundAudioClip in soundAudioClipArray) {
            if(soundAudioClip.sound == sound) {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound: " +sound+" does not exist");
        return null;
    }
}

[System.Serializable] public class SoundAudioClip {
    public SoundManager.Sound sound;
    public AudioClip audioClip;
}


