using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMenuButton : MonoBehaviour
{
    [SerializeField]
    private GameObject[] uiItemsToHide;

    private ChangeableImageButton _changeableImageButton;

    // Start is called before the first frame update
    void Awake()
    {
        _changeableImageButton = GetComponent<ChangeableImageButton>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PerformClick()
    {
        foreach (var uiItem in uiItemsToHide)
        {
            uiItem.SetActive(!uiItem.activeSelf);
        }

        _changeableImageButton.SwitchButtonSprites();
    }
}
