using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TowerBenchUI : MonoBehaviour
{
    private Dictionary<int, Tower> _benchTowerPoolDictionary;
    private TowerBenchSlot[] _towerBenchSlots;

    // Start is called before the first frame update
    void Start()
    {
        _benchTowerPoolDictionary = BenchTowerPool.Instance.BenchTowerPoolDictionary;

        _towerBenchSlots = transform.GetComponentsInChildren<TowerBenchSlot>();

        LoadBenchUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadBenchUI()
    {
        Debug.Log("Updating Bench UI.");

        foreach (var tower in _benchTowerPoolDictionary.Select((value, i) => new { i, value }))
        {
            var sprite = tower.value.Value.GetComponent<SpriteRenderer>().sprite;

            _towerBenchSlots[tower.i].SetActive(sprite, tower.value.Value);
        }
    }
}
