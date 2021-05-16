﻿/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading the Code Monkey Utilities
    I hope you find them useful in your projects
    If you have any questions use the contact form
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

//#define SOUND_MANAGER // Has Sound_Manager in project
//#define CURSOR_MANAGER // Has Cursor_Manager in project

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

    public Action ClickFunc = null;
    public Action MouseLeftClickWithLeftCtrlFunc = null;
    public Action MouseLeftClickWithLeftAltFunc = null;
    public Action MouseRightClickFunc = null;
    public Action MouseMiddleClickFunc = null;
    public Action MouseDownOnceFunc = null;
    public Action MouseUpFunc = null;
    public Action MouseOverOnceTooltipFunc = null;
    public Action MouseOutOnceTooltipFunc = null;
    public Action MouseOverOnceFunc = null;
    public Action MouseOutOnceFunc = null;
    public Action MouseOverFunc = null;
    public Action MouseOverPerSecFunc = null; //Triggers every sec if mouseOver
    public Action MouseUpdate = null;
    public Action<PointerEventData> OnPointerClickFunc;

    public enum HoverBehaviour
    {
        Custom,
        Change_Color,
        Change_Image,
        Change_SetActive,
    }
    public HoverBehaviour hoverBehaviourType = HoverBehaviour.Custom;
    private Action hoverBehaviourFunc_Enter, hoverBehaviourFunc_Exit;

    public Color hoverBehaviour_Color_Enter, hoverBehaviour_Color_Exit;
    public Image hoverBehaviour_Image;
    public Sprite hoverBehaviour_Sprite_Exit, hoverBehaviour_Sprite_Enter;
    public bool hoverBehaviour_Move = false;
    public Vector2 hoverBehaviour_Move_Amount = Vector2.zero;
    private Vector2 posExit, posEnter;
    public bool triggerMouseOutFuncOnClick = false;
    private bool mouseOver;
    private float mouseOverPerSecFuncTimer;
    public bool isSet;

    private Action internalOnPointerEnterFunc, internalOnPointerExitFunc, internalOnPointerClickFunc;

#if SOUND_MANAGER
        public Sound_Manager.Sound mouseOverSound, mouseClickSound;
#endif
#if CURSOR_MANAGER
        public CursorManager.CursorType cursorMouseOver, cursorMouseOut;
