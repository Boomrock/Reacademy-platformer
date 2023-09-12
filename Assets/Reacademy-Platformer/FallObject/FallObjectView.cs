using System;
using UnityEngine;
using Zenject;

namespace FallObject
{
    public class FallObjectView : MonoBehaviour
    {

        public SpriteRenderer SpriteRenderer => spriteRenderer;
        
        public event Action<Collision2D> OnCollisionEnter2DNotify; 
        
        [SerializeField] private SpriteRenderer spriteRenderer;
        private void Reinit(SpriteRenderer spriteRenderer1)
        {
            throw new NotImplementedException();
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            OnCollisionEnter2DNotify?.Invoke(other);
        }
        public class Pool : MemoryPool<SpriteRenderer, FallObjectView>
        {
            protected override void OnCreated(FallObjectView item)
            {
                // Called immediately after the item is first added to the pool
            }

            protected override void OnDestroyed(FallObjectView item)
            {
                // Called immediately after the item is removed from the pool without also being spawned
                // This occurs when the pool is shrunk either by using WithMaxSize or by explicitly shrinking the pool by calling the `ShrinkBy` / `Resize methods
            }

            protected override void OnSpawned(FallObjectView item)
            {
                // Called immediately after the item is removed from the pool
            }

            protected override void OnDespawned(FallObjectView item)
            {
                // Called immediately after the item is returned to the pool
            }

            protected override void Reinitialize(SpriteRenderer s, FallObjectView fallObjectView)
            {
                // Similar to OnSpawned
                // Called immediately after the item is removed from the pool
                // This method will also contain any parameters that are passed along
                // to the memory pool from the spawning code
                fallObjectView.Reinit(s);
            }
        }


    }
}