using UnityEngine;

namespace SceneObjectStorage
{
    public interface ISceneObjectStorage
    {
        T Create<T>(string path)where T : Component;
        T Get<T>() where T : Component;
        bool Add<T>(T gameObject)where T : Component;
        void Delete<T>() where T : Component;
    }
}