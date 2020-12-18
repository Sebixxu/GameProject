using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilePrefabs;

    public float TileSize => tilePrefabs[0].GetComponent<SpriteRenderer>().bounds.size.x;

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
        string[] mapData = new[]
        {
            "0000", "1111", "2222", "3333", "4444", "5555"
        };

        int maxXSize = mapData[0].ToCharArray().Length;
        int maxYSize = mapData.Length;

        var worldStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < maxYSize; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < maxXSize; x++)
            {
                PlaceTile(newTiles[x], x, y, worldStartPosition);
            }
        }
    }

    private void PlaceTile(char tileType, int x, int y, Vector3 worldStartPosition)
    {
        int tileIndex = int.Parse(tileType.ToString());

        GameObject newTile = Instantiate(tilePrefabs[tileIndex]);
        newTile.transform.position = new Vector3(worldStartPosition.x + TileSize * x, worldStartPosition.y - TileSize * y);
    }
}
