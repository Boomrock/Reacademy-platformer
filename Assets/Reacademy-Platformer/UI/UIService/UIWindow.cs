using System;
using UnityEngine;

namespace UI.UIService
{
    public abstract class UIWindow : MonoBehaviour, IUIWindow
    {
        public Action OnShowEvent;        
        public Action OnHideEvent;  
        
        public IUIService UIService { get; set; }
        
        public abstract void Show();
        public abstract void Hide();
    }
}