using System;

public static class DesignEnums
{
    public enum ItemType
    {
        Consumable = 0,
        Gold = 1,
        Equipment = 2,
    }
    public enum RarityType
    {
        Common = 0,
        Rare = 1,
        Epic = 2,
        Unique = 3,
    }
    public enum EquipmentType
    {
        Weapon = 0,
        Armor = 1,
        Accessory = 2,
    }
    public enum CalcType
    {
        Override = 0,
        Add = 1,
        Multiply = 2,
    }
    public enum StatType
    {
        AttackDamage = 0,
        AttackSpeed = 1,
        Armor = 2,
        Hp = 3,
        Mp = 4,
        MoveSpeed = 5,
    }
    public enum ConsumeType
    {
        HpRecovery = 0,
        MpRecovery = 1,
        MoveSpeed = 2,
        AttackSpeed = 3,
        AttackDamage = 4,
        MaxHp = 5,
        MaxMp = 6,
    }
    public enum NpcInteractionType
    {
        Dialogue = 0,
        Trade = 1,
        Quest = 2,
        Crafting = 3,
        Information = 4,
    }
    public enum PortraitPositionType
    {
        Left = 0,
        Center = 1,
        Right = 2,
    }
}
