using UI.UIService;
using UnityEngine;
using Zenject;

namespace UI.UIWindows
{
    public class UIEndGameWindowController : UIController<UIEndGameWindow>
    {
        private readonly UIService.UIService _uiService;
        private GameController _gameController;

        public UIEndGameWindowController(UIEndGameWindow uiEndGameWindow, UIRoot uiRoot ) : base(uiEndGameWindow, uiRoot)
        {
            _window.OnShowEvent += ShowWindow;
            _window.OnHideEvent += HideWindow;
        }
        [Inject]
        public void Initialization(GameController gameController)
        {
            _gameController = gameController;
        }
        public override void ShowWindow()
        {
            base.ShowWindow();
            _window.OnReturnButtonClickEvent += _gameController.StartGame;
        }
        public override void HideWindow()
        {
            base.HideWindow();
            _window.OnReturnButtonClickEvent -= _gameController.StartGame;
        }
        
    }
}