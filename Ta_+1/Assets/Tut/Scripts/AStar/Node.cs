using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Point GridPosition { get; private set; }
    public TileScript TileScript { get; private set; }
    public Node Parent { get; private set; }

    public Node(TileScript tileScript)
    {
        GridPosition = tileScript.GridPosition;
        TileScript = tileScript;
    }

    public void CalcValues(Node parent)
    {
        Parent = parent;
    }
}
