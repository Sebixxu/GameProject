using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private Sprite iconSprite;
    [SerializeField] 
    private int price;
    [SerializeField]
    private Text priceText;

    public GameObject TowerPrefab => towerPrefab;
    public Sprite IconSprite => iconSprite;
    public int Price => price;

    // Start is called before the first frame update
    void Start()
    {
        priceText.text = price + "$";

        GameManager.Instance.Changed += PriceCheck;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PriceCheck()
    {
        //if (price <= GameManager.Instance.Currency)
        //{
        //    GetComponent<Image>().color = Color.white;
        //    priceText.color = Color.white;
        //}
        //else
        //{
        //    GetComponent<Image>().color = Color.grey;
        //    priceText.color = Color.grey;
        //}
    }

    public void ShowInfo(string type)
    {
        string tooltip = String.Empty;
        switch (type)
        {
            case "Fire":
                FireTower fireTower = towerPrefab.GetComponentInChildren<FireTower>();
                tooltip = $"<color=#ffa500ff><size=20><b>Fire</b></size></color>\nDamage: {fireTower.Damage} \nChance for debuff: {fireTower.ChanceForDebuff}% \nDebuff duration: {fireTower.DebuffDuration} \nFire duration: {fireTower.DebuffDuration} \nHas chance to fire target";
                break;
            case "Frost":
                tooltip = "<color=#00ffffff><size=20><b>Frost</b></size></color>";
                break;
        }

        GameManager.Instance.SetTooltipText(tooltip);

        GameManager.Instance.ShowStats();
    }
}
