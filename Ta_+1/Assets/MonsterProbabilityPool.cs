using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProbabilityPool : Singleton<MonsterProbabilityPool>
{
    public List<MonsterProbabilityOnLevelModel> MonsterProbabilityOnLevel
    {
        get { return _monsterProbabilityOnLevel; }
        set { _monsterProbabilityOnLevel = value; }
    }

    [SerializeField]
    private List<MonsterProbabilityOnLevelModel> _monsterProbabilityOnLevel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
