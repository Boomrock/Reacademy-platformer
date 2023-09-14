using System;
using System.Collections.Generic;
using FallObject;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class FallObjectSpawner : ITickable
{
    public FallObjectView.Pool  Pool => _pool;
    private Dictionary<FallObjectView, FallObjectController> _fallObjectControllers;
    private readonly TickableManager _tickableManager;
    private readonly ScoreCounter _scoreCounter;
    private readonly FallObjectView.Pool  _pool;
    private readonly float _delayStartSpawn;
    private readonly float _spawnPeriodMin;
    private readonly float _spawnPeriodMax;
    private readonly float _minPositionX;
    private readonly float _maxPositionX;
    private readonly int _typesCount;
    private Vector3 _spawnPosition;
    private float _spawnPeriod;
    private float _timer;
    private FallObjectStorage _fallObjectStorage;
    private readonly FallObjectConfig _objectConfig;

    public FallObjectSpawner(FallObjectView.Pool pool, TickableManager tickableManager, FallObjectStorage fallObjectStorage, FallObjectConfig objectConfig)
    {
        var spawnerConfig = Resources.Load<FallObjectSpawnConfig>(ResourcesConst.FallObjectSpawnConfig);
        _delayStartSpawn = spawnerConfig.DelayStartSpawn;
        _spawnPeriodMax = spawnerConfig.SpawnPeriodMax;
        _spawnPeriodMin = spawnerConfig.SpawnPeriodMin;
        _minPositionX = spawnerConfig.MinPositionX;
        _maxPositionX = spawnerConfig.MaxPositionX;
        _spawnPosition = new Vector2(Random.Range(_minPositionX, _maxPositionX), spawnerConfig.PositionY);

        _pool = pool;
        _tickableManager = tickableManager;
        _fallObjectStorage = fallObjectStorage;
        _objectConfig = objectConfig;
        _typesCount = Enum.GetValues(typeof(FallObjectType)).Length;
        _spawnPeriod = Random.Range(_spawnPeriodMin, _spawnPeriodMax);
    }

    public void StartSpawn()
    {
        _spawnPeriod = 6.5f;
        _tickableManager.Add(this);
    }

    public void StopSpawn()
    {
        _tickableManager.Remove(this);
    }
    private void SpawnNewObject()
    {
        var type = Random.Range(0, _typesCount);
        var fallObjectView = _pool.Spawn((FallObjectType)type);
        var fallObjectController = _fallObjectStorage.Get(fallObjectView);
        if (fallObjectController == null)
        {
            fallObjectController = new FallObjectController(fallObjectView, _objectConfig.Get((FallObjectType)type), _tickableManager);
            fallObjectController.PlayerCatchFallingObjectNotify += (FallObjectController _) => _pool.Despawn(fallObjectView);
            _fallObjectStorage.Add(fallObjectView, fallObjectController);
        }
        fallObjectView.gameObject.SetActive(true);
        _spawnPosition.x = Random.Range(_minPositionX, _maxPositionX);
        fallObjectView.transform.position = _spawnPosition;
    }


    public void Tick()
    {
        _spawnPeriod -= Time.deltaTime;
        _timer += Time.deltaTime;
        
        if (_timer > _delayStartSpawn)
        {
            if (_spawnPeriod <= 0)
            {
                SpawnNewObject();
                _spawnPeriod = Random.Range(_spawnPeriodMin, _spawnPeriodMax);
            }
        }
    }
}