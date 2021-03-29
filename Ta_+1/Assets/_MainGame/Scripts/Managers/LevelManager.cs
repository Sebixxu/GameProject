using System;
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

        string[] levelZeroMapData = ReadLevelZeroTextFile();
        string[] levelOneMapData = ReadLevelOneTextFile();

        int maxXMapSize = levelZeroMapData[0].Replace("|", String.Empty).Replace(" ", String.Empty).ToCharArray().Length;
        int maxYMapSize = levelZeroMapData.Length;

        mapSize = new Point(maxXMapSize, maxYMapSize);

        var worldStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < maxYMapSize; y++)
        {
            var newTiles = levelZeroMapData[y].Replace(" ", String.Empty).Split('|');
            var objectForTiles = levelOneMapData[y].Replace(" ", String.Empty).Split('|');

            for (int x = 0; x < maxXMapSize; x++)
            {
                var placedTile = PlaceTile(newTiles[x], x, y, worldStartPosition);

                //TODO Refactor
                if (objectForTiles[x].Replace(" ", "") != "0")
                {
                    var currentColumn = GetColumn(levelOneMapData, x);
                    var placedGameObject = PlaceObject(objectForTiles[x], x, y, worldStartPosition, placedTile, currentColumn);

                    if(placedGameObject != null)
                        placedTile.AttachObjectToTile(placedGameObject);
                }
            }
        }

        var maxTile = Tiles[new Point(maxXMapSize - 1, maxYMapSize - 1)].transform.position;
        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

        //SpawnPortals();
    }

    private List<string> GetColumn(string[] levelOneMapData, int columnIndex)
    {
        var column = new List<string>();

        foreach (var s in levelOneMapData)
        {
            var test = s.Split('|');
            column.Add(test[columnIndex]);
        }

        return column;
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

    private GameObject PlaceObject(string tileTypeName, int x, int y, Vector3 worldStartPosition, TileScript parentTileScript, List<string> levelOneColumnStrings)
    {
        var tileChar = tileTypeName[0];
        var tileNumber = tileTypeName.Substring(1, 2);
        var currentSpriteIndex = int.Parse(tileTypeName[tileTypeName.Length - 1].ToString());

        ITileObject tileObject = null;

        if (tileChar == 'R')
        {
            tileObject = MapObjectPool.Instance.RockObjects.FirstOrDefault(o => o.TileNumber == tileNumber);
        }
        else if (tileChar == 'W')
        {
            tileObject = MapObjectPool.Instance.WaterObjects.FirstOrDefault(o => o.TileNumber == tileNumber);
        }
        else if (tileChar == 'C')
        {
            tileObject = MapObjectPool.Instance.ChestObjects.FirstOrDefault(o => o.TileNumber == tileNumber);
        }
        else if (tileChar == 'H')
        {
            tileObject = MapObjectPool.Instance.HoleObjects.FirstOrDefault(o => o.TileNumber == tileNumber);
        }
        else if (tileChar == 'B')
        {
            tileObject = MapObjectPool.Instance.MapBorderObject.FirstOrDefault(o => o.TileNumber == tileNumber);
        }

        if (tileObject != null)
        {
            var currentPrefab = tileObject.Prefabs[currentSpriteIndex];

            var transformWorldPosition = new Vector3(worldStartPosition.x + TileSize * x, worldStartPosition.y - TileSize * y, 0);
            var createdTileObject = Instantiate(currentPrefab);
            createdTileObject.transform.position = transformWorldPosition;

            int sortingOrder = 0;
            if (tileChar == 'R')
            {
                sortingOrder = GetOrderInLayerForRockObject(levelOneColumnStrings, tileChar + tileNumber);
                parentTileScript.Setup(tileObject, false, false);
            }
            else if (tileChar == 'W')
            {
                sortingOrder = GetOrderInLayerForWaterObject(levelOneColumnStrings, tileTypeName);
                parentTileScript.Setup(tileObject, false, true);
            }
            else if (tileChar == 'C')
            {
                sortingOrder = ++parentTileScript.GetComponent<SpriteRenderer>().sortingOrder;
                parentTileScript.Setup(tileObject, false, true);
            }
            else if (tileChar == 'H' || tileChar == 'B')
            {
                sortingOrder = ++parentTileScript.GetComponent<SpriteRenderer>().sortingOrder;
                parentTileScript.Setup(tileObject, false, false);
            }

            createdTileObject.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
            return createdTileObject;
        }

        return null; // TODO ?
    }

    private int GetOrderInLayerForRockObject(List<string> levelOneColumnStrings, string currentObjectName)
    {
        var lastIndex = levelOneColumnStrings.ToList().FindLastIndex(x => x.Contains(currentObjectName));

        return ++lastIndex;
    }

    private int GetOrderInLayerForWaterObject(List<string> levelOneColumnStrings, string currentObjectName)
    {
        var lastIndex = levelOneColumnStrings.ToList().FindIndex(x => x.Contains(currentObjectName));
        lastIndex += 2;
        return lastIndex;
    }

    private TileScript PlaceTile(string tileTypeName, int x, int y, Vector3 worldStartPosition)
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

        var transformWorldPosition = new Vector3(worldStartPosition.x + TileSize * x, worldStartPosition.y - TileSize * y, 0);
        newTile.Setup(tilePosition, transformWorldPosition, mapParentTransform, tileType);

        return newTile;
    }

    private string[] ReadLevelZeroTextFile()
    {
        TextAsset bindData = Resources.Load("Level0-0") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, String.Empty);

        return data.Split('-');
    }

    private string[] ReadLevelOneTextFile()
    {
        TextAsset bindData = Resources.Load("Level0-1") as TextAsset;

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
