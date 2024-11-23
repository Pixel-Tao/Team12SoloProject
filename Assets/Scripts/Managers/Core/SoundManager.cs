using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : IManager
{
    private readonly string SFX_KEY = "SFX";
    private readonly string BGM_KEY = "BGM";
    private readonly string MASTER_KEY = "MASTER";
    
    private AudioSource _bgmSource;
    public AudioSource BgmSource
    {
        get
        {
            if (_bgmSource == null)
            {
                GameObject bgmSourceObject = new GameObject { name = "@BGM" };
                _bgmSource = bgmSourceObject.AddComponent<AudioSource>();
                _bgmSource.outputAudioMixerGroup = masterMixer?.FindMatchingGroups("BGM")[0];
                _bgmSource.loop = true;
                Object.DontDestroyOnLoad(bgmSourceObject);
            }
            return _bgmSource;
        }
    }
    private AudioSourcePool audioSourcePool;
    

    private AudioMixer masterMixer;

    private int defaultCapacity = 10;
    private int maxSize = 20;
    
    private bool initialized;

    public void Init()
    {
        if (initialized) return;
        
        initialized = true;
        masterMixer = Managers.Resource.Load<AudioMixer>("Sounds/MasterMixer");
        var sfxGroup = masterMixer?.FindMatchingGroups("SFX")[0];
        // AudioSource 풀 생성
        audioSourcePool = new AudioSourcePool(
            masterMixerGroup: sfxGroup,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
        
        SetSFXVolume(PlayerPrefs.GetFloat(SFX_KEY, 1));
        SetBGMVolume(PlayerPrefs.GetFloat(BGM_KEY, 1));
        SetMasterVolume(PlayerPrefs.GetFloat(MASTER_KEY, 1));
    }
    public void Clear()
    {
        audioSourcePool?.Clear();
    }

    // 위치 기반 SFX 재생
    public void PlaySFX(string name, Vector3 position)
    {
        AudioClip clip = Managers.Resource.Load<AudioClip>(name);
        if (clip == null)
        {
            Debug.LogWarning($"SFX '{name}' not found!");
            return;
        }
        
        AudioSource source = audioSourcePool.Pop();
        source.transform.position = position;
        source.clip = clip;
        source.Play();

        // 사운드가 끝나면 반환
        Managers.Coroutine.StartCoroutine(name, ReturnSourceWhenFinished(source, clip.length));
    }

    private System.Collections.IEnumerator ReturnSourceWhenFinished(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSourcePool.Push(source);
    }

    // BGM 재생 (전역 사운드)
    public void PlayBGM(string name)
    {
        AudioClip clip = Managers.Resource.Load<AudioClip>(name);
        if (clip == null)
        {
            Debug.LogWarning($"BGM '{name}' not found!");
            return;
        }
        
        BgmSource.clip = clip;
        BgmSource.Play();
    }

    public void StopBGM()
    {
        BgmSource.Stop();
    }

    public void SetSFXVolume(float volume)
    {
        if (masterMixer == null) return;
        masterMixer.SetFloat("SFX", LinearToDecibel(volume));
    }

    public void SetBGMVolume(float volume)
    {
        if (masterMixer == null) return;
        masterMixer.SetFloat("BGM", LinearToDecibel(volume));
    }

    public void SetMasterVolume(float volume)
    {
        if (masterMixer == null) return;
        masterMixer.SetFloat("Master", LinearToDecibel(volume));
    }
    
    public void SaveVolume()
    {
        if (masterMixer.GetFloat("SFX", out float sfxVolume))
            PlayerPrefs.SetFloat(SFX_KEY, sfxVolume);
        if (masterMixer.GetFloat("BGM", out float bgmVolume))
            PlayerPrefs.SetFloat(BGM_KEY, bgmVolume);
        if (masterMixer.GetFloat("Master", out float masterVolume))
            PlayerPrefs.SetFloat(MASTER_KEY, masterVolume);
    }

    private float LinearToDecibel(float linear)
    {
        float dB;
        if (linear != 0)
            dB = 20f * Mathf.Log10(linear);
        else
            dB = -80f; // 음소거 수준
        return dB;
    }

    public void TransitionToSnapshot(string snapshotName, float transitionTime)
    {
        AudioMixerSnapshot snapshot = masterMixer.FindSnapshot(snapshotName);
        if (snapshot != null)
        {
            snapshot.TransitionTo(transitionTime);
        }
        else
        {
            Debug.LogWarning($"Snapshot '{snapshotName}' not found!");
        }
    }
}