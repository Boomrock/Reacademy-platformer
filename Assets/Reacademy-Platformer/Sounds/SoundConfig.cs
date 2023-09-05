using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "Configs/SoundConfig", order = 1)]
    public class SoundConfig : ScriptableObject
    {
        [SerializeField] private SoundModels[] soundModels;
        
        private Dictionary<SoundName, AudioClip> _dict = new();
        
        [NonSerialized] private bool _inited;

        private void Init()
        {
            foreach (var model in soundModels)
            {
                _dict.Add(model.Name, model.Clip);
            }
            _inited = true;
        }

        public AudioClip Get(SoundName soundName)
        {
            if (!_inited)
            {
                Init();
            }
            
            if (!_dict.ContainsKey(soundName))
            {
                Debug.LogWarning($"Sound named {soundName} not found.");
            }
            
            return _dict[soundName];
        }
    }

    public enum SoundName
    {
        StartGame,
        BackStart,
        BackMain,
        GameOver,
        Restart,
        GetDamage,
        Buff1,
        Buff2,
        Debuff1,
        Debuff2
    }

    [Serializable]
    public struct SoundModels
    {
        public SoundName Name;
        public AudioClip Clip;
    }
}