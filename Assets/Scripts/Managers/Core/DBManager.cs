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

    public void Init()
    {
        ItemEntityLoader = new ItemEntityLoader();
        ItemStatEntityLoader = new ItemStatEntityLoader();
        ItemConsumeEntityLoader = new ItemConsumeEntityLoader();
        ItemEquipEntityLoader = new ItemEquipEntityLoader();
        NpcEntityLoader = new NpcEntityLoader();
        ShopSaleEntityLoader = new ShopSaleEntityLoader();
        DialogueEntityLoader = new DialogueEntityLoader();
        DialogueOptionEntityLoader = new DialogueOptionEntityLoader();
        DialogueLineEntityLoader = new DialogueLineEntityLoader();
    }
    public void Clear()
    {
        
    }

}