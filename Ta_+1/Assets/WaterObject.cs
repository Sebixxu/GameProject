using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObject : MonoBehaviour, ITileObject
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [SerializeField]
    private char tileChar = 'W';

    public char TileChar
    {
        get { return tileChar; }
        set { tileChar = value; }
    }

    [SerializeField]
    private string tileNumber;

    public string TileNumber
    {
        get { return tileNumber; }
        set { tileNumber = value; }
    }

    //public int TileNumber { get; set; }

    [SerializeField]
    private GameObject[] prefabs;

    public GameObject[] Prefabs
    {
        get { return prefabs; }
        set { prefabs = value; }
    }
}
