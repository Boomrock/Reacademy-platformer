using System;
using UnityEngine;

namespace TickableManager
{
    public class TickableManager : MonoBehaviour
    {
        public static event Action UpdateNotify;
        public static event Action FixedUpdateNotify;
    
        private void Update()
        {
            UpdateNotify?.Invoke();
        }
    
        private void FixedUpdate()
        {
            FixedUpdateNotify?.Invoke();
        }
    }
}