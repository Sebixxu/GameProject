using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; private set; }

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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(Point gridPosition, Vector3 worldPosition, Transform parent)
    {
        GridPosition = gridPosition;
        transform.position = worldPosition;
        transform.SetParent(parent);

        LevelManager.Instance.Tiles.Add(gridPosition, this);
    }

    private void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject() || GameManager.Instance.ClickedTowerButton == null)
            return;

        if (Input.GetMouseButtonDown(0))
            PlaceTower();
    }

    private void PlaceTower()
    {
        var tower = Instantiate(GameManager.Instance.ClickedTowerButton.TowerPrefab, transform.position, Quaternion.identity);
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;
        tower.transform.SetParent(transform);

        GameManager.Instance.BuyTower();
    }
}
