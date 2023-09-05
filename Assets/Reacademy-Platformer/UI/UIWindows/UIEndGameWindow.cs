using System;
using UnityEngine;

namespace UI.UIWindows
{
    public class UIEndGameWindow : UIAnimationWindow
    {
        public Action OnReturnButtonClickEvent;
        
        [SerializeField] private UIButton returnButton;
        public override void Show()
        {
            base.Show();
            returnButton.OnClickButton += StartReturnButtonClickEvent;
        }

        public override void Hide()
        {
            base.Hide();
            returnButton.OnClickButton -= StartReturnButtonClickEvent;
        }

        private void StartReturnButtonClickEvent()
        {
            OnReturnButtonClickEvent.Invoke();
        }
    }
}