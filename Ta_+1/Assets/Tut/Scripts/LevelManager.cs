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
        string[] mapData = ReadLevelTextFile();

        int maxXSize = mapData[0].ToCharArray().Length;
        int maxYSize = mapData.Length;

        var worldStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < maxYSize; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < maxXSize; x++)
            {
                var currentTile = PlaceTile(newTiles[x], x, y, worldStartPosition);

                if (x == maxXSize - 1 && y == maxYSize - 1) // TODO Will be fixed soon™.
                {
                    var maxTile = currentTile;
                    cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));
                }
            }
        }

    }

    private Vector3 PlaceTile(char tileType, int x, int y, Vector3 worldStartPosition)
    {
        int tileIndex = int.Parse(tileType.ToString());

        GameObject newTile = Instantiate(tilePrefabs[tileIndex]);
        newTile.transform.position = new Vector3(worldStartPosition.x + TileSize * x, worldStartPosition.y - TileSize * y);

        return newTile.transform.position;
    }

    private string[] ReadLevelTextFile()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, String.Empty);

        return data.Split('-');
    }
}