#endif


    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        internalOnPointerEnterFunc?.Invoke();
        if (hoverBehaviour_Move) transform.localPosition = posEnter;
        hoverBehaviourFunc_Enter?.Invoke();
        MouseOverOnceFunc?.Invoke();
        MouseOverOnceTooltipFunc?.Invoke();
        mouseOver = true;
        mouseOverPerSecFuncTimer = 0f;
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        internalOnPointerExitFunc?.Invoke();
        if (hoverBehaviour_Move) transform.localPosition = posExit;
        hoverBehaviourFunc_Exit?.Invoke();
        MouseOutOnceFunc?.Invoke();
        MouseOutOnceTooltipFunc?.Invoke();
        mouseOver = false;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        internalOnPointerClickFunc?.Invoke();

        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left when Input.GetKey(KeyCode.LeftControl):
                MouseLeftClickWithLeftCtrlFunc?.Invoke();
                break;
            case PointerEventData.InputButton.Left when Input.GetKey(KeyCode.LeftAlt):
                MouseLeftClickWithLeftAltFunc?.Invoke();
                break;
            default:
                OnPointerClickFunc?.Invoke(eventData);
                break;
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (triggerMouseOutFuncOnClick)
            {
                OnPointerExit(eventData);
            }

            ClickFunc?.Invoke();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
            MouseRightClickFunc?.Invoke();
        if (eventData.button == PointerEventData.InputButton.Middle)
            MouseMiddleClickFunc?.Invoke();
    }
    public void Manual_OnPointerExit()
    {
        OnPointerExit(null);
    }
    public bool IsMouseOver()
    {
        return mouseOver;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        MouseDownOnceFunc?.Invoke();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        MouseUpFunc?.Invoke();
    }

    void Update()
    {
        if (mouseOver)
        {
            MouseOverFunc?.Invoke();
            mouseOverPerSecFuncTimer -= Time.unscaledDeltaTime;
            if (mouseOverPerSecFuncTimer <= 0)
            {
                mouseOverPerSecFuncTimer += 1f;
                MouseOverPerSecFunc?.Invoke();
            }
        }

        MouseUpdate?.Invoke();

        isSet = MouseOverOnceFunc != null;

    }
    void Awake()
    {
        posExit = transform.localPosition;
        posEnter = (Vector2)transform.localPosition + hoverBehaviour_Move_Amount;
        SetHoverBehaviourType(hoverBehaviourType);

#if SOUND_MANAGER
            // Sound Manager
            internalOnPointerEnterFunc += () => { if (mouseOverSound != Sound_Manager.Sound.None) Sound_Manager.PlaySound(mouseOverSound); };
            internalOnPointerClickFunc += () => { if (mouseClickSound != Sound_Manager.Sound.None) Sound_Manager.PlaySound(mouseClickSound); };
#endif

#if CURSOR_MANAGER
            // Cursor Manager
            internalOnPointerEnterFunc += () => { if (cursorMouseOver != CursorManager.CursorType.None) CursorManager.SetCursor(cursorMouseOver); };
            internalOnPointerExitFunc += () => { if (cursorMouseOut != CursorManager.CursorType.None) CursorManager.SetCursor(cursorMouseOut); };
#endif
    }
    public void SetHoverBehaviourType(HoverBehaviour hoverBehaviourType)
    {
        this.hoverBehaviourType = hoverBehaviourType;
        switch (hoverBehaviourType)
        {
            case HoverBehaviour.Change_Color:
                hoverBehaviourFunc_Enter = delegate () { hoverBehaviour_Image.color = hoverBehaviour_Color_Enter; };
                hoverBehaviourFunc_Exit = delegate () { hoverBehaviour_Image.color = hoverBehaviour_Color_Exit; };
                break;
            case HoverBehaviour.Change_Image:
                hoverBehaviourFunc_Enter = delegate () { hoverBehaviour_Image.sprite = hoverBehaviour_Sprite_Enter; };
                hoverBehaviourFunc_Exit = delegate () { hoverBehaviour_Image.sprite = hoverBehaviour_Sprite_Exit; };
                break;
            case HoverBehaviour.Change_SetActive:
                hoverBehaviourFunc_Enter = delegate () { hoverBehaviour_Image.gameObject.SetActive(true); };
                hoverBehaviourFunc_Exit = delegate () { hoverBehaviour_Image.gameObject.SetActive(false); };
                break;
        }
    }









    /*
     * Class for temporarily intercepting a button action
     * Useful for Tutorial disabling specific buttons
     * */
    public class InterceptActionHandler
    {

        private Action removeInterceptFunc;

        public InterceptActionHandler(Action removeInterceptFunc)
        {
            this.removeInterceptFunc = removeInterceptFunc;
        }
        public void RemoveIntercept()
        {
            removeInterceptFunc();
        }
    }
    public InterceptActionHandler InterceptActionClick(Func<bool> testPassthroughFunc)
    {
        return InterceptAction("ClickFunc", testPassthroughFunc);
    }
    public InterceptActionHandler InterceptAction(string fieldName, Func<bool> testPassthroughFunc)
    {
        return InterceptAction(GetType().GetField(fieldName), testPassthroughFunc);
    }
    public InterceptActionHandler InterceptAction(System.Reflection.FieldInfo fieldInfo, Func<bool> testPassthroughFunc)
    {
        Action backFunc = fieldInfo.GetValue(this) as Action;
        InterceptActionHandler interceptActionHandler = new InterceptActionHandler(() => fieldInfo.SetValue(this, backFunc));
        fieldInfo.SetValue(this, (Action)delegate ()
        {
            if (testPassthroughFunc())
            {
                // Passthrough
                interceptActionHandler.RemoveIntercept();
                backFunc();
            }
        });

        return interceptActionHandler;
    }
}