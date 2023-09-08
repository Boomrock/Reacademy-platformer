using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI.UIService
{
       public class  UIService : IUIService
    {

        private readonly UIRoot _uIRoot;
        private readonly Dictionary<Type, IUIController> _controllersStorage = new Dictionary<Type,IUIController>();
        private readonly Dictionary<Type, IUIWindow> _initWindows= new Dictionary<Type, IUIWindow>();

        private const string UISource = "";
        
        public UIService(UnityEngine.Camera camera)
        {
            _uIRoot = Resources.Load<UIRoot>("UIRoot");
            _uIRoot = Object.Instantiate(_uIRoot);
            _uIRoot.RootCanvas.worldCamera = camera;
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
        
        public TUIWindow Get<TUIWindow>() where TUIWindow : IUIWindow
        {
            if (_initWindows.TryGetValue(typeof(TUIWindow), out var window))
            {
                return (TUIWindow)window;
            }
            return default;
        }
        private IUIController GetController<T>() where T : IUIWindow
        {
            if (_controllersStorage.TryGetValue(typeof(T), out var controller))
            {
                return controller;
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

        void Add<T>(IUIController controller) where T : IUIWindow
        {
            _controllersStorage.Add(typeof(T), controller);
        }  
        private void Init(Type t, Transform parent = null)
        {
            if(_controllersStorage.ContainsKey(t))
            {
                GameObject view = null;
                if(parent!=null)
                {
                    view = Object.Instantiate(_controllersStorage[t].gameObject, parent);
                }
                else
                {
                    view = Object.Instantiate(_controllersStorage[t].gameObject);
                }

                var uiWindow = view.GetComponent<UIWindow>();
                uiWindow.UIService = this;
                
                _initWindows.Add(t, view);
            }
        }
    }
}
