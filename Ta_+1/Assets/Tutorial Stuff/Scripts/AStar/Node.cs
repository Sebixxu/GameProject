using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Point GridPosition { get; private set; }
    public Vector2 WorldPosition { get; private set; }
    public TileScript TileScript { get; private set; }
    public Node Parent { get; private set; }
    public int G { get; set; }
    public int F { get; set; }
    public int H { get; set; }

    public Node(TileScript tileScript)
    {
        GridPosition = tileScript.GridPosition;
        TileScript = tileScript;
        WorldPosition = tileScript.WorldPosition;
    }

    public void CalcValues(Node parent,  Node goal, int gCost)
    {
        Parent = parent;

        G = Parent.G + gCost;
        H = (Math.Abs(GridPosition.X - goal.GridPosition.X) + Math.Abs(GridPosition.Y - goal.GridPosition.Y)) * 10;
        F = G + H;
    }
}
