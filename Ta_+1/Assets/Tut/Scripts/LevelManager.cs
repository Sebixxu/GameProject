using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovement cameraMovement;

    public Dictionary<Point, TileScript> Tiles { get; set; }
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
        Tiles = new Dictionary<Point, TileScript>();

        string[] mapData = ReadLevelTextFile();

        int maxXMapSize = mapData[0].ToCharArray().Length;
        int maxYMapSize = mapData.Length;

        var worldStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < maxYMapSize; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < maxXMapSize; x++)
            {
                PlaceTile(newTiles[x], x, y, worldStartPosition);
            }
        }

        var maxTile = Tiles[new Point(maxXMapSize - 1, maxYMapSize - 1)].transform.position;
        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));
    }

    private void PlaceTile(char tileType, int x, int y, Vector3 worldStartPosition)
    {
        int tileIndex = int.Parse(tileType.ToString());

        var tilePosition = new Point(x, y);
        var newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();
        
        newTile.Setup(tilePosition, new Vector3(worldStartPosition.x + TileSize * x, worldStartPosition.y - TileSize * y, 0));
        Tiles.Add(tilePosition, newTile);
    }

    private string[] ReadLevelTextFile()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, String.Empty);

        return data.Split('-');
    }
}
