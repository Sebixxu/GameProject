using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tut.Scripts.Towers.StandardTowers;
using UnityEngine;

public class BenchTowerPool : Singleton<BenchTowerPool>
{
    private const int BenchSize = 7;

    [SerializeField]
    private Tower[] _chosenTowers;

    private Dictionary<int, Tower> _benchTowerPoolDictionary = new Dictionary<int, Tower>(BenchSize);

    public Dictionary<int, Tower> BenchTowerPoolDictionary
    {
        get { return _benchTowerPoolDictionary; }
        set { _benchTowerPoolDictionary = value; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        foreach (var chosenTower in _chosenTowers.Select((value, i) => new { i, value }))
        {
            _benchTowerPoolDictionary.Add(chosenTower.i, chosenTower.value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
