using System;
using UnityEngine;

namespace UI.UIService
{
    public interface IUIService
    {
        T Show<T>() where T : IUIWindow;
        T ShowOnly<T>() where T : IUIWindow;
        void Hide<T>(Action onEnd = null) where T : IUIWindow;
        void HideAll(Action onEnd = null);
        T Get<T>() where T : IUIWindow;
        public bool Add<T>(IUIController controller) where T : IUIWindow;

    }
}