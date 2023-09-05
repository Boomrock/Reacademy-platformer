namespace UI.UIWindows
{
    public class UIMainMenuController
    {
        private readonly UIService.UIService _uiService;
        
        private UIMainMenuWindow _mainMenuWindow;
        private GameController _gameController;

        public UIMainMenuController(UIService.UIService uiService, GameController gameController)
        {
            _uiService = uiService;
            _gameController = gameController;
            _mainMenuWindow = uiService.Get<UIMainMenuWindow>();
            
            _mainMenuWindow.OnShowEvent += ShowWindow;
            _mainMenuWindow.OnHideEvent += HideWindow;
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