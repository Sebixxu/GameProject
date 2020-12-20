using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private int spawnXCord;

    [SerializeField]
    private int spawnYCord;

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
        var spawnWorldPosition = _levelManager.Tiles[new Point(spawnXCord, spawnYCord)].GetComponent<TileScript>().WorldPosition;

        Instantiate(playerPrefab, spawnWorldPosition, Quaternion.identity);
    }
}
