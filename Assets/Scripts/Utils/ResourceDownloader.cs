using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceDownloader : InitBase
{
    /// <summary>
    /// Addressable 초기화 완료 이벤트 params
    /// bool : 초기화 성공 여부
    /// long : 다운로드 크기
    /// </summary>
    public event Action<bool, long> OnInitAddressableCompleted;
    /// <summary>
    /// 다운로드 진행 이벤트 params
    /// long : 현재 다운로드 크기
    /// long : 전체 다운로드 크기
    /// </summary>
    public event Action<long, long> OnDownloadProgress;
    /// <summary>
    /// 다운로드 완료 이벤트 params
    /// string[] 다운로드 완료한 labels
    /// </summary>
    public event Action<string[]> OnDownloadCompleted;
    
    private string[] labels;
    private long patchSize;
    private Dictionary<string, long> patchMap = new Dictionary<string, long>();

    private bool isAddressableInit;
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }
    
    public void InitAddressableAsync(params string[] labels)
    {
        this.labels = labels;
        Addressables.InitializeAsync().Completed += InitAddressableCompleted;        
    }
    void InitAddressableCompleted(AsyncOperationHandle<IResourceLocator> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            isAddressableInit = true;
            // 에셋 로드 작업
            CheckUpdateFiles(labels);
        }
        else
        {
            isAddressableInit = false;
            // 에셋 로드 실패
            Debug.LogError("Addressable Initialize Failed");
        }
    }
    
    public void CheckUpdateFiles(params string[] labels)
    {
        if (isAddressableInit == false)
        {
            Debug.LogError("Addressable is not initialized, InitAddressableAsync method first");
            return;
        }
        
        StartCoroutine(CoCheckUpdateFiles(labels));
    }
    
    IEnumerator CoCheckUpdateFiles(params string[] labels)
    {
        patchSize = default;
    
        foreach (var label in labels)
        {
            var size = Addressables.GetDownloadSizeAsync(label);
            yield return size;
            patchSize += size.Result;
        }
    
        if (patchSize > decimal.Zero)
        {
            OnInitAddressableCompleted?.Invoke(true, patchSize);
        }
        else
            OnInitAddressableCompleted?.Invoke(false, 0);
    }
    
    
    public void Download(params string[] labels)
    {
        StartCoroutine(PatchFiles(labels));
    }
    
    
    IEnumerator PatchFiles(params string[] labels)
    {
        foreach (var label in labels)
        {
            var handle = Addressables.GetDownloadSizeAsync(label);
            yield return handle;
    
            if (handle.Result != decimal.Zero)
            {
                StartCoroutine(DownloadLabel(label));
            }
        }
    
        yield return CheckDownload();
    }
    
    IEnumerator DownloadLabel(string label)
    {
        var handle = Addressables.DownloadDependenciesAsync(label, false);
    
        while (!handle.IsDone)
        {
            patchMap[label] = handle.GetDownloadStatus().DownloadedBytes;
            yield return new WaitForEndOfFrame();
        }
    
        patchMap[label] = handle.GetDownloadStatus().TotalBytes;
        Addressables.Release(handle);
    }
    
    IEnumerator CheckDownload()
    {
        long total = 0;
        while (true)
        {
            total += patchMap.Sum(t => t.Value);
            OnDownloadProgress?.Invoke(patchSize, total);
            if (total == patchSize)
            {
                OnDownloadCompleted?.Invoke(labels);
                break;
            }
    
            total = 0;
            yield return new WaitForEndOfFrame();
        }
    }
}
