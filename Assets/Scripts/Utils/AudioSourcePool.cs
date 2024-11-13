using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

public class AudioSourcePool
{
    private IObjectPool<AudioSource> pool;

    private AudioMixerGroup masterMixerGroup;
    private Transform parent;
    private Transform root
    {
        get
        {
            if (parent == null)
            {
                parent = new GameObject { name = $"@Sound_Pool" }.transform;
            }

            return parent;
        }
    }

    public AudioSourcePool(AudioMixerGroup masterMixerGroup, Transform parent = null, int defaultCapacity = 10, int maxSize = 10)
    {
        this.masterMixerGroup = masterMixerGroup;
        this.parent = parent;
        this.pool = new ObjectPool<AudioSource>(
            createFunc: CreateObject,
            actionOnGet: GetObject,
            actionOnRelease: ReleaseObject,
            actionOnDestroy: DestoryObject,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
        
    }
    private AudioSource CreateObject()
    {
        GameObject obj = new GameObject("PooledAudioSource");
        obj.transform.SetParent(root);
        AudioSource source = obj.AddComponent<AudioSource>();
        source.spatialBlend = 1.0f; // 3D 사운드로 설정
        source.outputAudioMixerGroup = masterMixerGroup;
        return source;
    }

    private void GetObject(AudioSource obj)
    {
        obj.gameObject.SetActive(true);
    }
    
    private void ReleaseObject(AudioSource obj)
    {
        obj.gameObject.SetActive(false);
    }
    
    private void DestoryObject(AudioSource obj)
    {
        GameObject.Destroy(obj.gameObject);
    }

    public AudioSource Pop()
    {
        return pool.Get();
    }

    public void Push(AudioSource source)
    {
        source.Stop();
        source.clip = null;
        source.transform.position = Vector3.zero;
        source.gameObject.SetActive(false);
        pool.Release(source);
    }
    
    public void Clear()
    {
        pool.Clear();
    }
}