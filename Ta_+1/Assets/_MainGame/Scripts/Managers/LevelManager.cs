﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    public event CurrencyChanged Changed;

    public int currentLevel;
    private int _currentCombatPoints;

    public int CurrentCombatPoints
    {
        get { return _currentCombatPoints; }
        set
        {
            _currentCombatPoints = value;
            _combatPointsText.text = $"CP {value}/{MaxCombatPoints}";

            OnCurrencyChanged();
        }
    }


    public int MaxCombatPoints
    {
        get { return _maxCombatPoints; }
        set { _maxCombatPoints = value; }
    }

    public Portal BluePortal { get; set; }
    public Dictionary<Point, TileScript> Tiles { get; set; }
    public float TileSize => tilePrefabs[0].GetComponent<SpriteRenderer>().bounds.size.x;

    public Stack<Node> Path
    {
        get
        {
            if (path == null)
                GeneratePath();

            return new Stack<Node>(new Stack<Node>(path));
        }
        //set { path = value; }
    }

    [SerializeField]
    private Text _combatPointsText;
    [SerializeField]
    private int _maxCombatPoints;
    [SerializeField]
    private GameObject[] tilePrefabs;
    [SerializeField]
    private SpecialTileModel[] specialTilesModel;
    [SerializeField]
    private CameraMovement cameraMovement;
    [SerializeField]
    private Transform mapParentTransform;
    [SerializeField]
    private GameObject bluePortalPrefab;
    [SerializeField]
    private GameObject redPortalPrefab;

    private Point mapSize;
    private Stack<Node> path; //W tej implementacji założenie jest takie że droga obliczana jest raz na początku
    private Point _startMonsterRoutePoint;
    private Point _endMonsterRoutePoint;

    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
        SetStartCombatPoints();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetStartCombatPoints()
    {
        CurrentCombatPoints = _maxCombatPoints;
    }

    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();

        string[] mapData = ReadLevelTextFile();

        int maxXMapSize = mapData[0].Replace("|", String.Empty).Replace(" ", String.Empty).ToCharArray().Length;
        int maxYMapSize = mapData.Length;

        mapSize = new Point(maxXMapSize, maxYMapSize);

        var worldStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < maxYMapSize; y++)
        {
            var newTiles = mapData[y].Replace(" ", String.Empty).Split('|');

            for (int x = 0; x < maxXMapSize; x++)
            {
                PlaceTile(newTiles[x], x, y, worldStartPosition);
            }
        }

        var maxTile = Tiles[new Point(maxXMapSize - 1, maxYMapSize - 1)].transform.position;
        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

        //SpawnPortals();
    }

    private void SpawnPortals()
    {
        _startMonsterRoutePoint = new Point(0, 0);
        GameObject bluePortal =
            Instantiate(bluePortalPrefab, Tiles[_startMonsterRoutePoint].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        BluePortal = bluePortal.GetComponent<Portal>();
        BluePortal.name = "BluePortal";

        _endMonsterRoutePoint = new Point(11, 6);
        Instantiate(redPortalPrefab, Tiles[_endMonsterRoutePoint].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }

    public bool InBounds(Point position)
    {
        return position.X >= 0 && position.Y >= 0 && position.X < mapSize.X && position.Y < mapSize.Y;
    }

    private void PlaceTile(string tileTypeName, int x, int y, Vector3 worldStartPosition)
    {
        GameObject tilePrefab = null;
        TileType tileType = TileType.Tile;

        if (int.TryParse(tileTypeName, out int tileIndex))
        {
            tilePrefab = tilePrefabs[tileIndex];
            tileType = TileType.Tile;
        }
        else
        {
            var specialTile = specialTilesModel.FirstOrDefault(tile => tile.tileChar == tileTypeName.ToCharArray()[0]);
            if (specialTile != null)
            {
                tilePrefab = specialTile.specialTilePrefab;
                tileType = specialTile.tileType;
            }
            else
                Debug.LogError("Something went wrong in placing tiles. Tile with this char was not found in LevelManager.");
        }

        var tilePosition = new Point(x, y);
        var newTile = Instantiate(tilePrefab).GetComponent<TileScript>();

        newTile.Setup(tilePosition, new Vector3(worldStartPosition.x + TileSize * x, worldStartPosition.y - TileSize * y, 0), mapParentTransform, tileType);
    }

    private string[] ReadLevelTextFile()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, String.Empty);

        return data.Split('-');
    }

    public void GeneratePath()
    {
        path = AStar.GetPath(_startMonsterRoutePoint, _endMonsterRoutePoint);
    }

    public Stack<Node> GeneratePath(Point startingPoint, Point goalPoint)
    {
        return AStar.GetPath(startingPoint, goalPoint);
    }

    public void OnCurrencyChanged()
    {
        if (Changed != null)
        {
            Changed();
            Debug.Log("Currency changed.");
        }
    }
}
