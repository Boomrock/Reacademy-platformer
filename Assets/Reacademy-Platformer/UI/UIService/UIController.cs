using UnityEngine;

namespace UI.UIService
{
    public class UIController<TUIWindow>: IUIController where TUIWindow : UIWindow
    {
        public IUIWindow UIWindow { get => _window; }

        protected Transform _transform;
        protected UIRoot _uiRoot;
        protected TUIWindow _window;

        public UIController(TUIWindow window, UIRoot uiRoot)
        {
            _uiRoot = uiRoot;
            _window = window;
        }


        public virtual void ShowWindow()
        {
            (_transform = _window.transform).SetParent(_uiRoot.Container, false);
            var windowPosition = _transform.position;
            windowPosition.y *= 2;
            _transform.position = windowPosition;
            _window.Show();
        }

        public virtual void HideWindow()
        {
            _window.Hide();
            (_transform = _window.transform).SetParent(_uiRoot.PoolContainer, false);
        }
    }
}