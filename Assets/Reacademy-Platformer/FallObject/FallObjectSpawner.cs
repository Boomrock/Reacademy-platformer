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

    public FallObjectSpawner(FallObjectView.Pool pool, TickableManager tickableManager, FallObjectStorage fallObjectStorage)
    {
        var spawnerConfig = Resources.Load<FallObjectSpawnConfig>(ResourcesConst.FallObjectSpawnConfig);
        _delayStartSpawn = spawnerConfig.DelayStartSpawn;
        _spawnPeriodMax = spawnerConfig.SpawnPeriodMax;
        _spawnPeriodMin = spawnerConfig.SpawnPeriodMin;
        var positionY = spawnerConfig.PositionY;
        _minPositionX = spawnerConfig.MinPositionX;
        _maxPositionX = spawnerConfig.MaxPositionX;
        _spawnPosition = new Vector2(Random.Range(_minPositionX, _maxPositionX), positionY);

        _pool = pool;
        _tickableManager = tickableManager;
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
        var newObject = _pool.Spawn((FallObjectType)type);

        _spawnPosition.x = Random.Range(_minPositionX, _maxPositionX);
        newObject.transform.position = _spawnPosition;
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