using UnityEngine;
using UObject = UnityEngine.Object;

public class UITitleScene : UISceneBase
{
    enum Buttons
    {
        StartButton,
    }
    
    private ResourceDownloader resourceDownloader;

    private void Start()
    {
        resourceDownloader = gameObject.GetOrAddComponent<ResourceDownloader>();
        resourceDownloader.OnInitAddressableCompleted += AddressableCompleted;
        resourceDownloader.InitAddressableAsync(Defines.ADDRESSABLE_LABELS);
    }

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        GetButton(Buttons.StartButton).gameObject.BindEvent(StartButtonEvent);

        return true;
    }
    
    
    public void StartButtonEvent()
    {
        Managers.Scene.LoadScene(Defines.SceneType.GameScene);
    }
    
    

    private void AddressableCompleted(string[] labels, bool needUpdate, long patchSize)
    {
        if (needUpdate)
        {
            // 업데이트 필요한 경우
            // TODO : UI 처리
            resourceDownloader.OnDownloadProgress += Downloading;
            resourceDownloader.OnDownloadCompleted += DownloadCompleted;
            resourceDownloader.Download();
            Debug.Log("need to update and download start!");
        }
        else
        {
            // 업데이트가 필요하지 않는 경우
            // TODO : UI 처리
            Debug.Log("No need to update");
            DownloadCompleted(labels);
        }        
    }
    
    private void Downloading(long downloaded, long total)
    {
        // TODO : UI 처리
        
    }
    
    private void DownloadCompleted(string[] labels)
    {
        // TODO : UI 처리
        Managers.Resource.LoadAllAsync<UObject>(labels, (label, key, count, totalCount) =>
        {
            float ratio = count / (float)totalCount;
            
            if (count >= totalCount)
            {
                Debug.Log("Download Completed");
                
            
                Managers.DB.Init();
                Managers.Sound.Init();
                Managers.Sound.PlayBGM("BGM_Title");
            
                Invoke("SetVolume", 10f);
                Managers.Sound.SetMasterVolume(1f);

                Managers.UI.ShowPopupUI<UIDialoguePopup>()?.SetData(9710000, "누군가");
            }
            Debug.Log($"label: {label}, key: {key}, count: {count}, totalCount: {totalCount}, ratio: {ratio}");
            
        });
    }

    private void SetVolume()
    {
        Debug.Log("SetVolume");
        Managers.Sound.SetMasterVolume(0.1f);
    }
    
    private string LoadingMessage(int count, int total, string message)
    {
        return $"<color=#00FFFF>{message}</color> ({count}/{total})";
    }

    private string LoadingMessage(long totalSize)
    {
        return $"<color=#00FFFF>Downloading...</color> {Utils.GetFileSize(totalSize)}";
    }

}