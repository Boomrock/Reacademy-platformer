namespace UI.UIWindows
{
    public class UIEndGameWindowController
    {
        private readonly UIService.UIService _uiService;
       
        private UIEndGameWindow _endGameWindow;
        private GameController _gameController;
        public UIEndGameWindowController(UIService.UIService uiService, GameController gameController)
        {
            _uiService = uiService;
            _gameController = gameController;
            _endGameWindow = uiService.Get<UIEndGameWindow>();

           
            _endGameWindow.OnShowEvent += ShowWindow;
            _endGameWindow.OnHideEvent += HideWindow;
        }

        private void ShowWindow()
        {
            _endGameWindow.OnReturnButtonClickEvent += ShowGameWindows;
            _endGameWindow.OnReturnButtonClickEvent += _gameController.StartGame;
        }
        private void HideWindow()
        {
            _endGameWindow.OnReturnButtonClickEvent -= ShowGameWindows;
            _endGameWindow.OnReturnButtonClickEvent -= _gameController.StartGame;
        }

        public void ShowGameWindows()
        {
            _uiService.Hide<UIEndGameWindow>();
            _uiService.Show<UIGameWindow>();
        }
    }
}