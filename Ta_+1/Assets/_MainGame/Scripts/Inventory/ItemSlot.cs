using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : ItemSlotsUI, IDropHandler
{
    [SerializeField]  //DEBUG only
    private int slotId;

    public int SlotId
    {
        get { return slotId; }
        set { slotId = value; }
    }


    [SerializeField] //DEBUG only
    private Item item;

    public Item Item
    {
        get { return item; }
        set { item = value; }
    }

    private Image slotImage;

    // Start is called before the first frame update
    void Awake()
    {
        slotImage = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");

        var itemSlotGameObject = eventData.pointerDrag;
        var receivedItemSlot = itemSlotGameObject.GetComponent<ItemSlot>();

        if (receivedItemSlot.item == null)
            return;

        if (item == null)
        {
            var receivedItemInInventory = Player.Instance.Inventory.Items[receivedItemSlot.SlotId]; //Bład gdy w ex są 2 takie same przedmioty - bez sensu

            if (receivedItemInInventory != null) //Chyba zbędny if
            {   
                //var receivedItemSlotImage = itemSlotGameObject.transform.GetChild(0).GetComponent<Image>();
                // receivedItemSlotImage.sprite = null;

                if (receivedItemInInventory.amount > 1 && Input.GetKey(KeyCode.LeftControl) && receivedItemInInventory.IsStackable())
                {
                    Debug.Log("Drop with ctrl");

                    var splitItemPanelUi = gameObject.GetComponent<Transform>().parent.GetComponent<ItemSlotsUI>().GetSplitItemPanelUi();
                    splitItemPanelUi.ProceedItemSplit(receivedItemInInventory, receivedItemInInventory.name, receivedItemInInventory.amount, receivedItemSlot.SlotId, slotId);
                }
                else
                {
                    Player.Instance.Inventory.DropItem(receivedItemSlot.SlotId, true);
                    Player.Instance.Inventory.AddItem(receivedItemInInventory, slotId);
                }
            }

            Debug.Log("Item wasn't null.");

            //Process drag and drop
        }

        //if (eventData.pointerDrag != null)
        //{
        //    //logika upuszczenia przedmiotu

        //    slotImage.sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
        //    slotImage.color = new Color(0, 0, 0, 1);
        //    //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
        //    //    GetComponent<RectTransform>().anchoredPosition;
        //}
    }
}
