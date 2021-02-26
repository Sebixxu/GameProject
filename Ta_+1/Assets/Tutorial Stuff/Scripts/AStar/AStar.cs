using System;
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

    public static Stack<Node> GetPath(Point startPoint, Point goalPoint)
    {
        if (nodes == null)
        {
            CreateNodes();
        }

        HashSet<Node> openList = new HashSet<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        Stack<Node> finalPath = new Stack<Node>();

        Node currentNode = nodes[startPoint];

        openList.Add(currentNode);

        while (openList.Any())
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Point neighborPoint = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);

                    if (LevelManager.Instance.InBounds(neighborPoint) && LevelManager.Instance.Tiles[neighborPoint].Walkable && neighborPoint != currentNode.GridPosition)
                    {
                        int gCost;
                        if (Math.Abs(x - y) == 1)
                            gCost = 10;
                        else
                        {
                            if (!IsConnectedDiagonally(currentNode, nodes[neighborPoint]))
                            {
                                continue;
                                
                            }

                            gCost = 14;

                        }

                        Node neighbor = nodes[neighborPoint];

                        if (openList.Contains(neighbor))
                        {
                            if (currentNode.G + gCost < neighbor.G)
                            {
                                neighbor.CalcValues(currentNode, nodes[goalPoint], gCost);
                            }
                        }
                        else if (!closedList.Contains(neighbor))
                        {
                            openList.Add(neighbor);
                            neighbor.CalcValues(currentNode, nodes[goalPoint], gCost);
                        }
                    }
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (openList.Any())
            {
                currentNode = openList.OrderBy(x => x.F).First();
            }

            if (currentNode == nodes[goalPoint])
            {
                while (currentNode.GridPosition != startPoint)
                {
                    finalPath.Push(currentNode);
                    currentNode = currentNode.Parent;
                }

                break;
            }
        }

        //GameObject.Find("Debug").GetComponent<AStarDebug>().DebugPath(openList, closedList, finalPath);

        return finalPath;
    }

    private static bool IsConnectedDiagonally(Node currentNode, Node neighborNode)
    {
        Point direction = neighborNode.GridPosition - currentNode.GridPosition;

        Point first = new Point(currentNode.GridPosition.X + direction.X, currentNode.GridPosition.Y);

        Point second = new Point(currentNode.GridPosition.X, currentNode.GridPosition.Y + direction.Y);

        if (LevelManager.Instance.InBounds(first) && !LevelManager.Instance.Tiles[first].Walkable)
        {
            return false;
        }

        if (LevelManager.Instance.InBounds(second) && !LevelManager.Instance.Tiles[second].Walkable)
        {
            return false;
        }

        return true;
    }
}
