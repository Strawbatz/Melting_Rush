using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    public enum Sound {
        WallHit,
        Bounce, 
        RopeHit,
        Chiptune,
        MouseOver,
        Click
    }
    [SerializeField] SoundAudioClip[] soundAudioClipArray;
    [SerializeField] GameObject audioSourcePrefab;
    private Stack<AudioSource> disabledSources = new Stack<AudioSource>();
    private AudioSource musicSource;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("More than one sound manager!");
        } else {
            instance = this;
            GameObject obj = Instantiate(audioSourcePrefab, transform);
            musicSource = obj.GetComponent<AudioSource>();
        }
    }

    void Start()
    {
        startPitch = musicSource.pitch;
    }

    public void PlaySound(Sound sound) {
        AudioSource source = GetSource();
        SoundAudioClip soundAudioClip = GetAudioClip(sound);
        source.clip = soundAudioClip.audioClip;
        source.volume = soundAudioClip.volume;
        source.Play();
        StartCoroutine(WaitThenDisable(source, source.clip.length));
    }

    public void PlayMusic(Sound sound) {
        if(sound == Sound.Chiptune) {
            SoundAudioClip soundAudioClip = GetAudioClip(sound);
            musicSource.clip = soundAudioClip.audioClip;
            musicSource.volume = soundAudioClip.volume;
            musicSource.Play();
        }
    }
    
    float startPitch;
    public void SetMusicSpeed(float speedMod)
    {
        musicSource.pitch = startPitch * speedMod;
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

    public void ToggleMusic(bool off) {
        if(off) {
            musicSource.Pause();
        } else {
            musicSource.UnPause();
        }
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
        yield return new WaitForSecondsRealtime(t);
        DisableSource(source);
    }

}

[System.Serializable] public class SoundAudioClip {
    public SoundManager.Sound sound;
    public AudioClip audioClip;
    
    [Range(0.0f, 1.0f)]
    public float volume;
}


