using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;

    public float TileSize => tile.GetComponent<SpriteRenderer>().bounds.size.x;

    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateLevel()
    {
        var worldStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                PlaceTile(x, y, worldStartPosition);
            }
        }
    }

    private void PlaceTile(int x, int y, Vector3 worldStartPosition)
    {
        GameObject newTile = Instantiate(tile);
        newTile.transform.position = new Vector3(worldStartPosition.x + TileSize * x, worldStartPosition.y - TileSize * y);
    }
}
