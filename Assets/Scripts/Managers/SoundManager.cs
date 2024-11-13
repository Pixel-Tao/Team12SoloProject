using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    private Dictionary<string, AudioClip> sfxClips;
    private Dictionary<string, AudioClip> bgmClips;

    private AudioSource bgmSource;
    private AudioSourcePool audioSourcePool;

    private AudioMixer masterMixer;

    private int defaultCapacity = 10;
    private int maxSize = 20;

    public override void Init()
    {
        base.Init();

        masterMixer = ResourceManager.Instance.Load<AudioMixer>("Sounds/MasterMixer");
        AudioClip[] bgms = ResourceManager.Instance.LoadAll<AudioClip>("Sounds/BGM");
        bgmClips = new Dictionary<string, AudioClip>();
        foreach (var clip in bgms)
            bgmClips.Add(clip.name, clip);

        Debug.Log($"BGM Loaded Count : {bgmClips.Count}");

        AudioClip[] sfxs = ResourceManager.Instance.LoadAll<AudioClip>("Sounds/SFX");
        sfxClips = new Dictionary<string, AudioClip>();
        foreach (var clip in sfxs)
            sfxClips.Add(clip.name, clip);

        Debug.Log($"SFX Loaded Count : {sfxClips.Count}");

        // BGM용 AudioSource 생성
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.outputAudioMixerGroup = masterMixer?.FindMatchingGroups("BGM")[0];
        bgmSource.loop = true;

        var sfxGroup = masterMixer?.FindMatchingGroups("SFX")[0];
        // AudioSource 풀 생성
        audioSourcePool = new AudioSourcePool(
            masterMixerGroup: sfxGroup,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    // 위치 기반 SFX 재생
    public void PlaySFX(string name, Vector3 position)
    {
        if (sfxClips.TryGetValue(name, out AudioClip clip))
        {
            AudioSource source = audioSourcePool.Pop();
            source.transform.position = position;
            source.clip = clip;
            source.Play();

            // 사운드가 끝나면 반환
            StartCoroutine(ReturnSourceWhenFinished(source, clip.length));
        }
        else
        {
            Debug.LogWarning($"SFX '{name}' not found!");
        }
    }

    private System.Collections.IEnumerator ReturnSourceWhenFinished(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSourcePool.Push(source);
    }

    // BGM 재생 (전역 사운드)
    public void PlayBGM(string name)
    {
        if (bgmClips.TryGetValue(name, out AudioClip clip))
        {
            bgmSource.clip = clip;
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning($"BGM '{name}' not found!");
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
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