using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : Singleton<InventoryUI>
{
    //inv
    private Transform itemSlots;

    public Transform ItemSlots
    {
        get { return itemSlots; }
        set { itemSlots = value; }
    }


    private Inventory _inventory;


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

    public void RefreshInventoryItems()
    {
        //czyszczenie
        for (int i = 0; i < itemSlots.childCount; i++)
        {
            var currentSlot = itemSlots.GetChild(i);

            var image = currentSlot.GetChild(0).GetComponent<Image>();
            image.color = new Color(255, 255, 255, 0);
            image.sprite = null;

            var amountText = currentSlot.GetChild(1);
            amountText.gameObject.SetActive(false);
        }

        //rysowanie
        foreach (var inventoryItem in _inventory.Items)
        {
            if(inventoryItem.Value == null)
                continue;

            var currentSlot = itemSlots.GetChild(inventoryItem.Key);

            currentSlot.GetComponent<ButtonUI>().ClickFunc = () =>
            {
                if(_inventory.Items.ContainsValue(inventoryItem.Value))
                    _inventory.UseItem(inventoryItem.Value);
            };

            currentSlot.GetComponent<ButtonUI>().MouseLeftClickWithLeftCtrlFunc = () =>
            {
                Debug.Log("Left Click + Left Ctrl"); //CLICK, NO DROP
            };

            currentSlot.GetComponent<ButtonUI>().MouseLeftClickWithLeftAltFunc = () =>
            {
                Debug.Log("Left Click + Left Alt"); //CLICK, NO DROP
            };

            currentSlot.GetComponent<ButtonUI>().MouseRightClickFunc = () =>
            {
                //TODO Pytanie czy napewno
                if (_inventory.Items.ContainsValue(inventoryItem.Value))
                    _inventory.DropItem(inventoryItem.Value, false);
            };

            var image = currentSlot.GetChild(0).GetComponent<Image>();
            image.color = new Color(255, 255, 255, 100);
            image.sprite = inventoryItem.Value.GetSprite();

            var amountText = currentSlot.GetChild(1);
            var text = amountText.GetComponent<Text>();
            if (inventoryItem.Value.amount > 1)
            {
                text.text = inventoryItem.Value.amount.ToString();
                amountText.gameObject.SetActive(true);
            }
        }
    }
}
