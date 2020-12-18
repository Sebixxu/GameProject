using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;

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
        var tileSpriteRenderer = tile.GetComponent<SpriteRenderer>();
        var bounds = tileSpriteRenderer.bounds;

        float tileSizeX = bounds.size.x, tileSizeY = bounds.size.y;

        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                GameObject newTile = Instantiate(tile);
                newTile.transform.position = new Vector3(tileSizeX * x, tileSizeY * y);
                //Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
