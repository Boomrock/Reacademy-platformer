using System.Collections.Generic;
using UnityEngine;

namespace FallObject
{
    public class FallObjectStorage
    {
        private Dictionary<FallObjectView, FallObjectController> _storage =
            new Dictionary<FallObjectView, FallObjectController>();

        public void Add(FallObjectView fallObjectView, FallObjectController fallObjectController)
        {
            if (fallObjectView == null)
            {
                Debug.Log("[FallObjectStorage.Add] Fall object is null");
                return;
            }

            _storage.Add(fallObjectView, fallObjectController);
        }

        public FallObjectController Get(FallObjectView fallObjectView)
        {
            if (_storage.TryGetValue(fallObjectView, out var value))
            {
                return value;
            }

            return null;
        }

        public void Delete(FallObjectView fallObjectView)
        {
            if (_storage.Remove(fallObjectView))
            {
                return;
            }

            Debug.Log("[FallObjectStorage.Delete] Failed to delete value by that id.Try another id.");
        }
    }
}