using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject draggedItemPlaceHolder;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;

    //placeholder
    private Image placeholderImage;
    private ItemSlot placeholderItemSlot;

    private void Awake()
    {
        //rectTransform = transform.GetChild(0).GetComponent<RectTransform>();
        //canvasGroup = transform.GetChild(0).GetComponent<CanvasGroup>();

        //placeholder
        placeholderImage = draggedItemPlaceHolder.transform.GetChild(0).GetComponent<Image>();
        placeholderItemSlot = draggedItemPlaceHolder.GetComponent<ItemSlot>();

        rectTransform = draggedItemPlaceHolder.transform.GetChild(0).GetComponent<RectTransform>();
        canvasGroup = draggedItemPlaceHolder.transform.GetChild(0).GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || eventData.pointerDrag.GetComponent<ItemSlot>()?.Item == null)
            return;

        Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<ItemSlot>().Item == null)
            return;

        placeholderImage.enabled = true;
        placeholderImage.sprite = eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<Image>().sprite;
        placeholderItemSlot = eventData.pointerDrag.GetComponent<ItemSlot>();

        draggedItemPlaceHolder.transform.position = startPosition = eventData.position;

        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //if (eventData.pointerDrag.GetComponent<ItemSlot>().Item == null)
        //    return;

        placeholderImage.enabled = false;

        //InventoryUI.Instance.RefreshInventoryItems();
        //rectTransform.position = startPosition;

        Debug.Log("OnEndDrag");
        //canvasGroup.alpha = 1f;
        //canvasGroup.blocksRaycasts = true;


    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position / canvas.scaleFactor; // ; // 
    }
}
