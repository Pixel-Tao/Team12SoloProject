using UnityEngine;

public abstract class InitBase : MonoBehaviour
{
    private bool isInit = false;
    
    protected virtual bool Init()
    {
        if (isInit) // 한번이라도 초기화 했다면 다시 초기화 하지 않도록 하기.
            return false;
        
        isInit = true;
        return true;
    }
}