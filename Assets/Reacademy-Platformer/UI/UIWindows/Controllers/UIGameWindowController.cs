using UI.HUD;
using UI.UIService;
using UnityEngine;

namespace UI.UIWindows
{
    public class UIGameWindowController: UIController<UIGameWindow>
    {
        private readonly HUDWindowController _hudWindowController;

        public UIGameWindowController(UIGameWindow window, 
            UIRoot uiRoot, 
            HUDWindowController hudWindowController) 
            : base(window, 
                uiRoot)
        {
            _hudWindowController = hudWindowController;
        }

        public override void ShowWindow()
        {
            base.ShowWindow();
            _hudWindowController.ShowWindow();
        }

        public override void HideWindow()
        {
            base.HideWindow();
            _hudWindowController.HideWindow();
        }
    }
}