﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private Color32 fullColor = new Color32(255, 118, 118, 255);
    private Color32 emptyColor = new Color32(96, 255, 90, 255);
    private Color32 defaultColor = Color.white;

    private Tower myTower;
    //public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public TileType TileType { get; private set; }
    public Point GridPosition { get; private set; }
    public bool IsEmpty { get; private set; }
    public bool DebugOn { get; set; }
    public bool Walkable { get; set; }

    public Vector2 WorldPosition
    {
        get
        {
            var position = transform.position;
            return new Vector2(position.x + GetComponent<SpriteRenderer>().bounds.size.x / 2, position.y - GetComponent<SpriteRenderer>().bounds.size.y / 2);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(Point gridPosition, Vector3 worldPosition, Transform parent, TileType tileType)
    {
        IsEmpty = true;
        Walkable = true;

        GridPosition = gridPosition;
        transform.position = worldPosition;
        TileType = tileType;
        transform.SetParent(parent);

        LevelManager.Instance.Tiles.Add(gridPosition, this);
    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedTowerButton != null)
        {
            if (IsEmpty)
                ColorTile(emptyColor);

            if (!IsEmpty)
                ColorTile(fullColor);
            else if (Input.GetMouseButtonDown(0))
                PlaceTower();
        }
        else if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedTowerButton == null && Input.GetMouseButtonDown(0))
        {
            if (myTower != null)
            {
                GameManager.Instance.SelectTower(myTower);
            }
            else
            {
                GameManager.Instance.DeselectTower();
            }
        }
    }

    private void OnMouseExit()
    {
        if(!DebugOn)
            ColorTile(defaultColor);
    }

    private void PlaceTower()
    {
        var tower = Instantiate(GameManager.Instance.ClickedTowerButton.TowerPrefab, transform.position, Quaternion.identity);
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;
        tower.transform.SetParent(transform);

        myTower = tower.transform.GetChild(0).GetComponent<Tower>();

        GameManager.Instance.BuyTower();

        IsEmpty = false;
        Walkable = false;
        ColorTile(defaultColor);
    }

    private void ColorTile(Color32 newColor)
    {
        _spriteRenderer.color = newColor;
    }
}
