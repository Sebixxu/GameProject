using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    public Dictionary<int, Item> Items => items;
    private Dictionary<int, Item> items;
    //private List<Item> items;
    private Action<Item> useItemAction;
    private List<ItemSlot> itemSlotsGameObjects;
    private const int InventorySize = 8;

    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        items = new Dictionary<int, Item>(InventorySize);
        itemSlotsGameObjects = GetChildObject(InventoryUI.Instance.ItemSlots, "InventoryItemSlot").Select(x => x.GetComponent<ItemSlot>()).ToList();

        SetupDictionary();

        AddItem(new Item { name = "Miecz", amount = 1, itemType = ItemType.Sword });
        AddItem(new Item { name = "Mikstura życia", amount = 2, itemType = ItemType.HealthPotion });
        AddItem(new Item { name = "Miecz2", amount = 1, itemType = ItemType.Sword });

        Debug.Log(items.Count);
    }

    public void SetupDictionary()
    {
        for (int i = 0; i < InventorySize; i++)
        {
            items.Add(i, null);
            itemSlotsGameObjects[i].SlotId = i;
            itemSlotsGameObjects[i].Item = null;
        }
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            var currentItemInInventory = items.FirstOrDefault(x => x.Value?.itemType == item.itemType);

            if (currentItemInInventory.Value != null)
            {
                currentItemInInventory.Value.amount += item.amount;
            }
            else
            {
                var firstEmptySlotIndex = items.FirstOrDefault(x => x.Value == null).Key;

                items[firstEmptySlotIndex] = itemSlotsGameObjects[firstEmptySlotIndex].Item = item;
            }
        }
        else
        {
            var firstEmptySlotIndex = items.FirstOrDefault(x => x.Value == null).Key;

            items[firstEmptySlotIndex] = itemSlotsGameObjects[firstEmptySlotIndex].Item = item;
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddItem(Item item, int index)
    {
        items[index] = itemSlotsGameObjects[index].Item = item;

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    //Dobrze byłoby to wyrzucić gdzieś jako helper
    public List<GameObject> GetChildObject(Transform parent, string _tag)
    {
        List<GameObject> childs = new List<GameObject>();

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.CompareTag(_tag))
            {
                childs.Add(child.gameObject);
            }
            //if (child.childCount > 0) //wersje z zakomenotwanym tym i odkomentowanym, tutaj akurat nie potrzebne, ale kiedys moze sie przydac
            //{
            //    GetChildObject(child, _tag);
            //}
        }

        return childs;
    }

    public void DropItem(int index, int amountToDrop)
    {
        var currentItemInInventory = items.ElementAt(index);

        DropItem(currentItemInInventory, amountToDrop);
    }

    public void DropItem(int index, bool fullRemove)
    {
        var currentItemInInventory = items.ElementAt(index);

        DropItem(currentItemInInventory, fullRemove);
    }

    public void DropItem(Item item, bool fullRemove)
    {
        var currentItemInInventory = items.FirstOrDefault(x => x.Value == item);

        DropItem(currentItemInInventory, fullRemove);
    }

    public void SwapItemSlot() //TODO
    {

    }

    public void SplitItemBetweenSlots(Item originalItem, int amountToNewSlot, int newSlotId, int previousSlotId)
    {
        var newItem = new Item
        {
            amount =  amountToNewSlot,
            itemType = originalItem.itemType,
            name = originalItem.name
        };

        Player.Instance.Inventory.DropItem(previousSlotId, amountToNewSlot);
        Player.Instance.Inventory.AddItem(newItem, newSlotId);
    }

    private void DropItem(KeyValuePair<int, Item> currentItemInInventory, int amountToDrop)
    {
        var item = currentItemInInventory.Value;

        if (item.IsStackable())
        {
            if (currentItemInInventory.Value != null && currentItemInInventory.Value.amount > 1 && currentItemInInventory.Value.amount != amountToDrop)
            {
                currentItemInInventory.Value.amount -= amountToDrop;
            }
            else
            {
                items[currentItemInInventory.Key] = null;
                itemSlotsGameObjects[currentItemInInventory.Key].Item = null;
            }
        }
        else
        {
            Debug.LogWarning("This shouldn't be possible.");
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    private void DropItem(KeyValuePair<int, Item> currentItemInInventory, bool fullRemove)
    {
        var item = currentItemInInventory.Value;

        if (item.IsStackable())
        {
            if (currentItemInInventory.Value != null && currentItemInInventory.Value.amount > 1 && !fullRemove)
            {
                currentItemInInventory.Value.amount -= 1;
            }
            else
            {
                items[currentItemInInventory.Key] = null;
                itemSlotsGameObjects[currentItemInInventory.Key].Item = null;
            }
        }
        else
        {
            items[currentItemInInventory.Key] = null;
            itemSlotsGameObjects[currentItemInInventory.Key].Item = null;
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Item item)
    {
        useItemAction(item);
    }
}
