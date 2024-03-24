using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;
    public enum Sound {
        WallHit,
        Bounce, 
        RopeHit,
        Chiptune,
        MouseOver,
        Click,
        Complete,
        MainMenu
    }
    [SerializeField] SoundAudioClip[] soundAudioClipArray;
    [SerializeField] GameObject audioSourcePrefab;
    private Stack<AudioSource> disabledSources = new Stack<AudioSource>();
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource speedyMusic;

    private void Awake() {
        if(instance != null) {
            Debug.LogWarning("More than one sound manager!");
        } else {
            instance = this;
        }
    }

    void Start()
    {
        startPitch = musicSource.pitch;
        Time.timeScale = 1;
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
            musicSource.loop = false;
            musicSource.Play();
            speedyMusic.clip = musicSource.clip;
            musicStartVolume = musicSource.volume;
        }
        if(sound == Sound.MainMenu) {
            SoundAudioClip soundAudioClip = GetAudioClip(sound);
            musicSource.clip = soundAudioClip.audioClip;
            musicSource.volume = soundAudioClip.volume;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
    
    float startPitch;
    float musicStartVolume;
    public void SetMusicSpeed(float speedMod)
    {
        if(paused) return;
        musicSource.pitch = startPitch*speedMod;
        if(speedMod > 1f && !speedyMusic.isPlaying)
        {
            musicSource.volume = 0;
            speedyMusic.time = musicSource.time;
            speedyMusic.Play();
        } else if(speedMod <= 1f && speedyMusic.isPlaying)
        {
            speedyMusic.Stop();
            musicSource.volume = musicStartVolume;
        }
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

    bool paused = false;
    public void ToggleMusic(bool off) {
        if(off) {
            paused = true;
            musicSource.Pause();
            speedyMusic.Pause();
        } else {
            paused = false;
            musicSource.UnPause();
            speedyMusic.UnPause();
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


