using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawner : Singleton<MonsterSpawner>
{
    public List<MonsterWaveModel> MonsterWaves => _monsterWaves;

    [SerializeField]
    [Tooltip("Min time before any monster will spawn (in second).")]
    private float minPreSpawnTime;

    [SerializeField]
    [Tooltip("Max time before any monster will spawn (in second).")]
    private float maxPreSpawnTime;

    [SerializeField]
    [Tooltip("Min time between monster spawn in second.")]
    private float minSecondBetweenMonsters;

    [SerializeField]
    [Tooltip("Max time between monster spawn in second.")]
    private float maxSecondBetweenMonsters;

    private readonly List<MonsterWaveModel> _monsterWaves;
    private List<MonsterProbabilityOnLevelModel> _monsterProbabilityOnLevel;
    private bool _spawningProcessStarted;

    public MonsterSpawner(List<MonsterWaveModel> monsterWaves)
    {
        _monsterWaves = monsterWaves;
    }

    // Start is called before the first frame update
    void Start()
    {
        _monsterProbabilityOnLevel = MonsterProbabilityPool.Instance.MonsterProbabilityOnLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_spawningProcessStarted)
            StartCoroutine(SpawnMonster());
    }

    private IEnumerator SpawnMonster()
    {
        _spawningProcessStarted = true;

        var preSpawnTime = Random.Range(minPreSpawnTime, maxPreSpawnTime);
        Debug.Log($"[MonsterSpawner] Pre Spawn Time set to: {preSpawnTime}.");
        yield return new WaitForSeconds(preSpawnTime);

        var monsterSpawnTiles = LevelManager.Instance.Tiles.Values.Where(x => x.TileType == TileType.MonsterSpawn).ToList();
        var goalTile = LevelManager.Instance.Tiles.Values.First(x => x.TileType == TileType.PlayerSpawn);

        var monsterProbabilityOnLevel = _monsterProbabilityOnLevel.FirstOrDefault(x => x.level == LevelManager.Instance.currentLevel);

        if (monsterProbabilityOnLevel == null)
        {
            Debug.LogError($"[MonsterSpawner] There is no Monster Probability defined for this level: {LevelManager.Instance.currentLevel}.", this);
            yield break;
        }

        var monsterCount = Random.Range(monsterProbabilityOnLevel.minMonsterCount,
            monsterProbabilityOnLevel.maxMonsterCount);

        Debug.Log($"[MonsterSpawner] Monster count set to: {monsterCount}");
        for (int i = 0; i < monsterCount; i++)
        {
            var getMonsterSpawnTileIndex = Random.Range(0, monsterSpawnTiles.Count);
            var monsterSpawnTile = monsterSpawnTiles[getMonsterSpawnTileIndex];

            var currentProbabilityMonsters = monsterProbabilityOnLevel.monsterProbabilityModels;

            float randomValue = Random.Range(0.0f, 0.99f);
            var monster = currentProbabilityMonsters.FirstOrDefault(x => randomValue >= x.minProbabilityRange && randomValue < x.maxProbabilityRange)?.monster;

            if (monster == null)
            {
                Debug.LogError($"[MonsterSpawner] There is no Monster defined for received probability: {randomValue} on level: {LevelManager.Instance.currentLevel}.");
                yield break;
            }

            var monsterFromObjectPool = ObjectPool.Instance.GetObject(monster.name).GetComponent<Monster>();
            Debug.Log($"[MonsterSpawner] Spawning monster: {monsterFromObjectPool.name} on " +
                      $"({monsterSpawnTile.GridPosition.X}, {monsterSpawnTile.GridPosition.Y}) grid position tile.");
            monsterFromObjectPool.Spawn(monsterSpawnTile.WorldPosition);
            monsterFromObjectPool.GenerateAndSetPath(monsterSpawnTile, goalTile);

            var secondBetweenMonsters = Random.Range(minSecondBetweenMonsters, maxSecondBetweenMonsters);
            Debug.Log($"[MonsterSpawner] Second between monster set to: {secondBetweenMonsters}");
            yield return new WaitForSeconds(secondBetweenMonsters);
        }
    }
}
