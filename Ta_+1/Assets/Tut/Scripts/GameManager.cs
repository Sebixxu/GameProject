using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public delegate void CurrencyChanged();

public class GameManager : Singleton<GameManager>
{
    public event CurrencyChanged Changed;

    public TowerButton ClickedTowerButton { get; set; }
    public ObjectPool ObjectPool { get; set; }

    [SerializeField]
    private Text currencyText;

    [SerializeField]
    private Text statText;

    [SerializeField]
    private float monsterHealth = 10f;

    [SerializeField]
    private GameObject inGameMenu;
    
    [SerializeField]
    private GameObject optionsMenu;

    [SerializeField] 
    private GameObject statsPanel;

    private int currency;
    private Tower selectedTower;

    public int Currency
    {
        get => currency;
        set
        {
            currency = value;
            currencyText.text = value + "<color=lime>$</color>";

            OnCurrencyChanged();
        }
    }

    private void Awake()
    {
        ObjectPool = GetComponent<ObjectPool>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Currency = 20;
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
            if (GameManager.Instance.ClickedTowerButton == null)
                ShowInGameMenu();

            Hover.Instance.DeactivateHover();
        }
    }

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();

        int monsterIndex = UnityEngine.Random.Range(0, 4);
        string type = String.Empty;

        switch (monsterIndex)
        {
            case 0:
                type = "BlueMonster";
                break;
            case 1:
                type = "RedMonster";
                break;
            case 2:
                type = "GreenMonster";
                break;
            case 3:
                type = "PurpleMonster";
                break;
        }

        Monster monster = ObjectPool.GetObject(type).GetComponent<Monster>();
        monster.Spawn(monsterHealth);

        yield return new WaitForSeconds(2.5f);
    }

    public void SelectTower(Tower tower)
    {
        if (selectedTower != null)
            selectedTower.Select(); //Odznaczenie obecnie zaznaczonego obiektu

        selectedTower = tower;

        selectedTower.Select();
    }

    public void DeselectTower()
    {
        if (selectedTower != null)
        {
            selectedTower.Select();
        }

        selectedTower = null;
    }

    public void OnCurrencyChanged()
    {
        if(Changed != null)
        {
            Changed();
            Debug.Log("Currency changed.");
        }
    }

    public void ShowStats()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
    }

    public void SetTooltipText(string text)
    {
        statText.text = text;
    }

    public void ShowInGameMenu()
    {
        optionsMenu.SetActive(false);
        inGameMenu.SetActive(!inGameMenu.activeSelf);

        Time.timeScale = !inGameMenu.activeSelf ? 1 : 0;
    }

    public void RestartGame()
    {

    }

    public void ShowOptions()
    {
        inGameMenu.SetActive(false);

        optionsMenu.SetActive(true);
    }

    public void QuitGame()
    {

    }
}

