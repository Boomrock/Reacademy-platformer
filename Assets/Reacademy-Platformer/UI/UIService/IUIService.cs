using System;
using UnityEngine;

namespace UI.UIService
{
    public interface IUIService
    {
        T Show<T>() where T : IUIController;
        void Hide<T>(Action onEnd = null) where T : IUIController;
        T Get<T>() where T : IUIController;

        void InitWindows(Transform poolDeactiveContiner);
        void LoadWindows(string source);
    }
}