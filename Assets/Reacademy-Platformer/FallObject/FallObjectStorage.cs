using System.Collections.Generic;
using UnityEngine;

namespace FallObject
{
    public class FallObjectStorage
    {
        private Dictionary<int, FallObjectView> _storage = new Dictionary<int, FallObjectView>();

        public void Add(FallObjectView fallObjectView)
        {
            if (fallObjectView == null)
            {
                Debug.Log("[FallObjectStorage.Add] Fall object is null");
                return;
            }
            _storage.Add(fallObjectView.GetInstanceID(), fallObjectView);
        }

        public FallObjectView Get(int index)
        {
            if (_storage.TryGetValue(index, out var value))
            {
                return value;
            }
        
            Debug.Log("[FallObjectStorage.Get] Failed to get value by that id. Try another id.");
            return null;
        }

        public void Delete(int index)
        {
            if (_storage.Remove(index))
            {
                return;
            }
        
            Debug.Log("[FallObjectStorage.Delete] Failed to delete value by that id.Try another id.");
        }
    }
}