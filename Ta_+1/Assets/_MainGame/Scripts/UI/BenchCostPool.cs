using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenchCostPool : Singleton<BenchCostPool>
{
    [SerializeField]
    private TowerCostModel[] towerCostModels;

    public TowerCostModel[] TowerCostModels
    {
        get { return towerCostModels; }
        set { towerCostModels = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
