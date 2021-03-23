using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TowerBenchSlot : MonoBehaviour
{
    public int Price => _tower.Cost;
    public Sprite SpriteIcon => _spriteIcon;
    public Tower Tower => _tower;

    [SerializeField] //Debug
    private Sprite _spriteIcon;
    [SerializeField] //Debug
    private Tower _tower;

    private Button _button;

    // Start is called before the first frame update
    void Awake()
    {
        _button = gameObject.transform.GetComponent<Button>();
    }

    private void Start()
    {
        LevelManager.Instance.Changed += PriceCheck;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PriceCheck()
    {
        if(Tower == null)
            return;

        if (Price <= LevelManager.Instance.CurrentCombatPoints)
        {
            GetComponent<Image>().color = Color.white;
            //priceText.color = Color.white;
        }
        else
        {
            GetComponent<Image>().color = Color.grey;
            //priceText.color = Color.grey;
        }
    }

    public void SetActive(Sprite spriteIcon, Tower tower)
    {
        var image = gameObject.transform.GetComponent<Image>();

        _tower = tower;

        image.sprite = spriteIcon;
        image.enabled = true;

        _button.onClick.AddListener(() => GameManager.Instance.PickTower(this));

        //DEBUG
        _spriteIcon = spriteIcon;

        SetActiveTowerBenchSlotCost(_tower.Cost);
    }

    private void SetActiveTowerBenchSlotCost(int towerCost)
    {
        var costGameObject = transform.GetChild(0);
        var imageComponent = costGameObject.GetComponent<Image>();

        var benchCostModel = BenchCostPool.Instance.TowerCostModels.First(x => x.cost == towerCost);

        imageComponent.sprite = benchCostModel.image;
        imageComponent.enabled = true;
    }
}
