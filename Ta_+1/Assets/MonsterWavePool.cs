using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasa dostępna z zewnątrz, tutaj ustawiam co chcemy spawnować i tylko o to w niej chodzi
//Spawner natomiast zajmuje się tylko wykonywaniem spawnowania nie modyfikujemy tam nic
public class MonsterWavePool : Singleton<MonsterWavePool>
{
    public List<MonsterWaveModel> MonsterWaveModels
    {
        get { return monsterWaveModels; }
        set { monsterWaveModels = value; }
    }

    [SerializeField]
    private List<MonsterWaveModel> monsterWaveModels;
}
