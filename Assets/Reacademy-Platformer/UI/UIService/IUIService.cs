using System;
using UnityEngine;

namespace UI.UIService
{
    public interface IUIService
    {
        T Show<T>() where T : IUIWindow;
        void Hide<T>(Action onEnd = null) where T : IUIWindow;
        T Get<T>() where T : IUIWindow;
        
    }
}