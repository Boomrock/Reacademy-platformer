using Zenject;

namespace UI.UIWindows
{
    public class UIMainMenuController
    {
        private readonly UIService.UIService _uiService;
        
        private UIMainMenuWindow _mainMenuWindow;
        private GameController _gameController;

        public UIMainMenuController(UIService.UIService uiService)
        {
            _uiService = uiService;
            _mainMenuWindow = uiService.Get<UIMainMenuWindow>();
            
            _mainMenuWindow.OnShowEvent += ShowWindow;
            _mainMenuWindow.OnHideEvent += HideWindow;
        }

        [Inject]
        void LateConstructor(GameController gameController)
        {
            _gameController = gameController;
        }
        private void ShowWindow()
        {
            _mainMenuWindow.OnStartButtonClickEvent += ShowGameWindow;
            _mainMenuWindow.OnStartButtonClickEvent += _gameController.StartGame;
        }
        private void HideWindow()
        {
            _mainMenuWindow.OnStartButtonClickEvent -= ShowGameWindow;
            _mainMenuWindow.OnStartButtonClickEvent -= _gameController.StartGame;

        }
        private void ShowGameWindow()
        {
            _uiService.Hide<UIMainMenuWindow>();
            _uiService.Show<UIGameWindow>();
        }
    }
}