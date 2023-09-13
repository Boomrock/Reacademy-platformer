using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace FallObject
{
    public class FallObjectView : MonoBehaviour
    {

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        
        public event Action<Collision2D> OnCollisionEnter2DNotify;
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
            private readonly FallObjectStorage _fallObjectStorage;
            public FallObjectConfig ObjectConfig = Resources.Load<FallObjectConfig>(ResourcesConst.FallObjectConfigPath);
            protected override void OnDespawned(FallObjectView item)
            {
                _fallObjectStorage.Get(item).SetActive(false);
            }

            protected override void Reinitialize(FallObjectType type, FallObjectView fallObjectView)
            {
                var fallObjectController = _fallObjectStorage.Get(fallObjectView);
                if (fallObjectController == null)
                {
                    fallObjectController = new FallObjectController(fallObjectView, ObjectConfig.Get(type));
                    _fallObjectStorage.Add(fallObjectView,fallObjectController);
                }
                fallObjectView.Reinit(ObjectConfig.Get(type).ObjectSprite);
                fallObjectController.SetActive(true);
            }
        }


    }
}