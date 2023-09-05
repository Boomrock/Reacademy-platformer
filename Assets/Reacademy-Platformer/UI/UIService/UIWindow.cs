using System;
using UnityEngine;

namespace UI.UIService
{
    public abstract class UIWindow : MonoBehaviour, IUIWindow
    {
        [HideInInspector] public Action OnShowEvent;        
        [HideInInspector] public Action OnHideEvent;  
        
        public IUIService UIService { get; set; }
        
        public abstract void Show();
        public abstract void Hide();
    }
}