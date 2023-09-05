using System;
using FallObject;
using Sounds;
using UnityEngine;

public class PlayerHpController
{
    public Action<float> OnHealthChanged;

    public Action OnZeroHealth;

    private SoundController _soundController;
    
    private float _health;
        
    public PlayerHpController(float health, SoundController soundController)
    {
        _health = health;
        _soundController = soundController;
        
        FallObjectController.DamageToPlayerNotify += ReduceHealth;
    }

    public void ReduceHealth(float damage)
    {
        _health -= damage;
        
        _soundController.Play(SoundName.GetDamage);
        OnHealthChanged?.Invoke(_health);
        
        if (_health <= 0)
        {
            OnZeroHealth?.Invoke();
        }
    }
}