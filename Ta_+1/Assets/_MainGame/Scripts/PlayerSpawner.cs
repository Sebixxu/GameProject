using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private int? spawnXCord;

    [SerializeField]
    private int? spawnYCord;

    [SerializeField]
    private GameObject playerPrefab;

    private LevelManager _levelManager;

    // Start is called before the first frame update
    void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnPlayer()
    {
        //TODO Bez sensu to troche jest napisane, przepisać.
        if (spawnXCord == null || spawnYCord == null)
        {
            var tile = _levelManager.Tiles.FirstOrDefault(x => x.Value.TileType == TileType.PlayerSpawn);
            
            spawnXCord = tile.Key.X;
            spawnYCord = tile.Key.Y;
        }

        var spawnWorldPosition = _levelManager.Tiles[new Point((int)spawnXCord, (int)spawnYCord)].GetComponent<TileScript>().WorldPosition;
        var player = Instantiate(playerPrefab, spawnWorldPosition, Quaternion.identity);
        player.SetActive(true);
    }
}
