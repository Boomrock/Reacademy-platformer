using System;
using FallObject;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class FallObjectSpawner : ITickable
{
    public FallObjectPool Pool => _pool;
    
    private readonly FallObjectPool _pool;
    private readonly float _spawnPeriodMin;
    private readonly float _spawnPeriodMax;
    private readonly float _minPositionX;
    private readonly float _maxPositionX;
    private readonly float _positionY;
    private readonly float _delayStartSpawn;
    private Vector3 _spawnPosition;
    private float _spawnPeriod;
    private float _timer;
    private int _typesCount;
    private bool _gameIsOn;

    public FallObjectSpawner(FallObjectPool pool)
    {
        var spawnerConfig = Resources.Load<FallObjectSpawnConfig>(ResourcesConst.FallObjectSpawnConfig);
        _positionY = spawnerConfig.PositionY;
        _minPositionX = spawnerConfig.MinPositionX;
        _maxPositionX = spawnerConfig.MaxPositionX;
        _spawnPeriodMin = spawnerConfig.SpawnPeriodMin;
        _spawnPeriodMax = spawnerConfig.SpawnPeriodMax;
        _delayStartSpawn = spawnerConfig.DelayStartSpawn;
        _spawnPosition = new Vector2(Random.Range(_minPositionX, _maxPositionX), _positionY);

        _pool = pool;
        _spawnPeriod = Random.Range(_spawnPeriodMin, _spawnPeriodMax);
        _typesCount = Enum.GetValues(typeof(FallObjectType)).Length;
    }

    public void StartSpawn()
    {
        _spawnPeriod = 6.5f;
        _gameIsOn = true;
    }

    public void StopSpawn()
    {
        _gameIsOn = false;
        Pool.Clear();
    }
    private void SpawnNewObject()
    {
        var type = Random.Range(0, _typesCount);
        var newObject = _pool.Spawn((FallObjectType)type);
        _spawnPosition.x = Random.Range(_minPositionX, _maxPositionX);
        newObject.View.gameObject.transform.position = _spawnPosition;
    }

    public void Tick()
    {
        if (!_gameIsOn) { return; }
        
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