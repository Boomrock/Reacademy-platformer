using Zenject;

namespace UI.UIWindows
{
    public class UIEndGameWindowController
    {
        private readonly UIService.UIService _uiService;
       
        private UIEndGameWindow _endGameWindow;
        private GameController _gameController;
        public UIEndGameWindowController(UIService.UIService uiService)
        {
            _uiService = uiService;
            _endGameWindow = uiService.Get<UIEndGameWindow>();

           
            _endGameWindow.OnShowEvent += ShowWindow;
            _endGameWindow.OnHideEvent += HideWindow;
        }
        [Inject]
        void LateConstructor(GameController gameController)
        {
            _gameController = gameController;
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