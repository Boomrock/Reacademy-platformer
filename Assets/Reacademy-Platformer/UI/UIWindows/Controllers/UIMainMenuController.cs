using UI.UIService;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

namespace UI.UIWindows
{
    public class UIMainMenuController : UIController<UIMainMenuWindow>
    {
        private GameController _gameController;
        
        public UIMainMenuController(UIMainMenuWindow window, UIRoot uiRoot) : base(window, uiRoot) { }
        
        [Inject]
        public void Initialization(GameController gameController)
        {
            _gameController = gameController;
        }

        public override void ShowWindow()
        {
            base.ShowWindow();
            _window.OnStartButtonClickEvent += _gameController.StartGame;
        }

        public override void HideWindow()
        {
            base.HideWindow();
            _window.OnStartButtonClickEvent -= _gameController.StartGame;
        }

    }
}