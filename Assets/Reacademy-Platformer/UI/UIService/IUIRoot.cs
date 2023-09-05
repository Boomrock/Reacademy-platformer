using UnityEngine;

namespace UI.UIService
{
    public interface IUIRoot
    {
        Transform Container { get; }
        Transform PoolContainer { get; }
    }
}