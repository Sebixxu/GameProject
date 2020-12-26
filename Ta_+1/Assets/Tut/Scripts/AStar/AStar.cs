using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AStar
{
    private static Dictionary<Point, Node> nodes;

    private static void CreateNodes()
    {
        nodes = new Dictionary<Point, Node>();

        foreach (var tile in LevelManager.Instance.Tiles.Values)
        {
            nodes.Add(tile.GridPosition, new Node(tile));
        }
    }

    public static void GetPath(Point startPoint)
    {
        if (nodes == null)
        {
            CreateNodes();
        }

        HashSet<Node> openList = new HashSet<Node>();
        Node currentNode = nodes[startPoint];

        openList.Add(currentNode);

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Point neighborPoint = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);

                if (LevelManager.Instance.InBounds(neighborPoint) && LevelManager.Instance.Tiles[neighborPoint].Walkable  && neighborPoint != currentNode.GridPosition)
                {
                    Node neighbor = nodes[neighborPoint];

                    if (!openList.Contains(neighbor))
                        openList.Add(neighbor);

                    neighbor.CalcValues(currentNode);
                }
            }
        }

        GameObject.Find("Debug").GetComponent<AStarDebug>().DebugPath(openList);
    }
}
