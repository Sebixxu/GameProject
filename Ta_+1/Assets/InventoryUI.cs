using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : Singleton<InventoryUI>
{
    private Inventory _inventory;

    //Inv
    private Transform itemSlots;

    //Eq
    private Transform weaponSlot;
    private Transform chestSlot;

    private void Awake()
    {
        SetReferences();
    }

    private void SetReferences()
    {
        itemSlots = transform.Find("ItemSlots");

        var eqSlots = transform.Find("EqSlots");

        weaponSlot = eqSlots.Find("WeaponSlot");
        chestSlot = eqSlots.Find("ChestSlot");
    }

    public void SetInventory(Inventory inventory)
    {
        _inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        for (int i = 0; i < itemSlots.childCount; i++)
        {
            var currentSlot = itemSlots.GetChild(i);

            var image = currentSlot.GetChild(0).GetComponent<Image>();
            image.color = new Color(255, 255, 255, 0);
            image.sprite = null;

            var amountText = currentSlot.GetChild(1);
            amountText.gameObject.SetActive(false);
        }

        foreach (var inventoryItem in _inventory.Items.Select((value, i) => new { i, value }))
        {
            var currentSlot = itemSlots.GetChild(inventoryItem.i);
            currentSlot.GetComponent<ButtonUI>().ClickFunc = () =>
            {
                _inventory.UseItem(inventoryItem.value);
            };

            currentSlot.GetComponent<ButtonUI>().MouseRightClickFunc = () =>
            {
                //TODO Pytanie czy napewno
                _inventory.RemoveItem(inventoryItem.value);
            };

            var image = currentSlot.GetChild(0).GetComponent<Image>();
            image.color = new Color(255, 255, 255, 100);
            image.sprite = inventoryItem.value.GetSprite();

            var amountText = currentSlot.GetChild(1);
            var text = amountText.GetComponent<Text>();
            if (inventoryItem.value.amount > 1)
            {
                text.text = inventoryItem.value.amount.ToString();
                amountText.gameObject.SetActive(true);
            }
        }
    }

    private void RefreshEquipmentItems()
    {

    }
}
