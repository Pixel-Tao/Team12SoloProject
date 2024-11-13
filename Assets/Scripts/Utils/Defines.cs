public static class Defines
{
  public enum CalcType
  {
    Add,
    Multiply,
    Override,
  }
  /// <summary>
  /// 아이템 타입
  /// </summary>
  public enum ItemType
  {
    None,
    Equipment,
    Consumable,
    Resource,
    Gold,
  }
  
  /// <summary>
  /// 아이템 소모품 타입
  /// </summary>
  public enum ItemConsumableType
  {
    None,
    HpRecovery,
    MpRecovery,
    MoveSpeed,
    AttackSpeed,
    AttackDamage,
    MaxHp,
  }
  
  /// <summary>
  /// 아이템 장비 타입
  /// </summary>
  public enum ItemEquipmentType
  {
    None,
    Weapon,
    Armor,
    Accessory,
  }

  /// <summary>
  /// 아이템 희귀도 타입
  /// </summary>
  public enum ItemRarityType
  {
    None,
    Common,
    Rare,
    Epic,
    Unique,
  }

  /// <summary>
  /// 캐릭터 스탯 타입
  /// </summary>
  public enum CharacterStatType
  {
    None,
    Hp,
    Mp,
    AttackDamage,
    AttackSpeed,
    MoveSpeed,
    Armor,
  }
}