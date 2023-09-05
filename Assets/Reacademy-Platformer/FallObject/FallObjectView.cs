using System;
using UnityEngine;

namespace FallObject
{
    public class FallObjectView : MonoBehaviour
    {

        public SpriteRenderer SpriteRenderer => spriteRenderer;
        
        public event Action<Collision2D> OnCollisionEnter2DNotify; 
        
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnCollisionEnter2DNotify?.Invoke(other);
        }
    }
}