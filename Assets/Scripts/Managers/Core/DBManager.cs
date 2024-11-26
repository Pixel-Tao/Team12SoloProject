using System.Collections.Generic;
using UnityEngine;

public class DBManager : IManager
{
    private const string dataListDirPath = "DataList";
    
    public ItemEntityLoader ItemEntityLoader;
    public ItemStatEntityLoader ItemStatEntityLoader;
    public ItemConsumeEntityLoader ItemConsumeEntityLoader;
    public ItemEquipEntityLoader ItemEquipEntityLoader;
    public NpcEntityLoader NpcEntityLoader;
    public ShopSaleEntityLoader ShopSaleEntityLoader;
    public DialogueEntityLoader DialogueEntityLoader;
    public DialogueOptionEntityLoader DialogueOptionEntityLoader;
    public DialogueLineEntityLoader DialogueLineEntityLoader;
    private TextAsset LoadJson(string addressableName) => Managers.Resource.Load<TextAsset>(addressableName); 

    public void Init()
    {
        ItemEntityLoader = new ItemEntityLoader(LoadJson);
        ItemStatEntityLoader = new ItemStatEntityLoader(LoadJson);
        ItemConsumeEntityLoader = new ItemConsumeEntityLoader(LoadJson);
        ItemEquipEntityLoader = new ItemEquipEntityLoader(LoadJson);
        NpcEntityLoader = new NpcEntityLoader(LoadJson);
        ShopSaleEntityLoader = new ShopSaleEntityLoader(LoadJson);
        DialogueEntityLoader = new DialogueEntityLoader(LoadJson);
        DialogueOptionEntityLoader = new DialogueOptionEntityLoader(LoadJson);
        DialogueLineEntityLoader = new DialogueLineEntityLoader(LoadJson);
    }
    
    
    public void Clear()
    {
        
    }

}