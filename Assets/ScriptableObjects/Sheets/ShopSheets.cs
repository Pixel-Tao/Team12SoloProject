using System.Collections.Generic;

[ExcelAsset(ExcelName = "ShopSheets", AssetPath = "Resources/SO/Sheets")]
public class ShopSheets : Sheet
{
	public List<ShopEntity> ShopList;
	public List<ShopSaleEntity> ShopSaleList;
}
