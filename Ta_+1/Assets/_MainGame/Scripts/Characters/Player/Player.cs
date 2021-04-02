using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public ExtendedStatistics Statistics => statistics;
    public float MovementInWater => _movementInWater;

    public Movement Movement
    {
        get { return _movement; }
        set { _movement = value; }
    }

    public static Player Instance;
    public float PlayerRange
    {
        get { return playerRange; }
        set { playerRange = value; }
    }

    //[SerializeField] private GameObject inventoryUiGameObject;
    [SerializeField] private float playerRange; //Defines transform.scale in each direction 

    private SpriteRenderer _spriteRenderer;
    private Movement _movement;
    private Inventory _inventory;
    private InventoryUI inventoryUi;
    private float _movementInWater;
    private bool _isInWater;
    private new void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _inventory = new Inventory(UseItem);
        inventoryUi = InventoryUI.Instance;

        inventoryUi.SetInventory(_inventory);

        #region Singleton

        if (Instance != null)
        {
            Debug.LogWarning("There is more than one instance of OreStorage!");
            return;
        }

        Instance = this;
        #endregion
    }

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _movement.MovementSpeed = statistics.movementSpeed;
        _movementInWater = statistics.movementSpeed / 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivatePlayerRange()
    {
        var rangeGameObject = transform.Find("Range");
        rangeGameObject.GetComponent<SpriteRenderer>().enabled = true;
        rangeGameObject.transform.localScale = new Vector3(playerRange * 2, playerRange * 2, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var chest = other.GetComponent<ChestTriggerCollider>();

        if (chest != null)
        {
            Debug.Log("Otwieram skrzynkę");

            _inventory.AddItem(new Item { amount = 2, itemType = ItemType.HealthPotion });
        }
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case ItemType.HealthPotion:
                Debug.Log("Using HP pot.");
                FlashRedColor();
                _inventory.RemoveItem(item);
                break;
        }
    }

    private void FlashRedColor()
    {
        GetComponent<MaterialTintColor>().SetTintColor(new Color(1, 0, 0, 1f));
    }
}
