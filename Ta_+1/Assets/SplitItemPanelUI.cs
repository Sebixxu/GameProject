using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplitItemPanelUI : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Text currentSlotValueText;

    [SerializeField]
    private Text newSlotValueText;

    [SerializeField]
    private Text titleText;

    private Item _itemToSplit;
    private int _itemIndexFrom;
    private int _itemIndexTo;

    private int _currentSlotValue; //Left Value
    private int _newSlotValue; //Right Value

    private int _startLeftSlotValue; //Left Start Value
    private int _startRightSlotValue; //Right Start Value

    private int _newValue;

    // Start is called before the first frame update
    void Awake()
    {
        slider.onValueChanged.AddListener((v) => { ChangeValues((int)v); });

        SetDefaultValues();

        UpdateValueTextFields();
    }

    private void SetDefaultValues()
    {
        _currentSlotValue = _startLeftSlotValue = 0;
        _newSlotValue = _startRightSlotValue = 0;

        slider.maxValue = _currentSlotValue;
    }

    public void AcceptButtonClick()
    {
        if((int)slider.value > 0)
            Player.Instance.Inventory.SplitItemBetweenSlots(_itemToSplit, (int)slider.value, _itemIndexTo, _itemIndexFrom);

        SetDefaultValues();
        GameManager.Instance.TurnOffPauseBlockImage();
        TurnOff();
    }

    public void RejectButtonClick()
    {
        SetDefaultValues();
        GameManager.Instance.TurnOffPauseBlockImage();
        TurnOff();
    }

    public int GetNewValueForItemSlot()
    {
        return _newSlotValue;
    }

    public void ChangeValues(int newValue)
    {
        _newValue = newValue;

        _currentSlotValue = _startLeftSlotValue - _newValue;
        _newSlotValue = _startRightSlotValue + _newValue;

        UpdateValueTextFields();
    }

    private void TurnOn(Item itemToSplit, string itemName, int itemSlotAmount, int itemIndexFrom, int itemIndexTo)
    {
        gameObject.SetActive(true);

        _itemToSplit = itemToSplit;

        _itemIndexFrom = itemIndexFrom;
        _itemIndexTo = itemIndexTo;

        _currentSlotValue = _startLeftSlotValue = itemSlotAmount;
        slider.maxValue = _currentSlotValue;

        titleText.text = $"Split: {itemName}";
        
        UpdateValueTextFields();
    }

    public void ProceedItemSplit(Item itemToSplit, string itemName, int itemSlotAmount, int itemIndexFrom, int itemIndexTo) //TODO itemName i itemSlotAmonut pobrać z item to split
    {
        GameManager.Instance.TurnOnPauseBlockImage();

        TurnOn(itemToSplit, itemName, itemSlotAmount, itemIndexFrom, itemIndexTo);
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }

    private void UpdateValueTextFields()
    {
        currentSlotValueText.text = _currentSlotValue.ToString();
        newSlotValueText.text = _newSlotValue.ToString();
    }
}
