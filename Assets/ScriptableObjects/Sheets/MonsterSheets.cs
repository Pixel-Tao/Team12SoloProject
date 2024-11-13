using System.Collections.Generic;

[ExcelAsset(ExcelName = "MonsterSheets", AssetPath = "Resources/SO/Sheets")]
public class MonsterSheets : Sheet
{
	public List<MonsterEntity> MonsterList;
	public List<MonsterDropItemEntity> MonsterDropItemList;
}
