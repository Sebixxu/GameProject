using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebug : MonoBehaviour
{
    [SerializeField]
    private TileScript startTileScript, goalTileScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClickTile();

        if(Input.GetKeyDown(KeyCode.Space))
            AStar.GetPath(startTileScript.GridPosition);
    }

    private void ClickTile()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                TileScript tmp = hit.collider.GetComponent<TileScript>();

                if (tmp != null)
                {
                    if (startTileScript == null)
                    {
                        startTileScript = tmp;
                        startTileScript.SpriteRenderer.color = Color.magenta;
                        startTileScript.DebugOn = true;
                    }
                    else if (goalTileScript == null)
                    {
                        goalTileScript = tmp;
                        goalTileScript.SpriteRenderer.color = Color.blue;
                        goalTileScript.DebugOn = true;
                    }
                }
            }
        }
    }

    public void DebugPath(HashSet<Node> openList)
    {
        foreach (var node in openList)
        {
            node.TileScript.SpriteRenderer.color = Color.black;
        }
    }
}
