using UI.UIService;
using UnityEngine.PlayerLoop;
using Zenject;

namespace UI.UIWindows
{
    public class UIMainMenuController : IUIController
    {
        private readonly UIService.UIService _uiService;
        
        private UIMainMenuWindow _mainMenuWindow;
        private GameController _gameController;

        public UIMainMenuController(UIMainMenuWindow mainMenuWindow)
        {
            _mainMenuWindow = mainMenuWindow;
        }
        [Inject]
        public void Initialization(GameController gameController)
        {
            _gameController = gameController;
        }

        public void ShowWindow()
        {
            _mainMenuWindow.OnStartButtonClickEvent += ShowGameWindow;
            _mainMenuWindow.OnStartButtonClickEvent += _gameController.StartGame;
            _mainMenuWindow.Show();
        }

        public void HideWindow()
        {
            _mainMenuWindow.Hide();
            _mainMenuWindow.OnStartButtonClickEvent -= ShowGameWindow;
            _mainMenuWindow.OnStartButtonClickEvent -= _gameController.StartGame;

        }
    }
}