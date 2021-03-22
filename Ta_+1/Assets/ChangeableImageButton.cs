using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeableImageButton : MonoBehaviour
{
    public bool IsDefaultState => _isDefaultState;

    [SerializeField]
    private Sprite mainSprite;
    [SerializeField]
    private Sprite mainPressedSprite;

    [SerializeField]
    private Sprite secondarySprite;
    [SerializeField]
    private Sprite secondaryPressedSprite;

    private bool _isDefaultState = true;

    private Image _image;
    private Button _button;

    // Start is called before the first frame update
    void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchButtonSprites()
    {
        if(_button.transition != Selectable.Transition.SpriteSwap)
            return;

        var buttonSpriteState = _button.spriteState;

        if (_isDefaultState)
        {
            _image.sprite = secondarySprite;
            buttonSpriteState.pressedSprite = secondaryPressedSprite;
        }

        if (!_isDefaultState)
        {
            _image.sprite = mainSprite;
            buttonSpriteState.pressedSprite = mainPressedSprite;
        }

        _button.spriteState = buttonSpriteState;
        _isDefaultState = !_isDefaultState;

    }

    public void SetDefaultSprite()
    {
        _isDefaultState = true;

        var buttonSpriteState = _button.spriteState;

        _image.sprite = mainSprite;
        buttonSpriteState.pressedSprite = mainPressedSprite;

        _button.spriteState = buttonSpriteState;
    }

    public void SetSecondarySprite()
    {
        _isDefaultState = false;

        var buttonSpriteState = _button.spriteState;

        _image.sprite = secondarySprite;
        buttonSpriteState.pressedSprite = secondaryPressedSprite;

        _button.spriteState = buttonSpriteState;

    }
}
