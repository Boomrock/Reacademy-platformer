using UI.HUD;
using UI.UIService;

namespace UI.UIWindows
{
    public class UIGameWindowController: IUIController
    {
        private readonly UIGameWindow _uiGameWindow;

        public UIGameWindowController(UIGameWindow uiGameWindow)
        {
            _uiGameWindow = uiGameWindow;
        }
        

        public void ShowWindow()
        {
            _uiGameWindow.Show();
        }

        public void HideWindow()
        {
            _uiGameWindow.Hide();
        }
    }
}