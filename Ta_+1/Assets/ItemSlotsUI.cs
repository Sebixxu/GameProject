using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotsUI : MonoBehaviour
{
    [SerializeField]
    private SplitItemPanelUI splitItemPanelUi;

    public SplitItemPanelUI GetSplitItemPanelUi()
    {
        return splitItemPanelUi;
    }
}
