using System;
using UnityEngine;

namespace UI.UIWindows
{
    public class UIMainMenuWindow : UIAnimationWindow
    {
        public Action OnStartButtonClickEvent;
        
        [SerializeField] private UIButton startButton;
        public override void Show()
        {
            base.Show();
            startButton.OnClickButton += StartButtonClickEvent;
        }

        public override void Hide()
        {
            base.Hide();
            startButton.OnClickButton -= StartButtonClickEvent;
        }

        private void StartButtonClickEvent()
        {
            OnStartButtonClickEvent?.Invoke();
        }
    }
}