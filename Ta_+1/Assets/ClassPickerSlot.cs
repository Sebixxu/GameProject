using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassPickerSlot : MonoBehaviour
{
    public PlayerClassPickerModel PlayerClassPicker => playerClassPicker;

    [SerializeField] //Debug
    private PlayerClassPickerModel playerClassPicker;

    private Toggle _toggle;
    void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Event do zmiany opisów
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetActive(PlayerClassPickerModel playerClassPickerModel)
    {
        playerClassPicker = playerClassPickerModel;

        var image = gameObject.transform.GetComponent<Image>();

        image.sprite = playerClassPickerModel.icon;
        image.enabled = true;

        if(!playerClassPickerModel.isAvailable)
        {
            image.color = Color.gray;

            _toggle.interactable = playerClassPickerModel.isAvailable;
        }

        var toggleSpriteState = _toggle.spriteState;
        toggleSpriteState.disabledSprite = playerClassPickerModel.icon;
        toggleSpriteState.selectedSprite = playerClassPickerModel.clickedIcon;
        _toggle.spriteState = toggleSpriteState;

        _toggle.isOn = false;
    }
}
