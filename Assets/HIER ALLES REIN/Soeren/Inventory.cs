using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(ItemData itemData, int amount = 1)
    {
        InventoryItem existing = items.Find(i => i.itemData == itemData);
        if (existing != null && existing.amount < itemData.maxStack)
        {
            int addable = Mathf.Min(amount, itemData.maxStack - existing.amount);
            existing.amount += addable;
            amount -= addable;
        }
        while (amount > 0)
        {
            int toAdd = Mathf.Min(amount, itemData.maxStack);
            items.Add(new InventoryItem(itemData, toAdd));
            amount -= toAdd;
        }
    }

    public void RemoveItem(ItemData itemData, int amount = 1)
    {
        for (int i = items.Count - 1; i >= 0 && amount > 0; i--)
        {
            if (items[i].itemData == itemData)
            {
                if (items[i].amount > amount)
                {
                    items[i].amount -= amount;
                    amount = 0;
                }
                else
                {
                    amount -= items[i].amount;
                    items.RemoveAt(i);
                }
            }
        }
    }
}

