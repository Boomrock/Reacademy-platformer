using UnityEngine;

namespace UI.UIService
{
    public class UIRoot : MonoBehaviour, IUIRoot
    {
        public Transform Container => activateContainer;
        public Transform PoolContainer => deactivateContainer;
        public Canvas RootCanvas => rootCanvas;
        
        [SerializeField] private Transform activateContainer;
        [SerializeField] private Transform deactivateContainer;
        [SerializeField] private Canvas rootCanvas;
    }
}