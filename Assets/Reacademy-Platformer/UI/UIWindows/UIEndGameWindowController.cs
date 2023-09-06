using UI.UIService;
using Zenject;

namespace UI.UIWindows
{
    public class UIEndGameWindowController : IUIController
    {
        private readonly UIService.UIService _uiService;
       
        public UIEndGameWindow EndGameWindow;
        private GameController _gameController;
        public UIEndGameWindowController(UIService.UIService uiService)
        {
            _uiService = uiService;
            EndGameWindow = uiService.Get<UIEndGameWindow>();
           
            EndGameWindow.OnShowEvent += ShowWindow;
            EndGameWindow.OnHideEvent += HideWindow;
        }
        [Inject]
        public void Initialization(GameController gameController)
        {
            _gameController = gameController;
        }
        public void ShowWindow()
        {
            EndGameWindow.OnReturnButtonClickEvent += ShowGameWindows;
            EndGameWindow.OnReturnButtonClickEvent += _gameController.StartGame;
        }
        public void HideWindow()
        {
            EndGameWindow.OnReturnButtonClickEvent -= ShowGameWindows;
            EndGameWindow.OnReturnButtonClickEvent -= _gameController.StartGame;
        }

        public void ShowGameWindows()
        {
            _uiService.Hide<UIEndGameWindow>();
            _uiService.Show<UIGameWindow>();
        }
    }
}