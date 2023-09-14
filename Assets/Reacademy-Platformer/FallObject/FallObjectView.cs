using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace FallObject
{
    public class FallObjectView : MonoBehaviour
    {

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        
        public event Action<Collision2D> OnCollisionEnter2DNotify;
        public event Action<bool> OnActiveNotify;


        private void OnEnable()
        {
            OnActiveNotify?.Invoke(true);
        }

        private void OnDisable()
        {
            OnActiveNotify?.Invoke(false);
        }

        [SerializeField] private SpriteRenderer _spriteRenderer;
        private void Reinit(Sprite sprite)
        {   
            _spriteRenderer.sprite = sprite;
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            OnCollisionEnter2DNotify?.Invoke(other);
        }
        public class Pool : MemoryPool<FallObjectType, FallObjectView>
        {
            private readonly FallObjectConfig _objectConfig;

            public Pool(FallObjectConfig objectConfig)
            {
                _objectConfig = objectConfig;
            }
            protected override void OnDespawned(FallObjectView item)
            {
                item.GameObject().SetActive(false);
            }

            protected override void Reinitialize(FallObjectType type, FallObjectView fallObjectView)
            {
                fallObjectView.Reinit(_objectConfig.Get(type).ObjectSprite);
                fallObjectView.GameObject().SetActive(false);
            }
        }


    }
}