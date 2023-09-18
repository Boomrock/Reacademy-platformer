using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI.UIService
{
       public class  UIService : IUIService
    {
        private readonly UIRoot _uiRoot;
        private readonly Dictionary<Type, IUIController> _controllersStorage = new Dictionary<Type,IUIController>();
        private readonly Dictionary<Type, IUIWindow> _initWindows= new Dictionary<Type, IUIWindow>();

        private const string UISource = "";
        
        public UIService(UnityEngine.Camera camera, UIRoot uiRoot)
        {
            _uiRoot = uiRoot;
            _uiRoot.RootCanvas.worldCamera = camera;
        }

        public TUIWindow Show<TUIWindow>() where TUIWindow : IUIWindow
        {
            var window = GetController<TUIWindow>();
            if(window != null)
            {
                window.ShowWindow();
                return (TUIWindow)window.UIWindow;
            }
            return default;
        }

        public T ShowOnly<T>() where T : IUIWindow
        {
            HideAll();
            return Show<T>();
        }

        public void HideAll(Action onEnd = null)
        {
            foreach (var pair in _controllersStorage)
            {
                pair.Value.HideWindow();
            }
            onEnd?.Invoke();
        }

        public TUIWindow Get<TUIWindow>() where TUIWindow : IUIWindow
        {
            if (_initWindows.TryGetValue(typeof(TUIWindow), out var window))
            {
                return (TUIWindow)window;
            }
            return default;
        }
        public void Hide<T>(Action onEnd = null) where T : IUIWindow
        {
            var windowController = GetController<T>();
            
            if(windowController!=null)
            {
                windowController.HideWindow();
                onEnd?.Invoke();
            }
        }

        public bool Add<T>(IUIController controller) where T : IUIWindow
        {
            
            return _controllersStorage.TryAdd(typeof(T), controller);
        }  
        public IUIController GetController<T>() where T : IUIWindow
        {
            if (_controllersStorage.TryGetValue(typeof(T), out var controller))
            {
                return controller;
            }
            return default;
        }



    }
}
