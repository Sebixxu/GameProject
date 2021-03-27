using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectPool : Singleton<MapObjectPool>
{
    [SerializeField]
    private RockObject[] rockObjects;

    public RockObject[] RockObjects => rockObjects;

    [SerializeField]
    private WaterObject[] waterObjects;

    public WaterObject[] WaterObjects => waterObjects;

    [SerializeField]
    private ChestObject[] chestObjects;

    public ChestObject[] ChestObjects => chestObjects;

    [SerializeField]
    private HoleObject[] holeObjects;

    public HoleObject[] HoleObjects => holeObjects;

    //[SerializeField] //Sadge że Unity tego nie wspiera
    //private ITileObject[] tileObjects;

    //public ITileObject[] TileObjects => tileObjects; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
