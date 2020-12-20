using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public TowerButton ClickedTowerButton { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }

    public void PickTower(TowerButton towerButton)
    {
        ClickedTowerButton = towerButton;
        Hover.Instance.ActivateHover(towerButton.IconSprite);
    }

    public void BuyTower()
    {
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
