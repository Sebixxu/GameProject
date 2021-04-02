using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    public List<Item> Items => items;
    private List<Item> items;
    private Action<Item> useItemAction;

    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        items = new List<Item>();

        AddItem(new Item { amount = 1, itemType = ItemType.Sword });
        AddItem(new Item { amount = 2, itemType = ItemType.HealthPotion });
        AddItem(new Item { amount = 1, itemType = ItemType.Sword });

        Debug.Log(items.Count);
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            var currentItemInInventory = items.FirstOrDefault(x => x.itemType == item.itemType);

            if (currentItemInInventory != null)
            {
                currentItemInInventory.amount += item.amount;
            }
            else
            {
                items.Add(item);
            }
        }
        else
        {
            items.Add(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            var currentItemInInventory = items.FirstOrDefault(x => x.itemType == item.itemType);

            if (currentItemInInventory != null && currentItemInInventory.amount > 1)
            {
                currentItemInInventory.amount -= 1;
            }
            else
            {
                items.Remove(item);
            }
        }
        else
        {
            items.Remove(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Item item)
    {
        useItemAction(item);
    }
}
