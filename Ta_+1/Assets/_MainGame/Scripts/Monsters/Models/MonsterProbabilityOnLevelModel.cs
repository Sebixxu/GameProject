using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterProbabilityOnLevelModel
{
    public int level;
    public int minMonsterCount;
    public int maxMonsterCount;
    public List<MonsterProbabilityModel> monsterProbabilityModels;
}
