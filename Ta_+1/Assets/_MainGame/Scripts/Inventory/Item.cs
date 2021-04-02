using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            case ItemType.Sword:
                return ItemAssets.Instance.swordSprite;
            case ItemType.HealthPotion:
                return ItemAssets.Instance.healthPotionSprite;
            default:
                return null; //TODO?
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            case ItemType.HealthPotion:
                return true;
            case ItemType.Sword:
                return false;
            default:
                return false;
        }
    }
}
