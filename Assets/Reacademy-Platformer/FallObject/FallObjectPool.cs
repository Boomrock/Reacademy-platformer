using System.Collections.Generic;
using FallObject;
using UnityEngine;
using Zenject;

public class FallObjectPool : MemoryPool<FallObjectType, FallObjectController>
{
    public FallObjectConfig ObjectConfig => _objectConfig;
        
    private FallObjectConfig _objectConfig = Resources.Load<FallObjectConfig>(ResourcesConst.FallObjectConfigPath);
    private FallObjectView _objectView = Resources.Load<FallObjectView>(ResourcesConst.FallObjectViewPath);

    public FallObjectPool()
    {
        
    }
    protected override void OnCreated(FallObjectController item)
    {
        var view = GameObject.Instantiate(_objectView);
        var model = _objectConfig.Get(FallObjectType.Type1);
        item = new FallObjectController(view, model);
        base.OnCreated(item);
    }

    protected override void Reinitialize(FallObjectType type, FallObjectController fallObjectView)
    {
        var view = GameObject.Instantiate(_objectView);
        var model = _objectConfig.Get(type);

        fallObjectView.View.SpriteRenderer.sprite = model.ObjectSprite;
        fallObjectView.View.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
    }
  
}
