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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
