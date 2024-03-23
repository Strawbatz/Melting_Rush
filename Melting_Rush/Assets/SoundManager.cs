using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] GameObject audioSourcePrefab;
    private Stack<AudioSource> disabledSources = new Stack<AudioSource>();

    private void Awake() {
        
    }

    private void Start() {
        if(instance != null) {
            Debug.LogWarning("More than one sound manager!");
        } else {
            instance = this;
        }
        PlaySound(Sound.Chiptune);
    }

    public void PlaySound(Sound sound) {
        AudioSource source = GetSource();
        SoundAudioClip soundAudioClip = GetAudioClip(sound);
        source.clip = soundAudioClip.audioClip;
        source.volume = soundAudioClip.volume;
        source.Play();
        StartCoroutine(WaitThenDisable(source, source.clip.length));
    }
    private SoundAudioClip GetAudioClip(Sound sound) {
        foreach(SoundAudioClip soundAudioClip in soundAudioClipArray) {
            if(soundAudioClip.sound == sound) {
                return soundAudioClip;
            }
        }
        Debug.LogError("Sound: " +sound+" does not exist");
        return null;
    }

    private void DisableSource(AudioSource source){
        source.gameObject.SetActive(false);
        disabledSources.Push(source);
    }

    private AudioSource GetSource() {
        AudioSource source;
        if(disabledSources.Count == 0) {
            GameObject obj = Instantiate(audioSourcePrefab, transform);
            source = obj.GetComponent<AudioSource>();
        } else {
            source = disabledSources.Pop();
        }
        source.gameObject.SetActive(true);
        return source;
    }

    private IEnumerator WaitThenDisable(AudioSource source, float t) {
        yield return new WaitForSeconds(t);
        DisableSource(source);
    }

}

[System.Serializable] public class SoundAudioClip {
    public SoundManager.Sound sound;
    public AudioClip audioClip;
    
    [Range(0.0f, 1.0f)]
    public float volume;
}


