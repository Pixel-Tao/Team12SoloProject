using System;

public class UserManager : IManager
{
    public event Action<long> OnGoldChanged;
    private long gold;
    public long CarriedGold => gold;
    
    public void Init()
    {
        
    }
    public void Clear()
    {
        
    }
    /// <summary>
    /// 골드를 지불
    /// </summary>
    /// <param name="amount"></param>
    public void Payment(long amount)
    {
        gold -= amount;
        OnGoldChanged?.Invoke(gold);
    }
    /// <summary>
    /// 골드를 얻음
    /// </summary>
    /// <param name="amount"></param>
    public void Earn(long amount)
    {
        gold += amount;
        OnGoldChanged?.Invoke(gold);
    }
    
}