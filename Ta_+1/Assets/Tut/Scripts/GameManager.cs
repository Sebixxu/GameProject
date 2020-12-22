using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public TowerButton ClickedTowerButton { get; set; }
    
    [SerializeField]
    private Text currencyText;

    private int currency;

    public int Currency
    {
        get => currency;
        set
        {
            currency = value;
            currencyText.text = value + "<color=lime>$</color>";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Currency = 5;
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }

    public void PickTower(TowerButton towerButton)
    {
        if (Currency < towerButton.Price) 
            return;

        ClickedTowerButton = towerButton;
        Hover.Instance.ActivateHover(towerButton.IconSprite);
    }

    public void BuyTower()
    {
        if (Currency < ClickedTowerButton.Price) 
            return;

        Currency -= ClickedTowerButton.Price;
        Hover.Instance.DeactivateHover();
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.DeactivateHover();
        }
    }
}
