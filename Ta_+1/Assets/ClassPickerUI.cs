using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ClassPickerUI : MonoBehaviour
{
    public Toggle currentSelection => _toggleGroup.ActiveToggles().FirstOrDefault();
    public ToggleGroup ToggleGroup => _toggleGroup;

    [SerializeField] private StartButton startButton;

    [SerializeField] private Text nameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text attackSpeedText;
    [SerializeField] private Text damageText;
    [SerializeField] private Text maxHpText;
    [SerializeField] private Text movementSpeedText;
    [SerializeField] private Text defenseText;
    [SerializeField] private Text strengthText;
    [SerializeField] private Text agilityText;
    [SerializeField] private Text vitalityText;

    private ToggleGroup _toggleGroup;
    private PlayerClassPickerModel[] _playerClassPickerModels;
    private ClassPickerSlot[] _classPickerSlots;

    // Start is called before the first frame update
    void Start()
    {
        _playerClassPickerModels = ClassPickerPool.Instance.PlayerClassPickerModels;
        _classPickerSlots = transform.GetComponentsInChildren<ClassPickerSlot>();
        _toggleGroup = GetComponent<ToggleGroup>();

        LoadClassPickerUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadClassPickerUI()
    {
        Debug.Log("Updating Class Picker UI.");

        foreach (var playerClass in _playerClassPickerModels.Select((value, i) => new { i, value }))
        {
            //var sprite = tower.value.Value.GetComponent<SpriteRenderer>().sprite;

            //_towerBenchSlots[tower.i].SetActive(sprite, tower.value.Value);

            _classPickerSlots[playerClass.i].SetActive(playerClass.value);
        }
    }

    public void RefreshDescription()
    {
        if (currentSelection == null)
        {
            startButton.DisableButton();

            SetDefaultDescription();
            return;
        }

        startButton.EnableButton();

        var playerClassPicker = currentSelection.GetComponent<ClassPickerSlot>().PlayerClassPicker;

        nameText.text = playerClassPicker.name;
        descriptionText.text = playerClassPicker.description;
        attackSpeedText.text = playerClassPicker.playerStatistics.attackSpeed.ToString();
        damageText.text = playerClassPicker.playerStatistics.damage.ToString();
        maxHpText.text = playerClassPicker.playerStatistics.maxHp.ToString();
        movementSpeedText.text = playerClassPicker.playerStatistics.movementSpeed.ToString();
        defenseText.text = playerClassPicker.playerStatistics.defense.ToString();
        strengthText.text = playerClassPicker.playerStatistics.strength.ToString();
        agilityText.text = playerClassPicker.playerStatistics.agility.ToString();
        vitalityText.text = playerClassPicker.playerStatistics.vitality.ToString();
    }

    public void SetDefaultDescription()
    {
        nameText.text = descriptionText.text = attackSpeedText.text = damageText.text = maxHpText.text = movementSpeedText.text
            = defenseText.text = strengthText.text = agilityText.text = vitalityText.text = String.Empty;
    }

    public void ResetToggleState()
    {
        _toggleGroup.SetAllTogglesOff();
    }
}
