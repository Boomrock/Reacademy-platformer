using UnityEngine;
using Zenject;

namespace FallObject
{
    public class FallObjectFactory : IFactory<FallObjectType , FallObjectController>
    {
        public FallObjectConfig ObjectConfig => _objectConfig;
        
        private FallObjectConfig _objectConfig = Resources.Load<FallObjectConfig>(ResourcesConst.FallObjectConfigPath);
        private FallObjectView _objectView = Resources.Load<FallObjectView>(ResourcesConst.FallObjectView);

        public FallObjectController Create(FallObjectType type)
        {
            var view = GameObject.Instantiate(_objectView);
            var model = _objectConfig.Get(type);
            //var controller = new FallObjectController(view, model);

            view.SpriteRenderer.sprite = model.ObjectSprite;
            view.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);

            return default;
        }
    }
}