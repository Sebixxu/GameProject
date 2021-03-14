using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBenchManager : Singleton<TowerBenchManager> //OreStorage?
{
    [SerializeField]
    private int towerBenchElementsCount => _towersDictionary.Count;

    [SerializeField]
    private Transform towerBenchParentTransform;

    [SerializeField]
    private Dictionary<int, Tower> _towersDictionary;

    [SerializeField]
    private GameObject[] _towersPool;

    [SerializeField]
    private TowerBenchSlot[] _towerBenchSlots;

    public Dictionary<int, Tower> TowersDictionary
    {
        get { return _towersDictionary; }
        set { _towersDictionary = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        //_towersDictionary.Add(0, _towersPool[0] as Tower);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}