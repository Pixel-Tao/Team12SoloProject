using System.Collections.Generic;

[ExcelAsset(ExcelName = "ItemSheets", AssetPath = "Resources/SO/Sheets")]
public class ItemSheets : Sheet
{
	public List<ItemEntity> ItemList; // Replace 'EntityType' to an actual type that is serializable.
	public List<ItemEquipableEntity> EquipableList; // Replace 'EntityType' to an actual type that is serializable.
	public List<ItemStatBoostEffectEntity> StatBoostEffectList; // Replace 'EntityType' to an actual type that is serializable.
	public List<ItemConsumableEntity> ConsumableList; // Replace 'EntityType' to an actual type that is serializable.
}
