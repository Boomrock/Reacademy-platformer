using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI.UIService
{
       public class UIService : IUIService
    {
        private Transform _deactivatedContainer;
        
        private readonly UIRoot _uIRoot;
        private readonly Dictionary<Type, IUIController> _controllersStorage = new Dictionary<Type,IUIController>();
        private readonly Dictionary<Type, GameObject> _initWindows= new Dictionary<Type, GameObject>();

        private const string UISource = "";
        
        public UIService(UnityEngine.Camera camera)
        {
            _uIRoot = Resources.Load<UIRoot>("UIRoot");
            _uIRoot = Object.Instantiate(_uIRoot);
            _uIRoot.RootCanvas.worldCamera = camera;

            LoadWindows(UISource);
            InitWindows(_uIRoot.PoolContainer);
        }

        public T Show<T>() where T : IUIController
        {
            var window = Get<T>();
            if(window != null)
            {
                window.transform.SetParent(_uIRoot.Container, false);

                var windowPosition = window.transform.position;
                windowPosition.y *= 2;
                window.transform.position = windowPosition;
                
                window.Show();
                return window;
            }
            return null;
        }
        
        public T Get<T>() where T : IUIController
        {
            var type = typeof(T);
            if (_initWindows.ContainsKey(type))
            {
                var view = _initWindows[type];            
                return view.GetComponent<T>();
            }
            return default;
        }

        public void Hide<T>(Action onEnd = null) where T : IUIController
        {
            var window = Get<T>();
            
            if(window!=null)
            {
                void changeParent() => window.transform.SetParent(_uIRoot.PoolContainer);
                window.OnHideEvent += changeParent;
                window.Hide();
                
                onEnd?.Invoke();
            }
        }

        void Add<T>(T controller) where T : IUIController
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
