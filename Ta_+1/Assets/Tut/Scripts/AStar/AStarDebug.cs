using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebug : MonoBehaviour
{
    [SerializeField]
    private TileScript startTileScript, goalTileScript;

    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private GameObject debugTilePrefab;
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

                        CreateDebugTile(startTileScript.WorldPosition, Color.magenta);
                        startTileScript.DebugOn = true;
                    }
                    else if (goalTileScript == null)
                    {
                        goalTileScript = tmp;

                        CreateDebugTile(goalTileScript.WorldPosition, Color.blue);
                        goalTileScript.DebugOn = true;
                    }
                }
            }
        }
    }

    public void DebugPath(HashSet<Node> openList, HashSet<Node> closedList)
    {
        foreach (var node in openList)
        {
            if(node.TileScript != startTileScript)
            {
                CreateDebugTile(node.TileScript.WorldPosition, Color.cyan);
            }

            PointToParent(node, node.TileScript.WorldPosition);
        }

        foreach (var node in closedList)
        {
            if (node.TileScript != startTileScript && node.TileScript != goalTileScript)
            {
                CreateDebugTile(node.TileScript.WorldPosition, Color.yellow);
            }

            PointToParent(node, node.TileScript.WorldPosition);
        }
    }

    private void PointToParent(Node node, Vector2 position)
    {
        if(node.Parent == null)
            return;
        
        GameObject arrow = Instantiate(arrowPrefab, position, Quaternion.identity);
        arrow.GetComponent<SpriteRenderer>().sortingOrder = 5;

        //Left
        if (node.GridPosition.X < node.Parent.GridPosition.X && node.GridPosition.Y == node.Parent.GridPosition.Y)
        {
            arrow.transform.eulerAngles = new Vector3(0,0,0);
        }
        //Bottom Left
        else if (node.GridPosition.X < node.Parent.GridPosition.X && node.GridPosition.Y > node.Parent.GridPosition.Y)
        {
            arrow.transform.eulerAngles = new Vector3(0, 0, 45);
        }
        //Bottom
        else if (node.GridPosition.X == node.Parent.GridPosition.X && node.GridPosition.Y > node.Parent.GridPosition.Y)
        {
            arrow.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        //Bottom right
        else if (node.GridPosition.X > node.Parent.GridPosition.X && node.GridPosition.Y > node.Parent.GridPosition.Y)
        {
            arrow.transform.eulerAngles = new Vector3(0, 0, 135);
        }
        //Right
        else if (node.GridPosition.X > node.Parent.GridPosition.X && node.GridPosition.Y == node.Parent.GridPosition.Y)
        {
            arrow.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        //Top right
        else if (node.GridPosition.X > node.Parent.GridPosition.X && node.GridPosition.Y < node.Parent.GridPosition.Y)
        {
            arrow.transform.eulerAngles = new Vector3(0, 0, 225);
        }
        //Top
        else if (node.GridPosition.X == node.Parent.GridPosition.X && node.GridPosition.Y < node.Parent.GridPosition.Y)
        {
            arrow.transform.eulerAngles = new Vector3(0, 0, 270);
        }
        //Top left
        else if (node.GridPosition.X < node.Parent.GridPosition.X && node.GridPosition.Y < node.Parent.GridPosition.Y)
        {
            arrow.transform.eulerAngles = new Vector3(0, 0, 315);
        }
    }

    private void CreateDebugTile(Vector3 worldPosition, Color32 color)
    {
        GameObject debugTile = Instantiate(debugTilePrefab, worldPosition, Quaternion.identity);

        debugTile.GetComponent<SpriteRenderer>().color = color;
    }
}
