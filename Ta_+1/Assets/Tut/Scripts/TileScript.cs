using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Input.GetMouseButtonDown(0))
        {
            PlaceTower();
        }
    }

    private void PlaceTower()
    {
        Instantiate(GameManager.Instance.TowerPrefab, transform.position, Quaternion.identity);
    }
}
