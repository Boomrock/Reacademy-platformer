using System;
using FallObject;
using Sounds;
using UnityEngine;

public class ScoreCounter
{
    public event Action<int> ScoreChangeNotify;
    public int Score => _score;

    private SoundController _soundController;

    private int _score = 0;

    public ScoreCounter(SoundController soundController)
    {
        _soundController = soundController;
    }
    
    public void PlayerCatchFallObjectEventHandler(FallObjectController controller)
    {
        _soundController.Play(SoundName.Buff1);
        _score += controller.PointsPerObject;
        ScoreChangeNotify?.Invoke(_score);
    }        

    public void ObjectFellEventHandler(FallObjectController controller)
    {
        _soundController.Play(SoundName.GetDamage);
        
        _score -= controller.Damage;
        ScoreChangeNotify?.Invoke(controller.Damage);
    }
}