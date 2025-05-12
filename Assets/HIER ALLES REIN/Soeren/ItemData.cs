using UnityEngine;

public enum ItemType
{
    HealPotion,
    ManaPotion,
    Buff,
    QuestItem
}

[CreateAssetMenu(fileName = "NewItemData", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string description;
    public ItemType itemType;
    public Sprite icon;
    public int maxStack = 1;
    public int healAmount; // Für Heiltränke
    public int manaAmount; // Für Manatränke
}

[System.Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int amount;

    public InventoryItem(ItemData data, int amount = 1)
    {
        this.itemData = data;
        this.amount = amount;
    }
}

