using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SceneObjectStorage
{
    public class SceneObjectStorage : ISceneObjectStorage
    {
        private Dictionary<Type, Component> _storage = new();
    
        public T Create<T>(string path) where T : Component
        {
            var prefab = Resources.Load<T>(path);
            if (prefab is null)
            {
                Debug.LogError($"prefab is null {path}");
                return null;
            }
    
            if (_storage.Keys.Contains(typeof(T)))
            {
                Debug.LogError($"This Item already created: {prefab}");
                return (T)_storage[typeof(T)];
            }
    
            var obj = Object.Instantiate(prefab);
            if (_storage.TryAdd(typeof(T), obj))
            {
                return obj;
            }
            else
            {
                Debug.LogError($"Item {prefab} doesn't adding to dictionary");
            }
    
            return null;
        }
    
        public T Get<T>() where T : Component
        {
            if (!_storage.ContainsKey(typeof(T)))
            {
                Debug.LogError($"The dictionary doesn't contains key {typeof(T)}");
            }
    
            Component component;
            if (_storage.TryGetValue(typeof(T), out component))
            {
                return (T)component;
            }
            else
            {
                Debug.LogError($"Item not got. Type: {typeof(T)}");
            }
    
            return null;
        }
    
        public bool Add<T>(T Object) where T : Component
        {
            if (_storage.TryAdd(typeof(T), Object))
            {
                return true;
            }
            else
            {
                Debug.LogError($"Item not added:{Object}");
                return false;
            }
        }
    
        public void Delete<T>() where T : Component
        {
            if (!_storage.Remove(typeof(T)))
            {
                Debug.LogError($"Item not removed. Key(Type): {typeof(T)}");
            }
        }
    }
}