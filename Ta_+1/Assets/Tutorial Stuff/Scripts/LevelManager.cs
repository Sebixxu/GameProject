using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Portal BluePortal { get; set; }
    public Dictionary<Point, TileScript> Tiles { get; set; }
    public float TileSize => tilePrefabs[0].GetComponent<SpriteRenderer>().bounds.size.x;

    public Stack<Node> Path
    {
        get
        {
            if(path == null)
                GeneratePath();

            return new Stack<Node>(new Stack<Node>(path));
        }
        //set { path = value; }
    }


    [SerializeField]
    private GameObject[] tilePrefabs;
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
    private Point _blueSpawnPoint;
    private Point _redSpawnPoint;

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
        mapSize = new Point(maxXMapSize, maxYMapSize);

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

        SpawnPortals();
    }

    private void SpawnPortals()
    {
        _blueSpawnPoint = new Point(0,0);
        GameObject bluePortal =
            Instantiate(bluePortalPrefab, Tiles[_blueSpawnPoint].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        BluePortal = bluePortal.GetComponent<Portal>();
        BluePortal.name = "BluePortal";

        _redSpawnPoint = new Point(11, 6);
        Instantiate(redPortalPrefab, Tiles[_redSpawnPoint].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }

    public bool InBounds(Point position)
    {
        return position.X >= 0 && position.Y >= 0 && position.X < mapSize.X && position.Y < mapSize.Y;
    }

    private void PlaceTile(char tileType, int x, int y, Vector3 worldStartPosition)
    {
        int tileIndex = int.Parse(tileType.ToString());

        var tilePosition = new Point(x, y);
        var newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        newTile.Setup(tilePosition, new Vector3(worldStartPosition.x + TileSize * x, worldStartPosition.y - TileSize * y, 0), mapParentTransform);
    }

    private string[] ReadLevelTextFile()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, String.Empty);

        return data.Split('-');
    }

    public void GeneratePath()
    {
        path = AStar.GetPath(_blueSpawnPoint, _redSpawnPoint);
    }
}
