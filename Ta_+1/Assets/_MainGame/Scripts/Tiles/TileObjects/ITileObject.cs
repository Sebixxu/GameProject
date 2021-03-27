using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITileObject
{
    char TileChar { get; set; }
    string TileNumber { get; set; }
    GameObject[] Prefabs { get; set; }
}
